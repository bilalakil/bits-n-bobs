using UnityEngine;

namespace BitsNBobs
{
    public static class PlayerCommands
    {
        public static void UpgradeEnemySpawner(string enemyKey)
        {
            if (!EnemySpawnController.I)
            {
                Debug.LogError($"[{nameof(UpgradeEnemySpawner)}]: No {nameof(EnemySpawnController)}!");
                return;
            }
            
            if (!EnemySpawnController.I.EnemyLibrary.EnemyPrefabsByKey.ContainsKey(enemyKey))
            {
                Debug.LogError($"[{nameof(UpgradeEnemySpawner)}]: Invalid enemy key!");
                return;
            }

            EnemySpawnController.I.TryUpgradeSpawner(enemyKey);
        }

        public static void TryBuyItem(int index)
        {
            if (index == -1 || !ShopController.I || !Player.I)
                return;
            ShopController.I.TryBuyItem(index);
        }

        public static void TryRefreshShop()
        {
            if (!ShopController.I || !CoinController.I)
                return;
            ShopController.I.TryRefreshShop();
        }
    }
}