using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public event Action OnEnemyKilled;

    private VictoryChecker _victoryChecker;
    private Vector3[] _spawnPoints;
    private Enemy _enemyPrefab;

    private float _spawnInterval = 3f;
    private int _enemyKillCount;

    private Coroutine _spawnCorutine;

    private void Start()
    {
       _spawnCorutine = StartCoroutine(SpawnEnemies());
    }

    public void InitializeVictoryChecker(VictoryChecker victoryChecker)
    {
        _victoryChecker = victoryChecker;
    }

    public void InitializeSpawner(Vector3[] spawnPoints, Enemy enemyPrefab)
    {
        _spawnPoints = spawnPoints;
        _enemyPrefab = enemyPrefab;
    }

    public void AddKill()
    {
        _enemyKillCount++;
        OnEnemyKilled?.Invoke();
    }

    internal int GetKillCount()
    {
        return _enemyKillCount;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];

            InitializeEnemy(spawnPoint, this);

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void InitializeEnemy(Vector3 pointSpawn, Spawner spawner)
    {
        Enemy enemy = Instantiate(_enemyPrefab, pointSpawn, Quaternion.identity);

        GameInput userInput = enemy.GetComponent<GameInput>();
        Mover enemyMover = new Mover();
        Health enemyHealth = new Health(50);

        enemy.Initialize(enemyMover, enemyHealth, userInput, this);
    }
}
