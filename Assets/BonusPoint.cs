using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoint : MonoBehaviour
{
    bool bonused = false;
    [SerializeField] int bonusCoins = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (bonused)
            return;

        if(other.CompareTag("Player"))
        {
            LevelManager.instance.uiManager.AddCoin(bonusCoins);
            LevelManager.instance.player.RemoveBox(bonusCoins);
            LevelManager.instance.player.LevelCompleted();
        }
    }
}
