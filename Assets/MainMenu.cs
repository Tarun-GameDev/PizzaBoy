using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        if(PlayerPrefs.GetInt("LevelsUnlocked", 0) != 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelsUnlocked", 0));
        }

    }
}
