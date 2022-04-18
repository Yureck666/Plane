using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class ForseField : MonoBehaviour
{
    [SerializeField] private float deltaScale;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;

    private float _alpha;
    private Vector3 _scale;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();

        _scale = transform.localScale;
        _alpha = _spriteRenderer.color.a;
        Color color = _spriteRenderer.color;
        color.a = 0;
        _spriteRenderer.color = color;
        _circleCollider.enabled = false;
    }

    public void SetActive(bool active, float time = 0)
    {
        if (active)
        {
            transform.DOScale(_scale * deltaScale, time);
            _spriteRenderer.DOFade(_alpha, 0.2f);
        }
        else
        {
            _spriteRenderer.DOFade(0, 0.2f);
            transform.localScale = _scale;
        }
        _circleCollider.enabled = active;
    }
}
