using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event Action OnShoted;

    private float _xInput;
    private float _yInput;

    private Vector2 _inputVector;

    private void Update()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");

        _inputVector = new Vector2(_xInput, _yInput);

        if (Input.GetKeyDown(KeyCode.E))
            OnShoted?.Invoke();

    }

    public Vector2 GetMovementVectorNormalized()
    {
        _inputVector = _inputVector.normalized;

        return _inputVector;
    }
}
