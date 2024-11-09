using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int Damage;

    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _speedMove;

    private Mover _mover;
    private Health _health;
    private Spawner _spawner;
    private GameInput _moveInput;
    private EntityList<Enemy> _enemyList;

    private void Update()
    {
        _mover.RandomPatrol(transform, _patrolRadius, _speedMove, _speedRotate);
    }

    public void Initialize(Mover mover, Health health, GameInput moveInput, Spawner spawner,EntityList<Enemy> entityList)
    {
        _mover = mover;
        _health = health;
        _moveInput = moveInput;
        _spawner = spawner;
        _enemyList = entityList;

        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            _enemyList.Remove(this);

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        _health.ReduceHealth(damage);
    }
}