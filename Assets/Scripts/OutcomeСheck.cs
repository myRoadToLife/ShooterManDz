using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class OutcomeСheck : MonoBehaviour
{
    public event Action OnWinHoldTime;

    [SerializeField] private TypeToWin _situationWin;
    [SerializeField] private TypeToLose _situationLose;

    [SerializeField] private float _timeLimit;
    [SerializeField] private int _killCount;
    [SerializeField] private int _enemyCountToCapture;

    [SerializeField] private TMP_Text _textWin;
    [SerializeField] private TMP_Text _textLose;

    private Spawner _spawnerEnemy;
    private Player _player;
    private EntityList<Enemy> _entityList;

    private Coroutine _holdTimeoroutine;
    private float _elapsedTime;

    private int _removedEnemiesCount;

    public void Initialize(Player player, Spawner spawner, EntityList<Enemy> entityList)
    {
        _textLose.gameObject.SetActive(false);
        _textWin.gameObject.SetActive(false);

        _player = player;
        _spawnerEnemy = spawner;
        _entityList = entityList;

        switch (_situationWin)
        {
            case TypeToWin.HoldTime:
                _holdTimeoroutine = StartCoroutine(TimeForWin());
                OnWinHoldTime += OnPlayerHoldTime;
                break;
            case TypeToWin.KillEnemies:
                _entityList.OnEntityRemoved += HandleEnemyRemoved;
                break;
        }

        switch (_situationLose)
        {
            case TypeToLose.PlayerDied:
                _player.OnPlayerDied += OnPlayerDied;
                break;
            case TypeToLose.ArenaCapture:
                _entityList.OnEntityAdded += HandleEnemyAdded;
                break;
        }
    }

    private void OnPlayerHoldTime()
    {
        _textWin.gameObject.SetActive(true);
        _textWin.text = "Время вышло! Игрок победил!";
    }

    private void OnPlayerDied()
    {
        _textLose.gameObject.SetActive(true);
        _textLose.text = "Игрок умер от потери крови!";
    }

    private void HandleEnemyRemoved(Enemy enemy)
    {
        _removedEnemiesCount++;

        if (_removedEnemiesCount >= _killCount)
        {
            Debug.Log($"Ты убил {_removedEnemiesCount} врагов и победил");

            _entityList.OnEntityRemoved -= HandleEnemyRemoved;
        }
    }

    private void HandleEnemyAdded(Enemy enemy)
    {
        if (_entityList.Count >= _enemyCountToCapture)
        {
            Debug.Log("Враги захватили арену, ты проиграл!");

            _entityList.OnEntityAdded -= HandleEnemyAdded;
        }
    }

    private IEnumerator TimeForWin()
    {
        _elapsedTime = 0;

        while (_elapsedTime < _timeLimit)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;  // Ожидание до следующего кадра
        }

        OnWinHoldTime?.Invoke();
    }
}
