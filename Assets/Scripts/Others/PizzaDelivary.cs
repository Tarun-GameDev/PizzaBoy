using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaDelivary : MonoBehaviour
{
    public int noOfPizzas = 1;
    public GameObject delivaryPos;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().PizzaDelivary(delivaryPos, noOfPizzas);
            triggered = true;
        }
    }
}
