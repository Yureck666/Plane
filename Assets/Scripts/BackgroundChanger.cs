using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Color berserkColor;

    private SpriteRenderer _spriteRenderer;
    private Color _color;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;

        _spriteRenderer.sprite = backgrounds[PlayerPrefs.GetInt("BG")];
    }

    public void BerserkMode(bool active)
    {
        if (active)
        {
            _spriteRenderer.DOColor(berserkColor, 0.5f);
        }
        else
        {
            _spriteRenderer.DOColor(_color, 0.5f);
        }
    }
}
