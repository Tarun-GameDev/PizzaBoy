using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playingMenu;
    [SerializeField] GameObject levelCompleteMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI levelNoText;
    [SerializeField] TextMeshProUGUI gemsText;
    [SerializeField] int gemsCollected;
    public static bool GameIsPaused = false;
    AudioManager audioManager;

    private void Start()
    {
        gemsCollected = PlayerPrefs.GetInt("Gems", 0);
        gemsText.text = "Coins " + gemsCollected.ToString("00");
        levelNoText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        audioManager = AudioManager.instance;
    }

    public void Resume()
    {
        playingMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioManager.Play("Click");
    }

    public void Pause()
    {
        playingMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioManager.Play("Click");
    }

    public void DeadMenu()
    {
        playingMenu.SetActive(false);
    }

    public void RestartButton()
    {
        audioManager.Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelComplete()
    {
        Invoke("levelCompleteDelay",1f);
    }

    void levelCompleteDelay()
    {
        levelCompleteMenu.SetActive(true);
        playingMenu.SetActive(false);
    }

    public void NextLevel()
    {
        audioManager.Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddCoin(int _coins)
    {
        gemsCollected += _coins;
        gemsText.text = "Gems " + gemsCollected.ToString("00");
        PlayerPrefs.SetInt("Gems", gemsCollected);
    }
     
}
