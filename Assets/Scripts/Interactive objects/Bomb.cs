using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem boomParticleSystem;
    [SerializeField] private ParticleSystem trailParticleSystem;

    [SerializeField] private AudioSource trailSound;
    [SerializeField] private AudioSource boomSound;
    
    private BombMoveController _bombMoveController;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider;

    public void Init(Plane plane)
    {
        _bombMoveController = GetComponent<BombMoveController>();
        _bombMoveController.Init(plane);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnBecameInvisible()
    {
        if (_capsuleCollider == null)
        {
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
        }
        _capsuleCollider.enabled = false;
    }
    
    private void OnBecameVisible()
    {
        if (_capsuleCollider == null)
        {
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
        }
        _capsuleCollider.enabled = true;
    }

    public IEnumerator Destroy()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+1);
        boomSound.Play();
        trailSound.Stop();
        LevelManager.BombBoomEvent.Invoke(transform.position);
        _capsuleCollider.enabled = false;
        _spriteRenderer.DOFade(0, 0.2f);
        _bombMoveController.Stop();
        boomParticleSystem.Play();
        trailParticleSystem.Stop();
        yield return new WaitWhile(() => boomParticleSystem.isPlaying);
        Destroy(gameObject);
    }
}
