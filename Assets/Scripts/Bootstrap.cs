using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Spawner _spawnerPrefab;


    private void Awake()
    {
        InitializePlayer();
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        Spawner spawner = Instantiate(_spawnerPrefab);

        Vector3[] spawnPoints = new Vector3[]
        {
            new Vector3(0, 0, -5),
            new Vector3(5, 0, 0),
            new Vector3(-5, 0, 0),
            new Vector3(0, 0, 5)
        };

        spawner.Initialize(spawnPoints, _enemyPrefab);
    }

    private void InitializePlayer()
    {
        Player player = Instantiate(_playerPrefab);
        GameInput userInput = player.GetComponent<GameInput>();
        Gun gun = player.GetComponentInChildren<Gun>();
        Mover playerMover = new Mover();
        Health playerHealth = new Health(100);

        player.Initialize(playerMover, playerHealth, userInput, gun);
    }
}
