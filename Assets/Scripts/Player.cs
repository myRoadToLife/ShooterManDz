using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public event Action<string> OnPlayerDied;

    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotate;

    private Mover _mover;
    private Health _health;
    private GameInput _userInput;
    private Gun _gun;

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

        _userInput.OnShoted += OnShoted;
        _health.OnHealthChanged += OnHealthChanged;

        Debug.Log(_health.CurrentValue.ToString());
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

        if (_health.CurrentValue <= 0)
        {
            OnPlayerDied?.Invoke("Игрок умер!");

            _health.OnHealthChanged -= OnHealthChanged;
        }
    }
    private void OnShoted()
    {
        _gun.Shot();
    }

    private void OnHealthChanged(int currentHealth)
    {
        Debug.Log("Здоровье игрока " + currentHealth.ToString());

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
