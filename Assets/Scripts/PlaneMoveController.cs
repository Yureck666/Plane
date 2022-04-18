using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class PlaneMoveController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float multiplyRotationSpeed;

    private Rigidbody2D _rigidbody;
    private bool _isPlaying;
    private Vector3 _scale;
    private void Awake()
    {
        _scale = transform.localScale;
        _isPlaying = true;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _rigidbody.velocity = transform.up * speed;
        }
    }

    public void Rotate(float x)
    {
        if (_isPlaying)
        {
            x = x * multiplyRotationSpeed;
            if (x > maxRotationSpeed)
            {
                x = maxRotationSpeed;
            }
            else if (x < -maxRotationSpeed)
            {
                x = -maxRotationSpeed;
            }
            _rigidbody.angularVelocity = x;
        }
    }

    public void StopRotate()
    {
        _rigidbody.angularVelocity = 0;
        transform.DOScale(_scale, 0.2f);
    }

    public void Stop()
    {
        _isPlaying = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0;
    }
}
