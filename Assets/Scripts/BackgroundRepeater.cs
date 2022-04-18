using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float _lengthX, _lengthY, _startPosX, _startPosY;
    void Start()
    {
        _startPosX = transform.position.x;
        _startPosY = transform.position.y;
        _lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        _lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        float tempX = (cam.transform.position.x * (1-parallaxEffect));
        float tempY = (cam.transform.position.y * (1-parallaxEffect));
        float distX = cam.transform.position.x * parallaxEffect;
        float distY = cam.transform.position.y * parallaxEffect;

        transform.position = new Vector3(_startPosX + distX, _startPosY + distY, transform.position.z);
        
        if (tempX > _startPosX + _lengthX) _startPosX += _lengthX;
        else if (tempX < _startPosX - _lengthX) _startPosX -= _lengthX;
        
        if (tempY > _startPosY + _lengthY) _startPosY += _lengthY;
        else if (tempY < _startPosY - _lengthY) _startPosY -= _lengthY;
    }
}
