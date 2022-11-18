using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DeckScript;

public class EmptyCard : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        if (GM1.phase != 1 && GM1.phase != 4 && GM1.phase != 6 && GM1.phase!=2)
        {
            return;
        }
        if (GM1.phase == 4)//non-participates of Diet of Worms
        {
            if (GM1.player == 0 || GM1.player == 2 || GM1.player == 3)
            {
                return;
            }
            if (GM1.player == 1 && GM2.secretCP[1]!=0|| GM1.player == 4 && GM2.secretCP[4] != 0|| GM1.player == 5 && GM2.secretCP[5] != 0)//participates can only submit once
            {
                return;
            }
            ConfirmScript.btn.interactable = false;

        }
        else
        {
            ConfirmScript.cardSelected = eventData.pointerCurrentRaycast.gameObject.name;
            ConfirmScript.btn.interactable = true;
        }

        
        int index = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(4)) - 1;
        UnityEngine.Debug.Log(index);
        UnityEngine.Debug.Log(cardsLib.Count());
        if ((int)cardsLib.ElementAt(index).cardType != 1)
        {
            CPButtonScript.cardSelected = eventData.pointerCurrentRaycast.gameObject.name;
            CPButtonScript.cardTag = eventData.pointerCurrentRaycast.gameObject.tag;
            CPButtonScript.btn.interactable = true;
        }
        else
        {
            CPButtonScript.btn.interactable = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
