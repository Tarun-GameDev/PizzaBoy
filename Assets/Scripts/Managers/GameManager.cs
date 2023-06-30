using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int coins;
    [SerializeField] Material groundMat;
    [Header("(.2,1) " +
        " (1,.5)")]
    [SerializeField]
    List<Vector2> matOffset = new List<Vector2>();

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Gems", 0);
        if (groundMat != null)
        {
            groundMat.mainTextureOffset = matOffset[Random.Range(0,matOffset.Count)];
        }
            
    }
}
