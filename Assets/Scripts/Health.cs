using System;
using UnityEngine;

public class Health
{
    public event Action<int> OnHealthChanged;

    private int _maxValue;
    public int CurrentValue { get; private set; }

    public Health(int maxValue)
    {
        _maxValue = maxValue;
        CurrentValue = _maxValue;
    }

    public void ReduceHealth(int damage)
    {
        CurrentValue -= damage;

        CurrentValue = Mathf.Clamp(CurrentValue, 0, _maxValue);

        OnHealthChanged?.Invoke(CurrentValue);

        if (CurrentValue <= 0)
            Debug.Log("Смерть");
    }
}
