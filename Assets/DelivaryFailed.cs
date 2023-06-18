using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelivaryFailed : MonoBehaviour
{
    [SerializeField] Animator emojiesAnimator;
    [SerializeField] Image emojiesImg;
    [SerializeField] Sprite sadEmoji;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered && other.CompareTag("Player"))
        {
            emojiesAnimator.SetTrigger("EmojiChange");
            emojiesImg.sprite = sadEmoji;
            triggered = true;
        }
    }
}
