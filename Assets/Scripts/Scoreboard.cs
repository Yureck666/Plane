using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private float SwitchTime;
    [SerializeField] private Text score, best;
    [SerializeField] private Image menuButton;

    private CanvasGroup _canvasGroup;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        menuButton.raycastTarget = false;
    }

    public void Refresh(int i)
    {
        score.text = Convert.ToString(i);
        best.text = Convert.ToString(PlayerPrefs.GetInt("Best"));
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            _canvasGroup.DOFade(1, SwitchTime);
            menuButton.raycastTarget = true;
        }
        else
        {
            _canvasGroup.DOFade(0, SwitchTime);
            menuButton.raycastTarget = false;
        }
    }

    public void OpenMenu()
    {
        _canvasGroup.DOFade(0, SwitchTime).OnComplete(() => SceneManager.LoadScene(0));
    }
}
