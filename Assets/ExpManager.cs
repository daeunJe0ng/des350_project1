using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public PlayerController playerController;
    public Slider slider;
    public TMPro.TextMeshProUGUI levelNumberText;
    public TMPro.TextMeshProUGUI levelUpText;

    public float maxExp;
    public float updatedExp;

    private AudioSource audioSource;
    public AudioClip powerUpClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerController.healthPoint = 100;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            updatedExp += 100;
        }

        slider.value = updatedExp / maxExp;

        if (updatedExp >= maxExp)
        {
            playerController.upgradeNumber++;
            levelNumberText.SetText(playerController.upgradeNumber.ToString());

            StartCoroutine(ShowLevelUpText());

            updatedExp = 0;
            maxExp *= 1.35f;
        }
    }

    IEnumerator ShowLevelUpText()
    {
        levelUpText.enabled = true;

        if (playerController.upgradeNumber > 8)
        {
            audioSource.clip = powerUpClip;
            audioSource.Play();
            levelUpText.SetText("Damage up!");
        }
        else
        {
            audioSource.clip = powerUpClip;
            audioSource.Play();
            levelUpText.SetText("One more bullet!");
        }

        yield return new WaitForSeconds(1.0f);

        levelUpText.enabled = false;
    }
}
