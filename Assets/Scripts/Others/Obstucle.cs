using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstucle : MonoBehaviour
{

    public int height = 7;
    bool collided = false;

    private void Start()
    {
        switch (height)
        {
            case 7:
                transform.localScale = new Vector3(transform.localScale.x, 3f, transform.localScale.z);
                break;
            case 10:
                transform.localScale = new Vector3(transform.localScale.x, 2.3f, transform.localScale.z);
                break;
            case 15:
                transform.localScale = new Vector3(transform.localScale.x, 1.3f, transform.localScale.z);
                break;
            case 20:
                transform.localScale = new Vector3(transform.localScale.x, .4f, transform.localScale.z);
                break;
            default:
                transform.localScale = new Vector3(transform.localScale.x, .4f, transform.localScale.z);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collided)
            return;

        var gameobj = other.gameObject;

        if (gameobj.CompareTag("CollectedBox"))
        {
            LevelManager.instance.player.EnbaleBoxPhysics(height);
            collided = true;
        }
    }
}
