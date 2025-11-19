using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs
{
    public class SpawnerProvider : MonoBehaviour
    {
        public string enemyKey;

        EnemySpawner _spawner;
        public EnemySpawner Spawner => _spawner ??= EnemySpawnController.I?.Spawners.GetValueOrDefault(enemyKey);
    }
}