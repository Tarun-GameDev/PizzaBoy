using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HouseDelivrayPosTrigger : MonoBehaviour
{
    [SerializeField] int noOfBoxes = 0;
    [SerializeField] bool boy = false;
    [SerializeField] Rig characterRig;
    [SerializeField] bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CollectedBox"))
        {
            other.GetComponent<PizzaBox>().delivered = true;
            other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (noOfBoxes * .15f), this.transform.position.z);
            other.tag = "Untagged";
            noOfBoxes++;

            if (boy && !collected)
            {
                if (characterRig != null)
                    characterRig.weight = 1f;
                collected = true;
            }
        }
    }
}
