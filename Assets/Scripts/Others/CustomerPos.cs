using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPos : MonoBehaviour
{
    [SerializeField] int noOfBoxes = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectedBox"))
        {

            other.GetComponent<PizzaBox>().delivered = true;
            other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (noOfBoxes * 5), this.transform.position.z);
            other.tag = "Untagged";
        }
    }
}
