using UnityEngine;

public class Mover
{
    private Vector3 _targetPoint;
    private float _pointReachThreshold = .2f;

    private float _changeDirectionInterval = 3.5f;
    private float _timeSinceLastChange = 0f;

    public void Movement(Transform transform, Vector2 inputVector, float speedMove, float speedRotate)
    {
        Vector3 moveDiraction = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDiraction * Time.deltaTime * speedMove;

        transform.forward = Vector3.Slerp(transform.forward, moveDiraction, Time.deltaTime * speedRotate);
    }

    public void RandomPatrol(Transform transform, float patrolRadius, float speedMove, float speedRotate)
    {
        if (_timeSinceLastChange >= _changeDirectionInterval || Vector3.Distance(transform.position, _targetPoint) < _pointReachThreshold)
        {
            Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
            _targetPoint = new Vector3(randomPoint.x, transform.position.y, randomPoint.y);

            _timeSinceLastChange = 0f; 
        }

        _timeSinceLastChange += Time.deltaTime;

        Vector3 directionToTarget = (_targetPoint - transform.position).normalized;
        Vector2 inputVector = new Vector2(directionToTarget.x, directionToTarget.z);

        Movement(transform, inputVector, speedMove, speedRotate);
    }
}
