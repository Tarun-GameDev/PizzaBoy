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
    [SerializeField] MathObstucle otherObstucle;
    public bool triggered = false;
    private void Start()
    {
        triggered = false;

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
            default:
                mathName = "+";
                break;
        }

        mathText.text = mathName + amount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            MathsPizza();
            CinemachineShake.instance.CameraShake(3f, .4f);
            triggered = true;
            if(otherObstucle != null)
                otherObstucle.triggered = true;
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
                    LevelManager.instance.player.AddBox(LevelManager.instance.currentPizzasCollected * (amount-1));
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
            default:
                LevelManager.instance.player.AddBox(amount);
                AudioManager.instance.Play("BoxesAdded");
                break;
        }
    }


}
