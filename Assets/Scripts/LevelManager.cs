using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float startDelay;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScorePoint scorePoint;
    [SerializeField] private BackgroundChanger[] _backgroundChanger;
    [SerializeField] private Plane plane;
    [SerializeField] private Bomb bomb;
    [SerializeField] private float bombSpawnDelay;
    [SerializeField] private Image circle;
    [SerializeField] private float berserkDuration;
    [SerializeField] private float circleTimeTick;
    [SerializeField] private float circleKillTick;
    [SerializeField] private Darkness darkness;

    public static UnityEvent<Vector3> BombBoomEvent;
    public static UnityEvent LoseGameEvent;

    private bool _gameIsActive;
    private bool _berserkMode;
    private float _bombSpawnTime;
    private int _score;
    private float _nextFieldFill;
    private int _lastBoomPoints;
    private float _lastBoomTime;
    private ScorePoint currentScorePoint;

    private void Awake()
    {
        _gameIsActive = true;
        _bombSpawnTime = Time.time + startDelay;
        _berserkMode = false;
        BombBoomEvent = new UnityEvent<Vector3>();
        LoseGameEvent = new UnityEvent();
        
        BombBoomEvent.AddListener(pos =>
        {
            AddScore(pos);
            FillCircle(circleKillTick);
        });
        
        LoseGameEvent.AddListener(() =>
        {
            DisableCircle();
            darkness.SetActive(true);
            SetBest();
            scoreboard.Refresh(_score);
            scoreboard.SetActive(true);
            _gameIsActive = false;
        });
        
        _score = 0;
    }

    private void Start()
    {
        darkness.SetActive(false);
    }

    private void Update()
    {
        if (Time.time > _bombSpawnTime)
        {
            SpawnBomb();
        }

        if (Time.time > _nextFieldFill)
        {
            FillCircle(circleTimeTick);
            _nextFieldFill = Time.time + 1;
        }
    }

    private void SetBest()
    {
        if (PlayerPrefs.GetInt("Best") < _score)
        {
            PlayerPrefs.SetInt("Best", _score);
        }
    }
    private void AddScore(Vector3 position)
    {
        if (_gameIsActive)
        {
            int points = 5;
            if (_lastBoomTime + 1 > Time.time)
            {
                points += _lastBoomPoints;
                currentScorePoint.AddLifeTime(Convert.ToString(points));
            }
            else
            {
                currentScorePoint = Instantiate(scorePoint.gameObject, position, Quaternion.identity, canvas.transform)
                    .GetComponent<ScorePoint>();
                currentScorePoint.Init(Convert.ToString(points));
            }

            _lastBoomTime = Time.time;
            _lastBoomPoints = points;
            _score += points;
        }
    }

    private void SpawnBomb()
    {
        if (_gameIsActive)
        {
            Vector3 spawnpos = plane.transform.position;
            if (Random.Range(0, 2) == 0)
            {
                spawnpos.x += Random.Range(300, 500) * 0.01f;
            }
            else
            {
                spawnpos.x += Random.Range(-500, -300) * 0.01f;
            }

            if (Random.Range(0, 2) == 0)
            {
                spawnpos.y += Random.Range(300, 500) * 0.01f;
            }
            else
            {
                spawnpos.y += Random.Range(-500, -300) * 0.01f;
            }

            var currentBomb = Instantiate(bomb.gameObject, spawnpos, Quaternion.identity).GetComponent<Bomb>();
            currentBomb.transform.LookAt(plane.transform);
            currentBomb.Init(plane);
            _bombSpawnTime = Time.time + bombSpawnDelay;
        }
    }

    private void FillCircle(float i)
    {
        if (circle.fillAmount == 0)
        {
            _berserkMode = false;
            plane.SetActiveForceField(false);
        }
        
        if (circle.fillAmount == 1)
        {
            _berserkMode = true;
            circle.DOFillAmount(0.01f, berserkDuration).OnComplete(() => circle.fillAmount = 0);
            plane.SetActiveForceField(true, berserkDuration);
            foreach (var backgroundChanger in _backgroundChanger)
            {
                backgroundChanger.BerserkMode(true);
            }
        }
        
        if ((circle.fillAmount < 1) && (!_berserkMode) && (circle.enabled))
        {
            circle.DOFillAmount(circle.fillAmount + i, 0.2f);
            foreach (var backgroundChanger in _backgroundChanger)
            {
                backgroundChanger.BerserkMode(false);
            }
        }
    }
    
    private void DisableCircle()
    {
        circle.DOFade(0, 0.2f).OnComplete(() => circle.enabled = false);
    }
}
