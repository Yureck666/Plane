using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMoveController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    
    private Transform _plane;
    private bool _inited;
    private Rigidbody2D _rigidbody;

    public void Init(Plane plane)
    {
        _plane = plane.transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _inited = true;
    }

    private void Update()
    {
        if (_inited)
        {
            _rigidbody.velocity = transform.up * speed;
            Vector2 new_rotation = _plane.position - transform.position;
            transform.up = Vector2.MoveTowards(transform.up, new_rotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void Stop()
    {
        _inited = false;
        _rigidbody.velocity = Vector2.zero;
    }
}
