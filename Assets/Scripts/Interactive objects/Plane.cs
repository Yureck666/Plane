using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private ParticleSystem boomParticleSystem;
    [SerializeField] private ForseField forseField;
    [SerializeField] private AudioSource _trailSound;
    
    private AudioListener _audioListener;
    private SpriteRenderer _spriteRenderer;
    private PlaneMoveController _planeMoveController;
    private CapsuleCollider2D _capsuleCollider;

    private void Awake()
    {
        _audioListener = GetComponent<AudioListener>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _planeMoveController = GetComponent<PlaneMoveController>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void SetActiveForceField(bool active, float time = 0)
    {
        forseField.SetActive(active, time);
    }
    
    public void Destroy()
    {
        _capsuleCollider.enabled = false;
        boomParticleSystem.Play();
        _planeMoveController.Stop();
        _trailSound.Stop();
        _spriteRenderer.DOFade(0, 0.2f).OnComplete(() =>
        {
            LevelManager.LoseGameEvent.Invoke();
            _audioListener.enabled = false;
        });
    }
}
