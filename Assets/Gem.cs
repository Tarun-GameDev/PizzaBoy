using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] GameObject collectedParticEff;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("CollectedBox"))
        {
            if (collectedParticEff != null)
                Instantiate(collectedParticEff, transform.position, Quaternion.identity);

            LevelManager.instance.uiManager.AddCoin(1);


            Destroy(gameObject);
        }
    }
}
