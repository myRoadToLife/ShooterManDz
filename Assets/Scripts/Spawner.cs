using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3[] _spawnPoints;
    private Enemy _enemyPrefab;
    private float _spawnInterval = 3f; // �������� ����� �������� ������

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
            // �������� ��������� ����� ������
            Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            // ������� ����� � ��������� �����
            InitializeEnemy(spawnPoint);

            // ��� ��������� �������� ����� ��������� �������
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
