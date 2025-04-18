using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PlayerInput _input;

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        _rigidBody.linearVelocity = _input.MoveDir * _config.MoveSpeed;
    }
}
