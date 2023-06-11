using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playingMenu;
    [SerializeField] GameObject levelCompleteMenu;
    [SerializeField] TextMeshProUGUI levelNoText;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] int coinsCollected;

    private void Start()
    {
        coinsCollected = PlayerPrefs.GetInt("coins", 0);
        coinsText.text = "Coins " + coinsCollected.ToString("00");
        levelNoText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
    }

    public void DeadMenu()
    {
        playingMenu.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelComplete()
    {
        levelCompleteMenu.SetActive(true);
        playingMenu.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddCoin(int _coins)
    {
        coinsCollected += _coins;
        coinsText.text = "Coins " + coinsCollected.ToString("00");
        PlayerPrefs.SetInt("coins", coinsCollected);
    }
     
}
