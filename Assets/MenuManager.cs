using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public GameObject howToPlayPanel;

    public void GameStart()
    {
        SceneManager.LoadScene("Level");
    }

    public void ShowHowToPlay()
    {
        titleScreenPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void HideHowToPlay()
    {
        titleScreenPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
