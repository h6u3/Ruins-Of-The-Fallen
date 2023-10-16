using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{

    public float cooldownTime = 10;
    public float timeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;

    public CanvasGroup canvasGroup;

    public FloraManager floraManager;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = cooldownTime;
        timerIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
       checkUI();

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                floraManager.ResetPlant();
                timerIsRunning = false;
            }
        }
        else {
            checkFlora();
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void checkFlora() {
        if (floraManager.cooldown_running) {
            timerIsRunning = true;
            timeRemaining = cooldownTime;
        }
        else {
            timerIsRunning = false;
        }
    }

    private void checkUI() {
        if (floraManager.showUI && floraManager.cooldown_running) {
            Show();
        }
        else {
            Hide();
        }
    }

    public void Show() {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void Hide() {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }


}
