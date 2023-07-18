using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("LevelUnlocked", SceneManager.GetActiveScene().buildIndex + 1);
            LevelManager.instance.player.LevelCompleted();
            if (particleEffect != null)
                particleEffect.Play();

            AudioManager.instance.Play("LevelCompleted");
        }
    }
}
