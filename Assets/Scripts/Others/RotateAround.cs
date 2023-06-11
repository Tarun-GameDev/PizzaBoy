using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Vector3 rotateAxis;
    [SerializeField] float rotationSpeed = 30f;

    public float moveDistance = 1f;
    public float moveSpeed = 1f;     

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(rotateAxis * rotationSpeed * Time.deltaTime);



        float newY = startingPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // Apply the new position to the object
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

