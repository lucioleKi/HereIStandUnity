using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using static EnumSpaceScript;
using static DeckScript;

public class EmptyCard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    bool hover = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        int index = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(4)) - 1;
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
        else if(playAsEvent(index))
        {
            ConfirmScript.cardSelected = eventData.pointerCurrentRaycast.gameObject.name;
            ConfirmScript.btn.interactable = true;
        }

        
        
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

        if (GM2.chosenCard == "HIS-004" && GM1.player == 3 && GM2.boolStates[0])
        {
            CPButtonScript.btn.interactable = false;
            ConfirmScript.btn.interactable = false;
            OtherButtonScript otherButtonScript = GameObject.Find("OtherButton").GetComponent("OtherButtonScript") as OtherButtonScript;
            otherButtonScript.cardTag = eventData.pointerCurrentRaycast.gameObject.tag;
            otherButtonScript.btn.interactable = true;
        }
        else
        {
            OtherButtonScript otherButtonScript = GameObject.Find("OtherButton").GetComponent("OtherButtonScript") as OtherButtonScript;
            otherButtonScript.btn.interactable = false;
        }


    }

    bool playAsEvent(int index)
    {
        if (index == 64)
        {
            if (GM1.player == 5 && DeckScript.debaters.ElementAt(12).status == (DebaterStatus)1)
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hover)
        {
            gameObject.transform.SetSiblingIndex(10);
            gameObject.transform.localScale = new Vector3(2, 2, 1);
            
            
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }
}
