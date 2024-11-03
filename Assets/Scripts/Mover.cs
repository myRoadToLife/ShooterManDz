using UnityEngine;

public class Mover
{
    public void Movement(Transform transform, Vector2 inputVector, float speedMove, float speedRotate)
    {
        Vector3 moveDiraction = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDiraction * Time.deltaTime * speedMove;

        transform.forward = Vector3.Slerp(transform.forward, moveDiraction, Time.deltaTime * speedRotate);
    }
}
