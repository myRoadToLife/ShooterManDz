using System;
using System.Collections;
using UnityEngine;

public class VictoryChecker : MonoBehaviour
{
    public event Action<string> OnVictory;

    [SerializeField] private TypeToWin _condition;
    [SerializeField] private float _timeLimit;
    [SerializeField] private int _killCount;

    private Spawner _spawner;

    private float _elapsedTime;

    public void Initialize(Spawner spawner)
    {
        _spawner = spawner;
        OnVictory += VictoryChecker_OnVictory;

        switch (_condition)
        {
            case TypeToWin.HoldTime:
                StartCoroutine(CheckTimeVictory());
                break;
            case TypeToWin.KillEnemies:
                _spawner.OnEnemyKilled += CheckKillVictory;
                break;
        }
    }

    private void VictoryChecker_OnVictory(string obj)
    {
        Debug.Log(obj);
    }

    private IEnumerator CheckTimeVictory()
    {
        yield return new WaitForSeconds(_timeLimit);
        OnVictory?.Invoke("Победа по времени!");
    }

    private void CheckKillVictory()
    {
        int enemyKillCount = _spawner.GetKillCount();
        Debug.Log(enemyKillCount);

        if (enemyKillCount >= _killCount)
        {
            OnVictory?.Invoke("Победа по убийствам!");
            _spawner.OnEnemyKilled -= CheckKillVictory; // Отписываемся от события
        }
    }

}
