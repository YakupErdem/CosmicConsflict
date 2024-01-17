using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierController : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed;
    public float rotationValue;
    //
    private float baseY;
    private Rigidbody2D _rb2D;
    private Animator _animator;
    private bool _isCrashed;

    private void Start()
    {
        baseY = transform.position.y;
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameManager.instance.isGameStarted)
        {
            return;
        }
        Move();
        MoveFix();
        Rotate();
    }

    private void MoveFix()
    {
        _rb2D.velocity = Vector2.zero;
        if (transform.position.x > 2f)
        {
            transform.position = new Vector3(2f, transform.position.y, 0);
        }
        if (transform.position.x < -2f)
        {
            transform.position = new Vector3(-2f, transform.position.y, 0);
        }
    }

    private void Rotate()
    {
        if (SimpleInput.GetAxis("Horizontal") != 0)
        {
            if (!(transform.rotation.z < -rotationValue || transform.rotation.z > rotationValue))
            {
                _rb2D.MoveRotation(SimpleInput.GetAxis("Horizontal") * -rotationSpeed);
            }
        }
    }

    private void Move()
    {
        float x = Time.deltaTime * (moveSpeed * SimpleInput.GetAxis("Horizontal"));
        if (!(transform.position.x > 2f || transform.position.x < -2f))
        {
            transform.position += new Vector3(x, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
    }
}
