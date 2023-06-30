using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [SerializeField] float speedRunMultiplier = 100f;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        LevelManager.instance.player.IncreaseSpeed(speedRunMultiplier);
        AudioManager.instance.Play("PowerUp");
        triggered = true;
        Destroy(gameObject);
    }

}
