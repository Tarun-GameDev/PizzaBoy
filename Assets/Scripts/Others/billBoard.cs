using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billBoard : MonoBehaviour
{
    public Transform Cam;

    private void Start()
    {
        Cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (Cam != null)
            transform.LookAt(transform.position + Cam.forward);
    }
}
