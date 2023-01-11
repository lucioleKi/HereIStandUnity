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
    public bool inFieldBattle;
    bool hover = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        int index = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(4)) - 1;
        if (GM1.phase == 3 && GM1.segment != 6 || GM1.phase == 5 || (GM1.phase == 6 && GM1.impulse != GM1.player || GM1.skipped[GM1.player]))
        {
            return;
        }
        if (GM1.phase == 4)//non-participates of Diet of Worms
        {
            if (GM1.player == 0 || GM1.player == 2 || GM1.player == 3)
            {
                return;
            }
            if (GM1.player == 1 && GM2.secretCP[1] != 0 || GM1.player == 4 && GM2.secretCP[4] != 0 || GM1.player == 5 && GM2.secretCP[5] != 0)//participates can only submit once
            {
                return;
            }
            ConfirmScript.btn.interactable = false;

        }
        else if (playAsEvent(index+1)&&GM1.phase != 5)
        {
            ConfirmScript.cardSelected = eventData.pointerCurrentRaycast.gameObject.name;
            ConfirmScript.btn.interactable = true;
        }else if(!playAsEvent(index+1)|| GM1.segment == 6 && GM1.phase == 5)
        {
            ConfirmScript.btn.interactable = false;
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
        //in land mvmt procedure
        if (GM2.boolStates[28] || GM2.boolStates[30] || GM2.boolStates[31])
        {
            CPButtonScript.btn.interactable = false;
        }

        //other button
        OtherButtonScript otherButtonScript = GameObject.Find("OtherButton").GetComponent("OtherButtonScript") as OtherButtonScript;
        switch (otherButtonScript.btnStatus)
        {
            case -1:

                otherButtonScript.btn.interactable = false;
                break;
            case 1:
                if (GM2.chosenCard == "HIS-004" && GM1.player == 3 && GM2.boolStates[0])
                {
                    CPButtonScript.btn.interactable = false;
                    ConfirmScript.btn.interactable = false;

                    otherButtonScript.cardTag = eventData.pointerCurrentRaycast.gameObject.tag;
                    otherButtonScript.btn.interactable = true;
                }
                else
                {

                    otherButtonScript.btn.interactable = false;
                }
                break;
            case 2:
                if (GM2.chosenCard == "HIS-112" && (GM1.player == 2 || GM1.player == 5))
                {
                    CPButtonScript.btn.interactable = false;
                    ConfirmScript.btn.interactable = false;

                    otherButtonScript.cardTag = eventData.pointerCurrentRaycast.gameObject.tag;
                    otherButtonScript.btn.interactable = true;
                }
                else
                {

                    otherButtonScript.btn.interactable = false;
                }
                break;
        }



    }

    

    bool playAsEvent(int index)
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        //combat cards
        if ((int)cardsLib.ElementAt(index).cardType == 3) { 
            if (landMvmt.status!=11&&landMvmt.status!=10)
        
            
            {
                return false;
            }
        }
        if((int)cardsLib.ElementAt(index).cardType != 3&&(landMvmt.status == 10 || landMvmt.status == 11))
        {
            return false;
        }
        if (index == 24 && index == 25)
        {
            if (landMvmt.status >= 10 && landMvmt.status <= 11)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        if (index == 26)
        {
            if (landMvmt.status >= 10 && landMvmt.status <= 11)
            {
                if (landMvmt.mvmtPlayer == 0 || landMvmt.fieldPlayer == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        if(index == 27||index==28)
        {
            return false;
        }
        if (index == 29)
        {
            if (landMvmt.status >= 10 && landMvmt.status <= 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if(index == 30)
        {
            if (landMvmt.status >= 10 && landMvmt.status <= 11)
            {
                if (landMvmt.mvmtPlayer == 1 || landMvmt.fieldPlayer == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        if (index == 31 || index == 32)
        {
            return false;
        }
        if (index == 34)
        {
            //automatically played in naval combat
            return false;
        }
        if (index == 35)
        {
            return false;
        }
        if (siegeScript.status == 4)
        {
            if (index == 33||index==36)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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
        if (index == 73)
        {
            //not playable by 0 or 5
            if (GM1.player == 0 || GM1.player == 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        if (index == 83)
        {
            //buda not under siege
            if (!spacesGM.ElementAt(115).sieged)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 84)
        {
            //HIS-009 has been played as an event
            if (GM2.boolStates[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 85)
        {
            if(DeckScript.debaters.ElementAt(12).status == (DebaterStatus)1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 89)
        {
            //HIS-009 has been played as an event
            if (GM2.boolStates[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 99)
        {
            if (!GM2.boolStates[48]&&(GM1.player == 1 && !GM2.boolStates[45] || GM1.player == 2 && !GM2.boolStates[46]|| GM1.player == 3 && !GM2.boolStates[47]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 101)
        {
            //playable by 1, 2, 3 if they haven't conquered
            if (GM1.player == 1 && !GM2.boolStates[22])
            {
                return true;
            }else if(GM1.player == 2 && !GM2.boolStates[23])
            {
                return true;
            }else if(GM1.player == 3 && !GM2.boolStates[24])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 100||index == 102)
        {
            //not playable by 5
            if (GM1.player == 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        if (index == 105)
        {
            //if there're sieged spaces
            List<int> pickSpaces = new List<int>();
            for (int i = 0; i < 134; i++)
            {
                if (spacesGM.ElementAt(i).sieged)
                {
                    pickSpaces.Add(i);
                }
            }
            if (pickSpaces.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 106)
        {
            //if there're merc on map
            List<int> pickSpaces = new List<int>();
            for (int i = 0; i < 134; i++)
            {
                if (spacesGM.ElementAt(i).merc > 0 && spacesGM.ElementAt(i).regularPower != GM1.player)
                {
                    pickSpaces.Add(i);
                }
            }
            if(pickSpaces.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (index == 109)
        {
            if (GM1.phase != 5)
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
        if (!eventData.fullyExited) return;
        hover = false;
    }
}
