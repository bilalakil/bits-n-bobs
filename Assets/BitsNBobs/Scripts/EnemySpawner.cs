using System;
using UnityEngine;

namespace BitsNBobs
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float intervalSeconds;
        public int count;

        EnemySpawnController _enemySpawnController;
        float _nextSpawnTime;

        public void Awake()
        {
            _enemySpawnController = GetComponentInParent<EnemySpawnController>();
        }

        public void OnEnable()
        {
            _nextSpawnTime = Time.time + intervalSeconds;
        }

        public void Update()
        {
            if (Time.time < _nextSpawnTime)
                return;
            _nextSpawnTime = Time.time + intervalSeconds;
            for (var i = 0; i < count; ++i)
                _enemySpawnController.Spawn(prefab);
        }
    }
}