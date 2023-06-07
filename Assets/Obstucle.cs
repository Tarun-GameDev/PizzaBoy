using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstucle : MonoBehaviour
{

    public int height = 5;
    bool collided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collided)
            return;

        var gameobj = other.gameObject;

        if (gameobj.CompareTag("CollectedBox"))
        {
            SceneManager.instance.player.EnbaleBoxPhysics(height);
            collided = true;
        }
    }
}
