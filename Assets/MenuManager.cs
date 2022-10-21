using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public GameObject howToPlayPanel;
    public AudioSource startButtonSFX;
    public AudioSource howToPlayButtonSFX;
    public AudioSource BackButtonSFX;

    public void GameStart()
    {
        startButtonSFX.Play();
        SceneManager.LoadScene("Level");
    }

    public void ShowHowToPlay()
    {
        howToPlayButtonSFX.Play();
        titleScreenPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void HideHowToPlay()
    {
        BackButtonSFX.Play();
        titleScreenPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
    }

    public void Quit()
    {
        howToPlayButtonSFX.Play();
        Application.Quit();
    }
}
