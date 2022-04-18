using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScorePoint : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    
    private Text _text;
    
    public void Init(string text)
    {
        _text = GetComponent<Text>();
        _text.text = text;
        _text.DOFade(0, lifeTime);
    }

    public void AddLifeTime(string text)
    {
        _text.text = text;
        _text.DOFade(1, 0.1f).OnComplete(() => _text.DOFade(0, lifeTime));
    }
}
