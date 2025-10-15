using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace BitsNBobs.Data
{
    [CreateAssetMenu(menuName = "BitsNBobs/EnemyLibrary", fileName = "EnemyLibrary")]
    public class EnemyLibrary : ScriptableObject
    {
        [SerializeField] List<GameObject> enemyPrefabs = new();
        public IReadOnlyList<GameObject> EnemyPrefabs => enemyPrefabs;

        readonly Dictionary<string, GameObject> _enemyPrefabsByKey = new();
        public IReadOnlyDictionary<string, GameObject> EnemyPrefabsByKey => _enemyPrefabsByKey;

        public void OnEnable() => RegenerateLookups();
        
        public void RegenerateLookups()
        {
            _enemyPrefabsByKey.Clear();
            foreach (var prefab in enemyPrefabs)
                _enemyPrefabsByKey[prefab.name] = prefab;
        }

#if UNITY_EDITOR
        public void OnValidate() => RegenerateLookups();

        const string PREFAB_FOLDER = "Assets/BitsNBobs/Prefabs/Enemies";
        const string LOG_PREFIX = "[EnemyLibrary] ";

        [ContextMenu("Regenerate")]
        public void RegenerateLibrary()
        {
            RepopulateEnemyPrefabs();
            RegenerateLookups();

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void RepopulateEnemyPrefabs()
        {
            enemyPrefabs.Clear();

            var fullFolderPath = Path.Combine(Application.dataPath, PREFAB_FOLDER["Assets/".Length..]);

            if (!Directory.Exists(fullFolderPath))
            {
                Debug.LogError($"{LOG_PREFIX} Missing enemies folder: {fullFolderPath}");
                return;
            }
            
            var prefabFilePaths = Directory.GetFiles(fullFolderPath, "*.prefab", SearchOption.AllDirectories);
            foreach (var absoluteFilePath in prefabFilePaths)
            {
                var assetPath = "Assets" + absoluteFilePath.Replace(Application.dataPath, "").Replace("\\", "/");
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                if (!prefab)
                {
                    Debug.LogError($"{LOG_PREFIX} Failed to load prefab at {assetPath}");
                    continue;
                }

                if (!prefab.TryGetComponent<Enemy>(out _))
                {
                    Debug.LogError($"{LOG_PREFIX} {prefab.name} prefab did not have an {nameof(Enemy)} component!");
                    continue;
                }
                
                enemyPrefabs.Add(prefab);
            }
        }
#endif
    }
}