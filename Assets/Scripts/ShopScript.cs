using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private Darkness darkness;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private int[] bgCost;
    [SerializeField] private Image showBackground;
    [SerializeField] private Button selectButton;
    [SerializeField] private Text selectButtonText;
    [SerializeField] private Text moneyText;

    private int _bg;
    private bool _buttonStatus;
    
    private void Awake()
    {
        RefreshMoney();
        _bg = PlayerPrefs.GetInt("BG");
        CheckButtonStatus();
        showBackground.sprite = backgrounds[_bg];
    }

    private void Start()
    {
        darkness.SetActive(false);
    }
    
    public void ChangeBackground(bool right)
    {
        if (right)
        {
            _bg += 1;
            if (_bg > backgrounds.Length - 1)
            {
                _bg = 0;
            }
        }
        else
        {
            _bg -= 1;
            if (_bg < 0)
            {
                _bg = backgrounds.Length - 1;
            }
        }

        showBackground.sprite = backgrounds[_bg];
        CheckButtonStatus();
    }

    private void RefreshMoney()
    {
        moneyText.text = Convert.ToString(PlayerPrefs.GetInt("Money"));
    }

    public void SelectButton()
    {
        if (_buttonStatus)
        {
            PlayerPrefs.SetInt("BG", _bg);
            showBackground.sprite = backgrounds[_bg];
        }
        else
        {
            int money = PlayerPrefs.GetInt("Money");
            int cost = PlayerPrefs.GetInt(backgrounds[_bg].name);
            if (money >= cost)
            {
                PlayerPrefs.SetInt("Money", money - cost);
                PlayerPrefs.SetInt(backgrounds[_bg].name, 0);
            }
        }
        CheckButtonStatus();
        RefreshMoney();
    }

    private void CheckButtonStatus()
    {
        if (!PlayerPrefs.HasKey(backgrounds[_bg].name))
        {
            PlayerPrefs.SetInt(backgrounds[_bg].name, bgCost[_bg]);
        }
        
        if (_bg == PlayerPrefs.GetInt("BG"))
        {
            _buttonStatus = true;
            selectButton.interactable = false;
            selectButtonText.text = "Selected";
        }
        else if (PlayerPrefs.GetInt(backgrounds[_bg].name) == 0)
        {
            _buttonStatus = true;
            selectButton.interactable = true;
            selectButtonText.text = "Select";
        }
        else
        {
            _buttonStatus = false;
            selectButton.interactable = true;
            selectButtonText.text = Convert.ToString(PlayerPrefs.GetInt(backgrounds[_bg].name));
        }
    }

    public void OpenMenu()
    {
        StartCoroutine(LoadSceneEnumerator(0));
    }

    private IEnumerator LoadSceneEnumerator(int scene)
    {
        darkness.SetActive(true);
        yield return new WaitUntil(() => darkness.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(scene);
    }
}
