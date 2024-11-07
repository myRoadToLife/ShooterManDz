using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Spawner _spawnerPrefab;
    [SerializeField] private Outcome—heck _outcome—heck;

    [SerializeField] private Transform _pointSpawnPlayer;

    private EntityList<Enemy> _entityList;

    private void Awake()
    {
        InitializeEntityList();

        InitializePlayer();
        InitializeSpawner();
        InitializeOutcomeCheck();

    }

    private void InitializeEntityList() => _entityList = new EntityList<Enemy>();

    private void InitializeOutcomeCheck() => _outcome—heck.Initialize(_playerPrefab, _spawnerPrefab, _entityList);
 
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

        spawner.InitializeSpawner(spawnPoints, _enemyPrefab, _entityList);
    }

    private void InitializePlayer()
    {
        Player player = Instantiate(_playerPrefab, _pointSpawnPlayer.position, Quaternion.identity);
        GameInput userInput = player.GetComponent<GameInput>();
        Gun gun = player.GetComponentInChildren<Gun>();
        Mover playerMover = new Mover();
        Health playerHealth = new Health(100);

        player.Initialize(playerMover, playerHealth, userInput, gun);
    }
}
