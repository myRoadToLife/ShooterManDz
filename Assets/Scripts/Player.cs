using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public event Action OnPlayerDied;
    public event Action<int> OnChangedHealth;

    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotate;

    private Mover _mover;
    private Health _health;
    private GameInput _userInput;
    private Gun _gun;

    public int PlayerCurrentHealth {  get; private set; }

    private void Update()
    {
        Vector2 inputVector = _userInput.GetMovementVectorNormalized();

        _mover.Movement(transform, inputVector, _speedMove, _speedRotate);
    }

    public void Initialize(Mover mover, Health health, GameInput userInput, Gun gun)
    {
        _mover = mover;
        _health = health;
        _userInput = userInput;
        _gun = gun;

        PlayerCurrentHealth = _health.CurrentValue;

        _userInput.OnShoted += OnShoted;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            TakeDamage(enemy.Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        _health.ReduceHealth(damage);

        OnChangedHealth?.Invoke(_health.CurrentValue);

        if (_health.CurrentValue <= 0)
        {
            OnPlayerDied?.Invoke();
        }
    }

    private void OnShoted()
    {
        _gun.Shot();
    }
}
