using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int Damage;

    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _speedMove;

    private float _pointReachThreshold = .2f;
    private Vector3 _targetPoint;

    private Mover _mover;
    private Health _health;
    private Spawner _spawner;
    private GameInput _moveInput;

    private void Update()
    {
        Patrol();
    }

    public void Initialize(Mover mover, Health health, GameInput moveInput, Spawner spawner)
    {
        _mover = mover;
        _health = health;
        _moveInput = moveInput;
        _spawner = spawner;

        _health.OnHealthChanged += Health_OnHealthChanged;
    }

    private void Health_OnHealthChanged(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            _spawner.AddKill();
            Destroy(gameObject);
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, _targetPoint) < _pointReachThreshold)
        {
            Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * _patrolRadius;
            _targetPoint = new Vector3(randomPoint.x, transform.position.y, randomPoint.y);
        }

        Vector3 directionToTarget = (_targetPoint - transform.position).normalized;
        Vector2 inputVector = new Vector2(directionToTarget.x, directionToTarget.z);

        _mover.Movement(transform, inputVector, _speedMove, _speedRotate);
    }

    public void TakeDamage(int damage)
    {
        _health.ReduceHealth(damage);
    }
}