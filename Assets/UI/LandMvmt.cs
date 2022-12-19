using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GraphUtils;
using static DeckScript;
using static GM2;

public class LandMvmt : MonoBehaviour
{
    public Button btn;
    public int mvmtPlayer;
    public int status;
    public int btnStatus;
    public int initial;
    public int destination;
    public bool hasLeader;
    public int lockExcept;

    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack());
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        status = -1;
        btnStatus = -1;
        initial = -1;
        destination = -1;
        hasLeader = false;
        lockExcept = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void buttonCallBack()
    {
        btn.interactable = false;
        switch (btnStatus)
        {
            case 0:
                status = 4;
                required2();
                break;
            case 1:
                status = 5;
                required2();
                break;
            case 2:
                required2();
                break;
        }
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
       
    }


    public void post()
    {
        GM2.boolStates[28] = true;
        btn.interactable = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        status = 0;
        mvmtPlayer = GM1.player;
        required2();
    }

    void reset()
    {
        GM2.boolStates[28] = false;
        btn.interactable = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        status = -1;
        initial = -1;
        destination = -1;
        hasLeader = false;
        lockExcept = -1;
    }

    public void required2()
    {
        switch (status)
        {
            case 0:
                StartCoroutine(wait1311());
                break;
            case 1:
                StartCoroutine(wait1312());
                break;
            case 2:
                //expend CP
                CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
                GM2.onCPChange(textScript.displayCP - 1);
                HighlightCPScript highlightCPScript = GameObject.Find("HighlightCPDisplay").GetComponent("HighlightCPScript") as HighlightCPScript;
                highlightCPScript.removeHighlight();
                status = 3;
                required2();
                break;
            case 3:
                check13141();
                break;
            case 4:
                check13142();
                break;
            case 5:
                check132();
                break;
            case 6:
                moveClear();
                reset();
                break;
        }
    }

    IEnumerator wait1311()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Declare Formation");
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        inputNumberObject.post();
        List<int> trace = findClearFormation(GM1.player);
        GM2.highlightSelected = -1;
        leaderSelected = -1;
        GM2.onRegLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }

        initial = GM2.highlightSelected;
        if (GM2.leaderSelected == spacesGM.ElementAt(initial).leader1 || leaderSelected == spacesGM.ElementAt(initial).leader2)
        {
            hasLeader = true;

        }
        GM2.highlightSelected = -1;
        inputNumberObject.reset();
        status = 1;
        required2();
    }

    IEnumerator wait1312()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Declare Destination");
        //InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        //inputNumberObject.post();
        List<int> trace = new List<int>();
        for (int i = 0; i < spaces.ElementAt(initial).adjacent.Count(); i++)
        {
            trace.Add(spaces.ElementAt(initial).adjacent[i] - 1);
            UnityEngine.Debug.Log(spaces.ElementAt(initial).adjacent[i] - 1);
        }

        GM2.highlightSelected = -1;
        //leaderSelected = -1;
        GM2.onRegLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        currentTextObject.reset();
        destination = GM2.highlightSelected;
        GM2.highlightSelected = -1;
        status = 2;
        required2();
    }

    void moveClear()
    {
        int permitted = 0;

        int command = 0;
        //todo: include 2 leaders
        if (hasLeader)
        {
            permitted = leaders.ElementAt(leaderSelected - 1).command;

        }
        else
        {
            permitted = 4;
        }
        
        if (!string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
        {
            if (hasLeader)
            {
                spacesGM.ElementAt(initial).removeLeader(leaderSelected);
                spacesGM.ElementAt(destination).addLeader(leaderSelected);
                onChangeLeader(initial, leaderSelected);
            }

            command = int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
            if (command > permitted)
            {
                command = permitted;
            }

            if (command > regulars[initial])
            {
                command = regulars[initial];
            }
        }


        spacesGM.ElementAt(destination).regular = spacesGM.ElementAt(destination).regular + command;
        spacesGM.ElementAt(initial).regular = spacesGM.ElementAt(initial).regular - command;
        regulars[destination] = regulars[destination] + command;
        regulars[initial] = regulars[initial] - command;
        onChangeReg(destination, GM1.player);
        onChangeReg(initial, GM1.player);
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        GM2.onCPChange(textScript.displayCP);
        //status = 3;
        //required2();
    }

    void check13141()
    {
        int has31 = -1;
        for (int i = 0; i < 6; i++)
        {
            List<CardObject> temp = null;
            switch (i)
            {
                case 0:
                    temp = hand0;
                    break;
                case 1:
                    temp = hand1;
                    break;
                case 2:
                    temp = hand2;
                    break;
                case 3:
                    temp = hand3;
                    break;
                case 4:
                    temp = hand4;
                    break;
                case 5:
                    temp = hand5;
                    break;
            }
            for (int j = 0; j < temp.Count(); j++)
            {
                if (temp.ElementAt(j).id == 31)
                {
                    has31 = i;
                }
            }
        }
        if (has31 != -1 && has31 != GM1.player)
        {
            UnityEngine.Debug.Log(has31);
            GM1.player = has31;
            GM2.onPlayerChange();
            GM2.chosenCard = "HIS-031";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 0;
            
        }
        else
        {
            status = 4;
            required2();
        }

    }



    void check13142()
    {
        if (hasLeader)
        {
            int has32 = -1;
            for (int i = 0; i < 6; i++)
            {
                List<CardObject> temp = null;
                switch (i)
                {
                    case 0:
                        temp = hand0;
                        break;
                    case 1:
                        temp = hand1;
                        break;
                    case 2:
                        temp = hand2;
                        break;
                    case 3:
                        temp = hand3;
                        break;
                    case 4:
                        temp = hand4;
                        break;
                    case 5:
                        temp = hand5;
                        break;
                }
                for (int j = 0; j < temp.Count(); j++)
                {

                    if (temp.ElementAt(j).id == 32)
                    {
                        has32 = i;
                    }
                }
            }
            if (has32 != -1 && has32 != GM1.player)
            {
                UnityEngine.Debug.Log(has32);
                GM1.player = has32;
                GM2.onPlayerChange();
                GM2.chosenCard = "HIS-032";
                GM2.onChosenCard();
                btn.interactable = true;
                btnStatus = 1;
               
            }
            else
            {
                status = 5;
                required2();
            }
        }
        else
        {
            status = 5;
            required2();
        }
        
    }

    void check132()
    {
        int[] canIntercept = new int[6];
        Array.Clear(canIntercept, 0,6);
        List<int> trace = new List<int>();
        for (int i=0; i<6; i++)
        {
            if (GM1.diplomacyState[GM1.player, i] == 1)
            {
                
                for (int j = 0; j < spaces.ElementAt(destination).adjacent.Count(); j++)
                {
                    if (spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j]-1).regularPower==i&&(spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j]-1).regular>0|| spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j]).merc > 0|| spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j]-1).cavalry > 0)){
                        canIntercept[i] = 1;
                        trace.Add(spaces.ElementAt(destination).adjacent[j] - 1);
                        UnityEngine.Debug.Log(spaces.ElementAt(initial).adjacent[j] - 1);
                    }
                    
                }
            }
        }
        if (canIntercept.Sum()>0)
        {
            for(int i=0; i<6; i++)
            {
                if (canIntercept[i] == 1)
                {
                    GM1.player = i;
                    GM2.onPlayerChange();
                    btn.interactable = true;
                    btnStatus = 2;
                }
            }
            
        }
        else
        {
            status = 6;
            required2();
        }
    }


}
