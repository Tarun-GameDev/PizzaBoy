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
                    mathName = "Add";
                }
                break;
            case "-":
                {
                    mathName = "Subtract";
                }
                break;
            case "*":
                {
                    mathName = "Multiply";
                }
                break;
            case "/":
                {
                    mathName = "Divide";
                }
                break;
        }

        mathText.text = mathName + " by " + amount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            MathsPizza();
    }

    void MathsPizza()
    {
        switch (mathSymbol)
        {
            case "+":
                {
                    SceneManager.instance.player.AddBox(amount);
                }
                break;
            case "-":
                {
                    SceneManager.instance.player.RemoveBox(amount);
                }
                break;
            case "*":
                {
                    SceneManager.instance.player.AddBox(SceneManager.instance.currentPizzasCollected * amount);
                }
                break;
            case "/":
                {
                    int val = SceneManager.instance.currentPizzasCollected / amount;
                    SceneManager.instance.player.RemoveBox(SceneManager.instance.currentPizzasCollected - val);
                }
                break;
        }
    }


}
