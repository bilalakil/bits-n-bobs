using UnityEngine;

namespace BitsNBobs
{
    public class EnemySpawnController : MonoBehaviour
    {
        public float spawnDistance;

        public void Spawn(GameObject prefab)
        {
            var spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;
            // TODO: Pooling!
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}