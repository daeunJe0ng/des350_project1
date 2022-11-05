using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject player;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    public GameObject loseScreenPanel;
    public GameObject winScreenPanel;
    public TextMeshProUGUI TimerText;
    public float timer;
    private int minutes, seconds;

    private AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;

    private bool isWinTriggered;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        timer += Time.deltaTime;

        minutes = Mathf.FloorToInt(timer / 60f);
        seconds = Mathf.FloorToInt(timer - minutes * 60f);

        TimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (minutes >= 3)
        {
            if (!isWinTriggered)
            {
                Win();
            }
        }

        UpdatePlayerHealthUI();
    }

    public void Win()
    {
        audioSource.clip = winClip;
        audioSource.Play();

        winScreenPanel.SetActive(true);
        Time.timeScale = 0;

        isWinTriggered = true;
    }

    public void Lose()
    {
        audioSource.clip = loseClip;
        audioSource.Play();

        loseScreenPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void UpdatePlayerHealthUI()
    {
        if (player != null)
        {
            switch (player.GetComponent<PlayerController>().healthPoint)
            {
                case 0:
                    heart1.SetActive(false);
                    break;
                case 1:
                    heart2.SetActive(false);
                    break;
                case 2:
                    heart3.SetActive(false);
                    break;
                case 3:
                    heart4.SetActive(false);
                    break;
                case 4:
                    heart5.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}