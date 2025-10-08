using UnityEngine;

namespace BitsNBobs
{
    public static class PlayerCommands
    {
        public static void SpawnEnemy(GameObject prefab)
        {
            if (!prefab)
            {
                Debug.LogError($"[{nameof(SpawnEnemy)}]: Prefab is null!");
                return;
            }

            if (!EnemySpawnController.I)
            {
                Debug.LogError($"[{nameof(SpawnEnemy)}]: No {nameof(EnemySpawnController)}!");
                return;
            }

            EnemySpawnController.I.Spawn(prefab);
        }

        public static void TryBuyItem(int index)
        {
            if (index == -1 || !ShopController.I || !Player.I)
                return;
            ShopController.I.TryBuyItem(index);
        }
    }
}