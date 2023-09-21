using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _prefabsEnemy;
        [SerializeField] private int _numberOfEnemiesToSpawn = 3;
        [SerializeField] private Vector2 _spawnAreaMin;
        [SerializeField] private Vector2 _spawnAreaMax;

        private void Start()
        {
            for (int i = 0; i < _numberOfEnemiesToSpawn; i++)
            {
                SpawnRandomEnemy();
            }
        }

        private void SpawnRandomEnemy()
        {
            if (_prefabsEnemy.Count == 0)
            {
                Debug.LogWarning("No enemy prefabs assigned.");
                return;
            }

            GameObject randomEnemyPrefab = _prefabsEnemy[Random.Range(0, _prefabsEnemy.Count)];

            float randomX = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
            float randomY = Random.Range(_spawnAreaMin.y, _spawnAreaMax.y);

            Instantiate(randomEnemyPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
        }
    }
}