using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Darkness : MonoBehaviour
{
    [SerializeField] private float SwitchTime;
    private Image _image;
    void Awake()
    {
        _image = GetComponent<Image>();
        Color color = _image.color;
        color.a = 1;
        _image.color = color;
    }


    
    public void SetActive(bool active)
    {
        if (active)
        {
            _image.DOFade(1, SwitchTime);
        }
        else
        {
            _image.DOFade(0, SwitchTime);
        }
    }
}
