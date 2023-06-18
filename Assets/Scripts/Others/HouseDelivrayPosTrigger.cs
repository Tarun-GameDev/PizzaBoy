using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class HouseDelivrayPosTrigger : MonoBehaviour
{
    [SerializeField] int noOfBoxes = 0;
    [SerializeField] bool boy = false;
    [SerializeField] Rig characterRig;
    [SerializeField] bool collected = false;

    [SerializeField] Image emojiesImage;
    [SerializeField] Sprite angrySprite;
    [SerializeField] Sprite happySprite;
    [SerializeField] Animator emojiesAnimator;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CollectedBox"))
        {
            other.GetComponent<PizzaBox>().delivered = true;
            other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (noOfBoxes * .15f), this.transform.position.z);
            other.tag = "Untagged";
            noOfBoxes++;

            if (boy && !collected)
            {
                if (characterRig != null)
                    characterRig.weight = 1f;
                collected = true;
            }

            if(!triggered)
            {
                if (emojiesImage != null)
                    emojiesImage.sprite = happySprite;

                if (emojiesAnimator != null)
                    emojiesAnimator.SetTrigger("EmojiChange");
            }

            triggered = true;
        }
    }
}
