using System.Collections;
using BitsNBobs.Data;
using UnityEngine;

namespace BitsNBobs
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] WaveLibrary waveLibrary;

        int _nextWaveI;

        public void Update()
        {
            if (_nextWaveI >= waveLibrary.OrderedWaves.Count)
                return;

            var timeSecondsPassed = (int)Time.time;
            var wave = waveLibrary.OrderedWaves[_nextWaveI];
            if (timeSecondsPassed < wave.TimeSeconds)
                return;
            
            StartCoroutine(StartSpawning(wave));
            _nextWaveI++;
        }

        static IEnumerator StartSpawning(Wave wave)
        {
            for (var enemyTypeI = 0; enemyTypeI < wave.EnemyKeys.Count; ++enemyTypeI)
            {
                var enemyPrefab = wave.EnemyKeys[enemyTypeI];
                var totalCount = wave.EnemyCounts[enemyTypeI];
                for (var enemyCountI = 0; enemyCountI < totalCount; ++enemyCountI)
                {
                    EnemySpawnController.I.Spawn(enemyPrefab);
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }
}