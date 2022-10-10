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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            levelUpText.SetText("Damage up!");
        }
        else
        {
            levelUpText.SetText("One more bullet!");
        }

        yield return new WaitForSeconds(1.0f);

        levelUpText.enabled = false;
    }
}
