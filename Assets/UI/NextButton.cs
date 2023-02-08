using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
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

    public void buttonCallBack()
    {
        GM3 gm3 = new GM3();
        SaveSystem.SaveState();
        buttonSwitch();
        //UnityEngine.Debug.Log("You have clicked the button!");
        if (phase == 1)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase2();
            GM1.enq1("Draw cards - (Automatic)");
            GM1.deq1(1);
            GM1.enq2("Go to phase 3 - (Any player)");
            
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
            switch (turn)
            {
                case 2:
                    if (!GM2.boolStates[5])
                    {
                        gm3.HIS009();
                    }
                    break;
                case 3:
                    if (!GM2.boolStates[3])
                    {
                        gm3.HIS010();
                    }
                    break;
                case 4:
                    if (!GM2.boolStates[6])
                    {
                        //gm3.HIS013();
                    }
                    if (!GM2.boolStates[7])
                    {
                        gm3.HIS014();
                    }
                    break;
                case 6:
                    if (!GM2.boolStates[8])
                    {
                        //gm3.HIS015();
                    }
                    break;
                default:
                    break;
            }
            phase++;
            segment = 1;
            GM2.onChangePhase();
            GM2.onPhase8(); 
        }
        else if (phase == 8)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase9();
        }
        else if (phase == 9)
        {
            turn++;
            phase=2;
            segment = 1;
            //reset the following bool in boolStates
            int[] resetBool = new int[14] {16, 17, 18, 19, 20, 21, 22, 23, 24, 29, 34, 35, 49, 59};
            int[] resetInt = new int[8] { 0, 1, 2, 3, 4, 8, 9, 10 };
            Array.Clear(GM1.skipped, 0, 6);
            foreach(int r in resetBool)
            {
                GM2.boolStates[r] = false;
            }
            foreach(int r in resetInt)
            {
                GM2.intStates[r] = -1;
            }
            GM2.onChangePhase();
            GM2.onPhase2();
            GM1.enq1("Draw cards - (Automatic)");
            GM1.deq1(1);
            GM1.enq2("Go to phase 3 - (Any player)");
        }
        

    }

    public void buttonSwitch()
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
