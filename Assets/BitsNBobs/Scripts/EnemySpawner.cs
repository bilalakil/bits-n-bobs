using System;
using UnityEngine;

namespace BitsNBobs
{
    public class EnemySpawner
    {
        public int UpgradeCost { get; private set; }
        public float NextSpawnPct { get; private set; }
        public event Action OnUpgraded;

        readonly EnemySpawnController _controller;
        readonly string _enemyKey;

        float _numSpawnsPerSecond;
        
        public EnemySpawner(EnemySpawnController controller, string enemyKey)
        {
            _controller = controller;
            _enemyKey = enemyKey;

            UpgradeCost = Config.BASE_ENEMY_SPAWNER_UPGRADE_COST;
            _numSpawnsPerSecond = Config.BASE_ENEMY_SPAWNER_SPAWNS_PER_SECOND;
        }

        public void Update()
        {
            if (_numSpawnsPerSecond <= 0) return;
            NextSpawnPct += _numSpawnsPerSecond * Time.deltaTime;
            if (NextSpawnPct < 1) return;
            NextSpawnPct = 0;
            _controller.Spawn(_enemyKey);
        }

        public bool TryUpgrade()
        {
            if (!CoinController.I) return false;
            if (CoinController.I.CoinAmount < UpgradeCost) return false;

            CoinController.I.CoinAmount -= UpgradeCost;
            
            UpgradeCost += Config.ENEMY_SPAWNER_UPGRADE_COST_ADDITION;
            _numSpawnsPerSecond += Config.ENEMY_SPAWNER_SPAWNS_PER_SECOND_GAIN;
            OnUpgraded?.Invoke();
            
            return true;
        }
    }
}