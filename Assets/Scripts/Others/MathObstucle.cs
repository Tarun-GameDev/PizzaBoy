using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathObstucle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mathText;
    [SerializeField] string mathSymbol = "+";
    [SerializeField] int amount = 0;
    string mathName;

    private void Start()
    {
        switch (mathSymbol)
        {
            case "+":
                {
                    mathName = "+";
                }
                break;
            case "-":
                {
                    mathName = "-";
                }
                break;
            case "*":
                {
                    mathName = "x";
                }
                break;
            case "/":
                {
                    mathName = "÷";
                }
                break;
        }

        mathText.text = mathName + amount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MathsPizza();
            CinemachineShake.instance.CameraShake(3f, .4f);
        }
    }

    void MathsPizza()
    {
        switch (mathSymbol)
        {
            case "+":
                {
                    LevelManager.instance.player.AddBox(amount);
                    AudioManager.instance.Play("BoxesAdded");
                }
                break;
            case "-":
                {
                    LevelManager.instance.player.RemoveBox(amount);
                    AudioManager.instance.Play("BoxesSubtract");
                }
                break;
            case "*":
                {
                    LevelManager.instance.player.AddBox(LevelManager.instance.currentPizzasCollected * amount);
                    AudioManager.instance.Play("BoxesAdded");
                }
                break;
            case "/":
                {
                    int val = LevelManager.instance.currentPizzasCollected / amount;
                    LevelManager.instance.player.RemoveBox(LevelManager.instance.currentPizzasCollected - val);
                    AudioManager.instance.Play("BoxesSubtract");
                }
                break;
        }
    }


}
