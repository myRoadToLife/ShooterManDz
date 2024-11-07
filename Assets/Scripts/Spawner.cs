using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Enemy _enemyPrefab;
    private EntityList<Enemy> _enemyList;

    private Vector3[] _spawnPoints;
    private Coroutine _spawnCoroutine;

    private float _spawnInterval = 3f;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void InitializeSpawner(Vector3[] spawnPoints, Enemy enemyPrefab, EntityList<Enemy> entityList)
    {
        _spawnPoints = spawnPoints;
        _enemyPrefab = enemyPrefab;
        _enemyList = entityList;
    }
 
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            InitializeEnemy(spawnPosition);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void InitializeEnemy(Vector3 spawnPosition)
    {
        Enemy enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        GameInput userInput = enemy.GetComponent<GameInput>();
        Mover enemyMover = new Mover();
        Health enemyHealth = new Health(50);

        enemy.Initialize(enemyMover, enemyHealth, userInput, this, _enemyList);

        _enemyList.Add(enemy);
    }
}
