using UnityEngine;

namespace BitsNBobs
{
    public class EnemySpawnController : MonoBehaviour
    {
        public static EnemySpawnController I { get; private set; }
        
        public float spawnDistance;

        public void OnEnable()
        {
            I = this;
        }

        public void OnDisable()
        {
            I = null;
        }

        public void Spawn(GameObject prefab)
        {
            var spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;
            // TODO: Pooling!
            Instantiate(prefab, transform.position + new Vector3(spawnPosition.x, spawnPosition.y, 0),
                Quaternion.identity);
        }
    }
}