using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int coins;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins", 0);
    }
}
