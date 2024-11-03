using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3[] _spawnPoints;
    private Enemy _enemyPrefab;
    private float _spawnInterval = 3f; // Интервал между спавнами врагов

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void Initialize(Vector3[] spawnPoints, Enemy enemyPrefab)
    {
        _spawnPoints = spawnPoints;
        _enemyPrefab = enemyPrefab;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Выбираем случайную точку спавна
            Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            // Спавним врага в выбранной точке
            InitializeEnemy(spawnPoint);

            // Ждём указанный интервал перед следующим спавном
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void InitializeEnemy(Vector3 pointSpawn)
    {
        Enemy enemy = Instantiate(_enemyPrefab, pointSpawn, Quaternion.identity);
        GameInput userInput = enemy.GetComponent<GameInput>();
        Mover enemyMover = new Mover();
        Health enemyHealth = new Health(50);

        enemy.Initialize(enemyMover, enemyHealth, userInput);
    }
}
