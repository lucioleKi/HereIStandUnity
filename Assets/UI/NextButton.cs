using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;

public class NextButton : MonoBehaviour
{
    public Button btn;
    public int cardIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        cardIndex = 8;
        btn.interactable = false;
        btn.onClick.AddListener(()=>buttonCallBack());
        
    }

    void OnEnable()
    {
        GM2.onPhaseEnd += buttonSwitch;
    }

    void OnDisable()
    {
        GM2.onPhaseEnd -= buttonSwitch;
    }

    void buttonCallBack()
    {
        buttonSwitch();
        //UnityEngine.Debug.Log("You have clicked the button!");
        if (phase == 1)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase2();
            GM1.enq1("Any player to go to phase 3");
        }
        else if (phase == 2)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase3();
            TodoScript todoObject = GameObject.Find("TodoBox").GetComponent("TodoScript") as TodoScript;
            todoObject.put2();
        }
        else if (phase == 3 && turn == 1)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase4();
        }
        else if (phase == 3) {
            phase = phase + 2;
            GM2.onChangePhase();
            GM2.onPhase5();
        }
        else if (phase == 4)
        {
            CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
            currentTextObject.reset();
            phase++;
            GM2.onChangePhase();
            GM2.onPhase5();
        }
        else if (phase == 5)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase6();
        }
        else if (phase == 6)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase7();
        }else if (phase == 7)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase8(); 
        }
        else if (phase == 8)
        {
            phase++;
            GM2.onChangePhase();
        }
        /*else if (phase == 9)
        {
            turn++;
            phase=2;
            GM2.onChangePhase();
            GM2.onChangePhase();
            GM2.onPhase2();
        }
        */

    }

    void buttonSwitch()
    {
        if (btn.interactable)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
