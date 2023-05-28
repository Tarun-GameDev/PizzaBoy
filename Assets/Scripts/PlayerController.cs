using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 100f;
    Rigidbody rb;
    Collider col;
    [SerializeField] float movement;
    [SerializeField] Transform pizzaBoxPivot;
    [SerializeField] int boxesCollected = 0;
    [SerializeField] Rig characterRig;
    [SerializeField] Animator animator;
    [SerializeField] float xPos = -2f;
    [SerializeField] float sideMoveSpeed = 1f;
    [SerializeField] bool right = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        if(characterRig != null)
            characterRig.weight = 0f;
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (movement > .1f && !right)
        {
            xPos = 2f;
            animator.Play("MoveSide-R");
            right = true;
        }
        else if (movement < -.1f && right)
        {
            xPos = -2f;
            animator.Play("MoveSide-L");
            right = false;
        }

        rb.position = Vector3.Lerp(rb.position, new Vector3(xPos, rb.position.y, rb.position.z), Time.deltaTime * sideMoveSpeed);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PizzaBox"))
        {
            if (characterRig != null)
                characterRig.weight = 1f;
            other.transform.SetParent(pizzaBoxPivot);
            other.transform.localPosition = new Vector3(0f, boxesCollected * .2f, 0f);
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            other.GetComponent<RotateAround>().enabled = false;

            boxesCollected++;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(0f, rb.position.y, moveSpeed * Time.fixedDeltaTime);
    }
}
