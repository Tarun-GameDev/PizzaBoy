using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBox : MonoBehaviour
{ 
    [SerializeField] Rigidbody rb;
    [SerializeField] float startUpForce = 200f;
    GameObject pizzaPosTrigger;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] bool canMove = false;

    private void Start()
    {

        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    public void moveTowardsPos(GameObject _pizzaPosTrigger)
    {
        pizzaPosTrigger = _pizzaPosTrigger;
        rb.AddForce(transform.up * startUpForce * Time.deltaTime, ForceMode.VelocityChange);
        rb.isKinematic = true;
        canMove = true;

    }

    private void Update()
    {
        if (!canMove)
            return;

        if(pizzaPosTrigger != null)
        {
            transform.LookAt(pizzaPosTrigger.transform);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

}
