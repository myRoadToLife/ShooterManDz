using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Spawner _spawnerPrefab;
    [SerializeField] private VictoryChecker _victoryChecker;


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

        _victoryChecker.Initialize(spawner);

        spawner.InitializeSpawner(spawnPoints, _enemyPrefab);
        spawner.InitializeVictoryChecker(_victoryChecker);
        
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
