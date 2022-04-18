using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Bomb>(out var bomb))
        {
            StartCoroutine(bomb.Destroy());
        }
        else if (col.gameObject.TryGetComponent<Plane>(out var plane))
        {
            StartCoroutine(this.GetComponent<Bomb>().Destroy());
            plane.Destroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<ForseField>(out _))
        {
            StartCoroutine(this.GetComponent<Bomb>().Destroy());
        }
    }
}
