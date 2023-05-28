using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathObstucle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mathText;
    [SerializeField] string mathType = "Add";
    [SerializeField] int number = 0;

    private void Start()
    {
        switch (mathType)
        {
            case "Add":
                {

                }
                break;
            case "Subtract":
                {

                }
                break;
            case "Multiply":
                {

                }
                break;
            case "Divide":
                {

                }
                break;
            default:
                break;
        }
    }


}
