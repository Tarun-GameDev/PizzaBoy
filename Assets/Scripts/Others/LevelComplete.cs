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
            LevelManager.instance.player.LevelCompleted();
            if (particleEffect != null)
                particleEffect.Play();
            
            
        }
    }
}
