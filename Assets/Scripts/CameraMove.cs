using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform plane;
    private void Update()
    {
        Vector3 campos = transform.position;
        campos.x = plane.position.x;
        campos.y = plane.position.y;
        transform.position = campos;
    }
}
