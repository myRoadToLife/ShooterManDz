using System;
using System.Collections;
using UnityEngine;

public class OutcomeCheck : MonoBehaviour
{
    public event Action OnWinHoldTime;

    [SerializeField] private TypeToWin _situationWin;
    [SerializeField] private TypeToLose _situationLose;

    [SerializeField] private float _timeLimit;
    [SerializeField] private int _needToKillCount;
    [SerializeField] private int _enemyCountToCapture;
    [SerializeField] private UI _ui;

    private Player _player;
    private EntityList<Enemy> _entityList;

    private Coroutine _holdTimeCoroutine;
    private float _elapsedTime;
    private int _currentKilledEnemiesCount;

    public void Initialize(Player player, EntityList<Enemy> entityList)
    {
        _player = player;
        _entityList = entityList;

        switch (_situationWin)
        {
            case TypeToWin.HoldTime:
                _holdTimeCoroutine = StartCoroutine(StartWinTimer());
                OnWinHoldTime += DisplayWinMessageOnTime;
                break;
            case TypeToWin.KillEnemies:
                // Чтобы текст отображался сразу при инициализации, а не при первом убийстве
                _ui.TextCounter(_ui.WinCounterText, _ui.WinObjectiveText, _currentKilledEnemiesCount, _needToKillCount, _ui.ObjectiveKillEnemies, _ui.TextEnemy);

                _entityList.OnEntityRemoved += IncrementEnemyKillCount;
                break;
        }

        switch (_situationLose)
        {
            case TypeToLose.PlayerDied:
                // Чтобы текст отображался сразу при инициализации, а не при первом ранении
                UpdatePlayerHealthDisplay(_player.PlayerCurrentHealth);

                _player.OnChangedHealth += UpdatePlayerHealthDisplay;
                _player.OnPlayerDied += DisplayPlayerDeathMessage;
                break;
            case TypeToLose.ArenaCapture:
                _entityList.OnEntityAdded += UpdateArenaCaptureStatus;
                _entityList.OnEntityRemoved += UpdateRemainingEnemiesCount;
                break;
        }
    }

    public void UpdatePlayerHealthDisplay(int state)
    {
        _ui.TextCounter(_ui.LoseCounterText, _ui.LoseObjectiveTex, _player.PlayerCurrentHealth, state, _ui.ObjectivePlayerHealth, null);
    }

    private void DisplayWinMessageOnTime()
    {
        _ui.TextMessageEvent(Color.blue, _ui.WinMessageTimeOver);
        _ui.DisableTextObjects();

        UnsubscribeFromEvents();
        StopCoroutine();
    }

    private void DisplayPlayerDeathMessage()
    {
        _ui.TextMessageEvent(Color.red, _ui.LoseMessagePlayerDied);
        _ui.DisableTextObjects();

        UnsubscribeFromEvents();
        StopCoroutine();
        Destroy(_player.gameObject);
    }

    private void UpdateArenaCaptureStatus()
    {
        UpdateRemainingEnemiesDisplay();

        if (_entityList.Count >= _enemyCountToCapture)
        {
            _ui.TextMessageEvent(Color.red, _ui.LoseMessageArenaCaptured);
            _ui.DisableTextObjects();

            UnsubscribeFromEvents();
        }
    }

    private void UpdateRemainingEnemiesDisplay()
    {
        _ui.TextCounter(_ui.LoseCounterText, _ui.LoseObjectiveTex, _entityList.Count, _enemyCountToCapture, _ui.ObjectiveCaptureArena, _ui.TextEnemy);
    }

    private void IncrementEnemyKillCount()
    {
        _currentKilledEnemiesCount++;

        if (_currentKilledEnemiesCount >= _needToKillCount)
        {
            _ui.TextMessageEvent(Color.yellow, _ui.WinMessageEnemiesDefeated);

            UnsubscribeFromEvents();
            _ui.DisableTextObjects();
        }
    }

    private void UpdateRemainingEnemiesCount()
    {
        if (_currentKilledEnemiesCount < _needToKillCount)
        {
            _ui.TextCounter(_ui.WinCounterText, _ui.WinObjectiveText, _currentKilledEnemiesCount, _needToKillCount, _ui.ObjectiveKillEnemies, _ui.TextEnemy);

            UpdateRemainingEnemiesDisplay();
        }
    }

    private IEnumerator StartWinTimer()
    {
        _elapsedTime = 0;

        while (_elapsedTime < _timeLimit)
        {
            _elapsedTime += Time.deltaTime;
            _ui.TextCounter(_ui.WinCounterText, _ui.WinObjectiveText, _elapsedTime, _timeLimit, _ui.ObjectiveHoldArena, _ui.TextTime);
            yield return null;
        }

        OnWinHoldTime?.Invoke();
    }

    private void StopCoroutine()
    {
        if (_holdTimeCoroutine != null)
        {
            StopCoroutine(_holdTimeCoroutine);
        }
    }

    private void UnsubscribeFromEvents()
    {
        OnWinHoldTime -= DisplayWinMessageOnTime;

        if (_entityList != null)
        {
            _entityList.OnEntityRemoved -= UpdateRemainingEnemiesCount;
            _entityList.OnEntityAdded -= UpdateArenaCaptureStatus;
            _entityList.OnEntityRemoved -= IncrementEnemyKillCount;
        }

        if (_player != null)
        {
            _player.OnPlayerDied -= DisplayPlayerDeathMessage;
            _player.OnChangedHealth -= UpdatePlayerHealthDisplay;
        }
    }
}
