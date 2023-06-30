using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerToFinish : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Animator timerTextAnimator;
    [SerializeField] GameObject levelFailedUI;
    [SerializeField] GameObject TimeOutText;
    [SerializeField] int min;
    [SerializeField] int sec;
    [SerializeField] float startTime;
    public bool levelCompleted = false;
    [SerializeField] bool levelFailed = false;
    [SerializeField] AudioSource CountDownAudio;
    bool countdown = false;
    
   
    void Update()
    {
        if (startTime <= 0f || levelCompleted || levelFailed)
            return;

        if(startTime <= 5f && !countdown)
        {
            if(CountDownAudio!= null)
                CountDownAudio.Play();
            timerText.color = Color.red;
            if (timerTextAnimator != null)
                timerTextAnimator.SetTrigger("CountDown");
            countdown = true;
        }

        startTime -= Time.deltaTime;
        sec = (int)(startTime % 60);
        min = (int)(startTime / 60 % 60);

        timerText.text = string.Format("{0:00}:{1:00}", min, sec);

        if(startTime <= 0f)
        {
            levelFailed = true;
            LevelFailed();
            if (CountDownAudio != null)
                CountDownAudio.Stop();
        }
            
    }

    void LevelFailed()
    {
        if (!LevelManager.instance.player.levelCompleted)
        {
            LevelManager.instance.player.RemoveAllPizzas();
            if (levelFailedUI != null)
                levelFailedUI.SetActive(true);
            if (TimeOutText != null)
                TimeOutText.SetActive(true);
        }

    }
}
