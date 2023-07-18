using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] TextMeshProUGUI levelNoText;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (levelNoText != null)
            levelNoText.text = "Level:" + (PlayerPrefs.GetInt("LevelUnlocked", 1)-1).ToString();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelUnlocked", 1));
    }
}
