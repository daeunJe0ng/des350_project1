using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public GameObject howToPlayPanel;
    public AudioClip startButtonSFX;
    public AudioClip defaultButtonSFX;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        audioSource.clip = startButtonSFX;
        audioSource.Play();
        SceneManager.LoadScene("Level");
    }

    public void ShowHowToPlay()
    {
        audioSource.clip = defaultButtonSFX;
        audioSource.Play();
        titleScreenPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void HideHowToPlay()
    {
        audioSource.clip = defaultButtonSFX;
        audioSource.Play();
        titleScreenPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
    }

    public void Quit()
    {
        audioSource.clip = defaultButtonSFX;
        audioSource.Play();
        Application.Quit();
    }
}
