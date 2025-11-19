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

        [SerializeField] List<string> enemyKeys;
        public IReadOnlyList<string> EnemyKeys => enemyKeys;

        [SerializeField] List<int> enemyCounts;
        public IReadOnlyList<int> EnemyCounts => enemyCounts;

        public Wave(int timeSeconds, List<string> enemyKeys, List<int> enemyCounts)
        {
            this.timeSeconds = timeSeconds;
            this.enemyKeys = enemyKeys;
            this.enemyCounts = enemyCounts;
        }
    }
}