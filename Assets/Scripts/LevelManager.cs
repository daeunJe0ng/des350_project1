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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        minutes = Mathf.FloorToInt(timer / 60f);
        seconds = Mathf.FloorToInt(timer - minutes * 60f);

        TimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        Win();
        LoseHealth();
    }

    public void Win()
    {
        if (minutes >= 3)
        {
            winScreenPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Lose()
    {
        loseScreenPanel.SetActive(true);
    }

    public void LoseHealth()
    {
        if(player != null)
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