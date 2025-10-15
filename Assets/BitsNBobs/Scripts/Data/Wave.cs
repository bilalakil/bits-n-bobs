using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitsNBobs.Data
{
    [Serializable]
    public class Wave
    {
        [SerializeField] int timeSeconds;
        public int TimeSeconds => timeSeconds;

        [SerializeField] List<GameObject> enemyPrefabs;
        public IReadOnlyList<GameObject> EnemyPrefabs => enemyPrefabs;

        [SerializeField] List<int> enemyCounts;
        public IReadOnlyList<int> EnemyCounts => enemyCounts;

        public Wave(int timeSeconds, List<GameObject> enemyPrefabs, List<int> enemyCounts)
        {
            this.timeSeconds = timeSeconds;
            this.enemyPrefabs = enemyPrefabs;
            this.enemyCounts = enemyCounts;
        }
    }
}