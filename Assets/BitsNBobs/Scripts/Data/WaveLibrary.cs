using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
#endif

namespace BitsNBobs.Data
{
    [CreateAssetMenu(menuName = "BitsNBobs/WaveLibrary", fileName = "WaveLibrary")]
    public class WaveLibrary : ScriptableObject
    {
        [SerializeField] List<Wave> orderedWaves = new();
        public IReadOnlyList<Wave> OrderedWaves => orderedWaves;

        [SerializeField] EnemyLibrary enemyLibrary;
        
#if UNITY_EDITOR
        class RelevantConfigNames
        {
            public readonly HashSet<string> WaveNames = new();
            public readonly Dictionary<string, HashSet<string>> SpawnableEnemyNamesByWaveName = new();
        }
        RelevantConfigNames _configNames;
        
        const string LOG_PREFIX = "[WaveLibrary] ";

        [ContextMenu("Regenerate")]
        public void RegenerateLibrary()
        {
            enemyLibrary.RegenerateLookups();
           
            orderedWaves.Clear();

            _configNames = ExtractConfigNames();
            foreach (var waveName in _configNames.WaveNames)
            {
                if (!TryBuildWave(waveName, out var wave))
                    continue;
                orderedWaves.Add(wave);
            }
            
            orderedWaves.Sort((waveA, waveB) => waveA.TimeSeconds - waveB.TimeSeconds);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        static readonly Regex s_waveNameInConfigRe = new(@"^Wave\.([^\.]+)\.(.+)$");
        static readonly Regex s_spawnCountsInConfigRemainderRe = new(@"^SpawnCounts\.([^.]+)$");
        RelevantConfigNames ExtractConfigNames()
        {
            var result = new RelevantConfigNames();
            foreach (var match in Config.DataKeys.Select(key => s_waveNameInConfigRe.Match(key))
                         .Where(match => match.Success))
            {
                var waveName = match.Groups[1].Value;
                result.WaveNames.Add(waveName);

                var remainder = match.Groups[2].Value;
                var spawnCountMatch = s_spawnCountsInConfigRemainderRe.Match(remainder);
                if (spawnCountMatch.Success)
                {
                    if (!result.SpawnableEnemyNamesByWaveName.ContainsKey(waveName))
                        result.SpawnableEnemyNamesByWaveName.Add(waveName, new HashSet<string>());
                    result.SpawnableEnemyNamesByWaveName[waveName].Add(spawnCountMatch.Groups[1].Value);
                }
            }
            return result;
        }

        bool TryBuildWave(string waveName, out Wave wave)
        {
            var hasError = false;
            if (!TryGetTime(waveName, out var timeSeconds))
            {
                Debug.LogError($"{LOG_PREFIX} {waveName} is missing time!");
                hasError = true;
            }

            if (!TryGetEnemySpawns(waveName, out var enemyPrefabs, out var enemyCounts))
            {
                // Logs its own errors...
                hasError = true;
            }
            else if (enemyPrefabs.Count == 0)
            {
                Debug.LogError($"{LOG_PREFIX} {waveName} has no specified enemies!");
                hasError = true;
            }

            wave = hasError ? null : new Wave(timeSeconds, enemyPrefabs, enemyCounts);
            return !hasError;
        }

        static bool TryGetTime(string waveName, out int timeSeconds)
        {
            var configKey = $"Wave.{waveName}.TimeSeconds";
            return Config.TryGet(configKey, out timeSeconds);
        }

        bool TryGetEnemySpawns(string waveName, out List<GameObject> enemyPrefabs, out List<int> enemyCounts)
        {
            enemyPrefabs = new List<GameObject>();
            enemyCounts = new List<int>();

            if (!_configNames.SpawnableEnemyNamesByWaveName.ContainsKey(waveName))
            {
                Debug.LogError($"{LOG_PREFIX} {waveName} is missing spawnable enemies!");
                return false;
            }
            
            var hasError = false;
            foreach (var enemyKey in _configNames.SpawnableEnemyNamesByWaveName[waveName])
            {
                if (!enemyLibrary.EnemyPrefabsByKey.TryGetValue(enemyKey, out var enemyPrefab))
                {
                    Debug.LogError($"{LOG_PREFIX} {waveName} contains non-existent enemy: {enemyKey}");
                    hasError = true;
                    continue;
                }

                if (
                    !Config.TryGet<int>($"Wave.{waveName}.SpawnCounts.{enemyKey}", out var spawnCount) ||
                    spawnCount < 0
                )
                {
                    Debug.LogError($"{LOG_PREFIX} {waveName} must have a non-negative integer spawn count for {enemyKey}!");
                    hasError = true;
                    continue;
                }
                
                if (spawnCount == 0)
                    continue;
                
                enemyPrefabs.Add(enemyPrefab);
                enemyCounts.Add(spawnCount);
            }

            return !hasError;
        }
#endif
    }
}