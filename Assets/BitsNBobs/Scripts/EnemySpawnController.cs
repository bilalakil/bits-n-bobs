using System.Collections.Generic;
using BitsNBobs.Data;
using UnityEngine;

namespace BitsNBobs
{
    public class EnemySpawnController : MonoBehaviour
    {
        public static EnemySpawnController I { get; private set; }
        
        public float spawnDistance;
        [SerializeField] EnemyLibrary enemyLibrary;

        public EnemyLibrary EnemyLibrary => enemyLibrary;
        public IReadOnlyDictionary<string, EnemySpawner> Spawners;

        public void Awake()
        {
            var spawners = new Dictionary<string, EnemySpawner>();
            foreach (var enemyKey in enemyLibrary.EnemyPrefabsByKey.Keys)
                spawners[enemyKey] = new EnemySpawner(this, enemyKey);
            Spawners = spawners;
        }

        public void OnEnable()
        {
            I = this;
        }

        public void OnDisable()
        {
            I = null;
        }

        public void Update()
        {
            foreach (var spawner in Spawners.Values)
                spawner.Update();
        }

        public bool TryUpgradeSpawner(string enemyKey) =>
            Spawners.TryGetValue(enemyKey, out var spawner) && spawner.TryUpgrade();

        public void Spawn(string enemyKey)
        {
            if (!enemyLibrary.EnemyPrefabsByKey.TryGetValue(enemyKey, out var prefab)) return;
            var spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;
            // TODO: Pooling!
            Instantiate(prefab, transform.position + new Vector3(spawnPosition.x, spawnPosition.y, 0),
                Quaternion.identity);
        }
    }
}