using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Darkness darkness;
    [SerializeField] private Text muteButtonText;
    [SerializeField] private CanvasGroup ppCanvasGroup;
    [SerializeField] private Image ppButtonImage;
    
    private void Awake()
    {
        ppCanvasGroup.alpha = 0;
        ppButtonImage.raycastTarget = false;
    }
    void Start()
    {
        darkness.SetActive(false);
        if (!PlayerPrefs.HasKey("Mute"))
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
        if (!PlayerPrefs.HasKey("BG"))
        {
            PlayerPrefs.SetInt("BG", 0);
        }
        if (!PlayerPrefs.HasKey("Bombs"))
        {
            PlayerPrefs.SetInt("Bombs", 0);
        }
        CheckMute();
    }

    public void SetActivePP(bool active)
    {
        if (active)
        {
            ppCanvasGroup.DOFade(1, 0.5f);
            ppButtonImage.raycastTarget = true;
        }
        else
        {
            ppCanvasGroup.DOFade(0, 0.5f);
            ppButtonImage.raycastTarget = false;
        }
    }

    private void CheckMute()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            muteButtonText.text = "Unmute";
        }
        else
        {
            muteButtonText.text = "Mute";
        }
    }

    public void MuteClick()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        CheckMute();
    }
    
    public void StartGame()
    {
        StartCoroutine(LoadSceneEnumerator(2));
    }

    public void OpenShop()
    {
        StartCoroutine(LoadSceneEnumerator(1));
    }

    private IEnumerator LoadSceneEnumerator(int scene)
    {
        darkness.SetActive(true);
        yield return new WaitUntil(() => darkness.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(scene);
    }
}
