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
    public bool[] alreadyIntercepted;
    public List<int> tempTrace;
    public int fieldPlayer;
    public int attackerDice;
    public int defenderDice;
    public int has29;
    //if hapsburg plays 30
    public bool has30;
    public int attackerHit;
    public int defenderHit;
    public bool attackerElim;
    public bool defenderElim;

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
        alreadyIntercepted = new bool[6];
        tempTrace = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void buttonCallBack()
    {
        if (btnStatus != -1)
        {
            btn.interactable = false;
        }
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;

        switch (btnStatus)
        {
            case 0:
                //skip HIS-031
                status = 4;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 1:
                //skip HIS-032
                status = 5;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 2:
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 3:
                status = 8;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 4:
                status = 9;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 5:
                //skip attacker combat cards
                status = 11;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = fieldPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 6:
                //skip defender combat cards
                status = 12;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = fieldPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 7:
                status = 14;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 8:
                //skip avoid battle
                status = 17;
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                startButton.btn.interactable = false;
                startButton.status = 2;
                required2();
                break;
            case 9:
                //skip withdraw into fortifications, go to field battle
                status = 7;
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                startButton.btn.interactable = false;
                startButton.status = 4;
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
        initial = -1;
        destination = -1;
        hasLeader = false;
        lockExcept = -1;
        Array.Clear(alreadyIntercepted, 0, 6);
        tempTrace.Clear();
        mvmtPlayer = GM1.player;
        attackerDice = 0;
        defenderDice = 0;
        has29 = -1;
        has30 = false;
        attackerElim = false;
        defenderElim = false;
        required2();
    }

    void reset()
    {
        GM1.player = mvmtPlayer;
        GM2.onPlayerChange();
        btn.interactable = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        status = -1;
        initial = -1;
        destination = -1;
        hasLeader = false;
        lockExcept = -1;
        Array.Clear(alreadyIntercepted, 0, 6);
        tempTrace.Clear();
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        GM2.onCPChange(textScript.displayCP - 1);
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.reset();
        GM2.boolStates[28] = false;

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
                //check empty siege
                if (spaces.ElementAt(destination).spaceType != 0 && (spacesGM.ElementAt(destination).regularPower!=mvmtPlayer)&&(spacesGM.ElementAt(destination).regular ==0 && spacesGM.ElementAt(destination).merc ==0))
                {
                    spacesGM.ElementAt(destination).sieged = true;
                    reset();
                }
                else
                {
                    status = 3;
                    required2();
                }
                break;
            case 3:
                //HIS-031
                check13141();
                break;
            case 4:
                //HIS-032
                check13142();
                break;
            case 5:
                //interception
                check132();
                break;
            case 6:
                //end state
                moveClear();
                reset();
                break;
            case 7:
                //field battle 1. HIS-033
                check1411();
                break;
            case 8:
                //field battle 2. HIS-036
                check1412();
                break;
            case 9:
                battleDice();
                break;
            case 10:
                //field battle 4. attacker combat cards
                attackerCombatCards();
                break;
            case 11:
                //field battle 5. defender combat cards
                defenderCombatCards();
                break;
            case 12:
                StartCoroutine(roll146());
                break;
            case 13:
                //field battle 7: if power is ottoman, offer the option to play janissaries
                GM1.player = 0;
                GM2.onPlayerChange();
                GM2.chosenCard = "HIS-001";
                GM2.onChosenCard();
                btn.interactable = true;
                btnStatus = 7;
                break;
            case 14:
                //1418-1420
                check1420();
                break;
            case 15:
                //retreat
                retreat();
                moveClear();
                reset();
                break;
            case 16:
                //avoid battle
                check133();
                break;
            case 17:
                //withdraw into fortifications
                check134();
                break;
            case 18:
                //successful withdraw
                checkField();
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


        spacesGM.ElementAt(destination).regularPower = mvmtPlayer;
        UnityEngine.Debug.Log("regular power changed " + destination.ToString());
        regulars[destination] = regulars[destination] + command;
        regulars[initial] = regulars[initial] - command;
        if (spacesGM.ElementAt(initial).merc > command)
        {
            spacesGM.ElementAt(destination).merc = spacesGM.ElementAt(destination).merc + command;

            spacesGM.ElementAt(initial).merc = spacesGM.ElementAt(initial).merc - command;
            onChangeMerc(destination, GM1.player);
            onChangeMerc(initial, GM1.player);
            command = 0;
        }
        else if (spacesGM.ElementAt(initial).merc > 0)
        {
            spacesGM.ElementAt(destination).merc = spacesGM.ElementAt(destination).merc + spacesGM.ElementAt(initial).merc;
            command -= spacesGM.ElementAt(initial).merc;
            spacesGM.ElementAt(initial).merc = 0;
            onChangeMerc(destination, GM1.player);
            onChangeMerc(initial, GM1.player);
        }
        if (spacesGM.ElementAt(initial).regular == 0 && spacesGM.ElementAt(initial).merc == 0 && spacesGM.ElementAt(initial).cavalry == 0 && spaces.ElementAt(initial).spaceType == 0)
        {
            spacesGM.ElementAt(initial).regularPower = -1;
            return;
        }
        
            spacesGM.ElementAt(destination).regular = spacesGM.ElementAt(destination).regular + command;
            
            spacesGM.ElementAt(initial).regular = spacesGM.ElementAt(initial).regular - command;
            onChangeReg(destination, GM1.player);
            onChangeReg(initial, GM1.player);
       
        if (spacesGM.ElementAt(initial).regular == 0 && spacesGM.ElementAt(initial).merc == 0 && spacesGM.ElementAt(initial).cavalry == 0 && spaces.ElementAt(initial).spaceType == 0)
        {
            spacesGM.ElementAt(initial).regularPower = -1;
        }

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
        int canIntercept = -1;
        tempTrace.Clear();
        for (int i = 0; i < 6; i++)
        {
            if (!alreadyIntercepted[i])
            {
                alreadyIntercepted[i] = true;
                if (GM1.diplomacyState[GM1.player, i] == 1 || GM1.diplomacyState[i, GM1.player] == 1)
                {

                    for (int j = 0; j < spaces.ElementAt(destination).adjacent.Count(); j++)
                    {
                        if (spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j] - 1).regularPower == i && (spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j] - 1).regular > 0 || spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j]).merc > 0 || spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j] - 1).cavalry > 0))
                        {
                            tempTrace.Add(spaces.ElementAt(destination).adjacent[j] - 1);
                            UnityEngine.Debug.Log(spaces.ElementAt(initial).adjacent[j] - 1);
                        }

                    }
                    if (tempTrace.Count() > 0)
                    {
                        canIntercept = i;
                        break;
                    }
                }
            }


        }
        UnityEngine.Debug.Log("intercept? " + canIntercept.ToString());
        if (canIntercept != -1)
        {
            GM1.player = canIntercept;
            GM2.onPlayerChange();
            GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Intercept";
            StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
            startButton.startOther(1);
            btn.interactable = true;
            btnStatus = 2;
        }
        else
        {
            GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
            StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
            startButton.btn.interactable=true;
            startButton.status = 1;
            status = 16;
            required2();
        }
    }

    void check1411()
    {

        int has33 = -1;
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

                if (temp.ElementAt(j).id == 33)
                {
                    has33 = i;
                }
            }
        }
        if (has33 == fieldPlayer && has33 == GM1.player)
        {
            UnityEngine.Debug.Log(has33);
            GM1.player = has33;
            GM2.onPlayerChange();
            GM2.chosenCard = "HIS-033";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 3;

        }
        else
        {
            status = 8;
            required2();
        }


    }

    void check1412()
    {

        int has36 = -1;
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

                if (temp.ElementAt(j).id == 36)
                {
                    has36 = i;
                }
            }
        }
        if (has36 == fieldPlayer && has36 == GM1.player)
        {
            UnityEngine.Debug.Log(has36);
            GM1.player = has36;
            GM2.onPlayerChange();
            GM2.chosenCard = "HIS-036";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 4;

        }
        else
        {
            status = 9;
            required2();
        }


    }

    void battleDice()
    {
        int permitted = 0;
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

            attackerDice = int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
            if (attackerDice > permitted)
            {
                attackerDice = permitted;
            }

            if (attackerDice > regulars[initial])
            {
                attackerDice = regulars[initial];
            }
        }
        if (hasLeader)
        {
            attackerDice += leaders.ElementAt(leaderSelected - 1).battle;
        }
        if (spacesGM.ElementAt(destination).leader1 != 0)
        {
            defenderDice += leaders.ElementAt(spacesGM.ElementAt(destination).leader1 - 1).battle;
        }
        if (spacesGM.ElementAt(destination).leader2 != 0)
        {
            defenderDice += leaders.ElementAt(spacesGM.ElementAt(destination).leader2 - 1).battle;
        }
        defenderDice += spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc+1;

        status = 10;
        required2();
    }

    void attackerCombatCards()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Play Combat Cards");
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        startButton.btn.interactable = false;
        btn.interactable = true;
        btnStatus = 5;
    }

    void defenderCombatCards()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Play Combat Cards");
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        startButton.btn.interactable = false;
        btn.interactable = true;
        btnStatus = 6;
    }

    IEnumerator roll146()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;

        attackerHit = 0;
        defenderHit = 0;
        if (has30)
        {
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex >= 4)
                {
                    if (mvmtPlayer == 1)
                    {
                        attackerHit++;
                    }
                    else
                    {
                        defenderHit++;
                    }
                }
            }

        }
        if (has29 == mvmtPlayer)
        {
            for (int i = 0; i < attackerDice; i++)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex >= 5)
                {
                    attackerHit++;
                }
            }
            defenderDice -= attackerHit;
            casualties(fieldPlayer, destination, attackerHit);
            for (int i = 0; i < defenderDice; i++)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex >= 5)
                {
                    defenderHit++;
                }
            }

            currentTextObject.post("Attacker hit: " + attackerHit.ToString() + " out of " + attackerDice.ToString() + ".\nDefender hit: " + defenderHit.ToString() + " out of " + defenderDice.ToString() + ".");
        }
        else if (has29 == fieldPlayer)
        {
            for (int i = 0; i < defenderDice; i++)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex >= 5)
                {
                    defenderHit++;
                }
            }
            attackerDice -= defenderHit;
            casualties(mvmtPlayer, destination, defenderHit);
            for (int i = 0; i < attackerDice; i++)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex >= 5)
                {
                    attackerHit++;
                }
            }

            currentTextObject.post("Attacker hit: " + attackerHit.ToString() + " out of " + attackerDice.ToString() + ".\nDefender hit: " + defenderHit.ToString() + " out of " + defenderDice.ToString() + ".");
        }
        else
        {

            for (int i = 0; i < defenderDice; i++)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex >= 5)
                {
                    defenderHit++;
                }
            }
            for (int i = 0; i < attackerDice; i++)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex >= 5)
                {
                    attackerHit++;
                }
            }

            currentTextObject.post("Attacker hit: " + attackerHit.ToString() + " out of " + attackerDice.ToString() + ".\nDefender hit: " + defenderHit.ToString() + " out of " + defenderDice.ToString()+".");
        }
        yield return new WaitForSeconds(3);
        if ((mvmtPlayer == 0 || fieldPlayer == 0) && hand0.ElementAt(0).cardType != 0)
        {
            status = 13;

        }
        else
        {
            status = 14;
        }
        required2();
    }

    void check1420()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;


        if (has29 == mvmtPlayer)
        {
            casualties(mvmtPlayer, initial, defenderHit);
        }
        else if (has29 == fieldPlayer)
        {
            casualties(fieldPlayer, destination, attackerHit);
        }
        else
        {
            casualties(mvmtPlayer, initial, defenderHit);
            casualties(fieldPlayer, destination, attackerHit);
        }
        if (attackerElim && defenderElim)
        {
            if (attackerHit <= defenderHit)
            {
                spacesGM.ElementAt(destination).regular++;
                regulars[destination]++;
                spacesGM.ElementAt(destination).regularPower = fieldPlayer;
                GM2.onChangeReg(destination, fieldPlayer);
                defenderElim = false;
            }
            else
            {
                spacesGM.ElementAt(initial).regular++;
                regulars[initial]++;
                spacesGM.ElementAt(initial).regularPower = mvmtPlayer;
                GM2.onChangeReg(initial, mvmtPlayer);
                attackerElim = false;
            }
        }
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        if (attackerElim && hasLeader)
        {
            switch (fieldPlayer)
            {
                case 0:
                    handMarkerScript.bonus0.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(initial).leader1);
                    break;
                case 1:
                    handMarkerScript.bonus1.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(initial).leader1);
                    break;
                case 2:
                    handMarkerScript.bonus2.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(initial).leader1);
                    break;
                case 3:
                    handMarkerScript.bonus3.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(initial).leader1);
                    break;
                case 4:
                    handMarkerScript.bonus4.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(initial).leader1);
                    break;
                case 5:
                    handMarkerScript.bonus5.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(initial).leader1);
                    break;
            }
            spacesGM.ElementAt(initial).removeLeader(spacesGM.ElementAt(initial).leader1);
            onChangeLeader(initial, spacesGM.ElementAt(initial).leader1);
        }
        else if (defenderElim && spacesGM.ElementAt(destination).leader1 > 0 && spacesGM.ElementAt(destination).leader2 > 0)
        {
            switch (mvmtPlayer)
            {
                case 0:
                    handMarkerScript.bonus0.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(destination).leader1);
                    break;
                case 1:
                    handMarkerScript.bonus1.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(destination).leader1);
                    break;
                case 2:
                    handMarkerScript.bonus2.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(destination).leader1);
                    break;
                case 3:
                    handMarkerScript.bonus3.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(destination).leader1);
                    break;
                case 4:
                    handMarkerScript.bonus4.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(destination).leader1);
                    break;
                case 5:
                    handMarkerScript.bonus5.Add("Sprites/jpg/Leader/" + spacesGM.ElementAt(destination).leader1);
                    break;
            }
            spacesGM.ElementAt(destination).removeLeader(spacesGM.ElementAt(destination).leader1);
            onChangeLeader(destination, spacesGM.ElementAt(destination).leader1);
        }
        if (attackerHit > defenderHit)
        {
            currentTextObject.post("Winner: Attacker");
            if (!defenderElim)
            {
                if (spaces.ElementAt(destination).spaceType != 0 && spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc > spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc)
                {
                    //active player retreat because no siege
                    currentTextObject.post("Field battle winner: Attacker.\nDefender is not under siege.");
                    reset();
                }
                else if (spaces.ElementAt(destination).spaceType != 0 && spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc > 4)
                {
                    //defender retreats #-4 units from fortified space
                    status = 15;
                    required2();
                }
                else if (spaces.ElementAt(destination).spaceType != 0)
                {
                    //start siege
                    spacesGM.ElementAt(destination).sieged = true;
                    reset();
                }
                else
                {
                    //defender retreats all
                    status = 15;
                    required2();
                }

            }
            else
            {
                //finish land movement
                status = 6;
                required2();
            }

        }
        else
        {
            currentTextObject.post("Field battle winner: Defender");

            GM2.boolStates[28] = false;
            reset();

        }


    }

    public void check133()
    {
        tempTrace.Clear();


        if (spacesGM.ElementAt(destination).regularPower != mvmtPlayer && spacesGM.ElementAt(destination).regularPower != -1 && (spacesGM.ElementAt(destination).regular > 0 || spacesGM.ElementAt(destination).merc > 0))
        {
            fieldPlayer = spacesGM.ElementAt(destination).regularPower;
            for (int j = 0; j < spaces.ElementAt(destination).adjacent.Count(); j++)
            {
                if (spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j] - 1).regularPower == fieldPlayer || spacesGM.ElementAt(spaces.ElementAt(destination).adjacent[j] - 1).regularPower == -1)
                {
                    tempTrace.Add(spaces.ElementAt(destination).adjacent[j] - 1);
                    UnityEngine.Debug.Log(spaces.ElementAt(destination).adjacent[j] - 1);
                }

            }
            GM1.player = spacesGM.ElementAt(destination).regularPower;
            UnityEngine.Debug.Log("avoid? " + tempTrace.Count().ToString());
            GM2.onPlayerChange();
            btn.interactable = true;
            btnStatus = 8;
            GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Avoid battle";
            StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
            startButton.startOther(2);
        }
        else
        {
            UnityEngine.Debug.Log("no avoid battle");
            GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
            StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
            startButton.btn.interactable = false;
            startButton.status = -1;
            status = 6;
            required2();
        }
    }

    public void check134()
    {
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        if (spacesGM.ElementAt(destination).regularPower != mvmtPlayer && spacesGM.ElementAt(destination).regularPower != -1 && (spacesGM.ElementAt(destination).regular > 0 || spacesGM.ElementAt(destination).merc > 0) && (spaces.ElementAt(destination).spaceType != 0 && (spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc <= 4)))
        {
            //can withdraw
            fieldPlayer = spacesGM.ElementAt(destination).regularPower;
            GM1.player = spacesGM.ElementAt(destination).regularPower;
            GM2.onPlayerChange();
            btn.interactable = true;
            btnStatus = 9;
            GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Withdraw";
            startButton.startOther(3);
        }
        else if (spacesGM.ElementAt(destination).regularPower != mvmtPlayer && spacesGM.ElementAt(destination).regularPower != -1 && (spacesGM.ElementAt(destination).regular > 0 || spacesGM.ElementAt(destination).merc > 0)&&spaces.ElementAt(destination).spaceType != 0) {
            //cannot withdraw because not fortified, go to field battle
            fieldPlayer = spacesGM.ElementAt(destination).regularPower;
            status = 7;
            GM1.player = mvmtPlayer;
            GM2.onPlayerChange();
            startButton.btn.interactable = false;
            startButton.status = -1;
            required2();
        }
        else
        {
            //no enemy units, move clear
            UnityEngine.Debug.Log("no enemy units left");
            GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
            
            startButton.btn.interactable = false;
            startButton.status=-1;
            status = 6;
            required2();
        }
    }

    public void checkField()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        GM2.onCPChange(textScript.displayCP - 1);
        HighlightCPScript highlightCPObject = GameObject.Find("HighlightCPDisplay").GetComponent("HighlightCPScript") as HighlightCPScript;
        highlightCPObject.removeHighlight();
        if (textScript.displayCP >= 1)
        {
            //active player retreat because no siege
            currentTextObject.post("Defender is not under siege.");
            UnityEngine.Debug.Log("defender not under siege");
            reset();
            post();
            required2();
        }
        else
        {
            //active player retreat because no siege
            GM2.boolStates[28] = false;
            reset();
        }

    }


    IEnumerator retreat()
    {
        List<int> trace = new List<int>();
        for (int i = 0; i < spaces.ElementAt(destination).adjacent.Count(); i++)
        {
            //cannot retreat into initial space
            if (spaces.ElementAt(destination).adjacent.ElementAt(i) == initial)
            {
                continue;
            }
            //cannot retreat into unrest
            if (spacesGM.ElementAt(spaces.ElementAt(destination).adjacent.ElementAt(i)).unrest)
            {
                continue;
            }
            //cannot retreat into independent space
            if (spacesGM.ElementAt(spaces.ElementAt(destination).adjacent.ElementAt(i)).controlPower == 10)
            {
                continue;
            }
            //cannot retreat into space containing enemy units
            if (spacesGM.ElementAt(spaces.ElementAt(destination).adjacent.ElementAt(i)).regularPower == mvmtPlayer)
            {
                continue;
            }
            trace.Add(spaces.ElementAt(destination).adjacent.ElementAt(i));
        }
        if (trace.Count() == 0)
        {
            casualties(fieldPlayer, destination, 10);
            yield break;
        }
        GM2.highlightSelected = -1;
        leaderSelected = -1;
        GM2.onRegLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }

        int retreatTo = GM2.highlightSelected;

        if (spaces.ElementAt(destination).spaceType == 0)
        {

            spacesGM.ElementAt(retreatTo).regular += spacesGM.ElementAt(destination).regular;
            spacesGM.ElementAt(destination).regular = 0;
            spacesGM.ElementAt(retreatTo).regularPower = fieldPlayer;
            spacesGM.ElementAt(destination).regularPower = -1;
            regulars[retreatTo] += regulars[destination];
            regulars[destination] = 0;
            onChangeReg(destination, fieldPlayer);
            onChangeReg(initial, fieldPlayer);

            spacesGM.ElementAt(retreatTo).merc += spacesGM.ElementAt(destination).merc;
            spacesGM.ElementAt(destination).merc = 0;

            onChangeMerc(destination, fieldPlayer);
            onChangeMerc(initial, fieldPlayer);
        }
        else
        {
            spacesGM.ElementAt(retreatTo).regular += spacesGM.ElementAt(destination).regular - 4;
            spacesGM.ElementAt(destination).regular = 4;
            spacesGM.ElementAt(retreatTo).regularPower = fieldPlayer;
            regulars[retreatTo] += regulars[destination] - 4;
            regulars[destination] = 4;
            onChangeReg(destination, fieldPlayer);
            onChangeReg(initial, fieldPlayer);

            spacesGM.ElementAt(retreatTo).merc += spacesGM.ElementAt(destination).merc;
            spacesGM.ElementAt(destination).merc = 0;

            onChangeMerc(destination, fieldPlayer);
            onChangeMerc(initial, fieldPlayer);
        }
    }

    void casualties(int player, int place, int hit)
    {

        if (spacesGM.ElementAt(place).merc >= hit)
        {

            spacesGM.ElementAt(place).merc -= hit;
            hit = 0;
            GM2.onChangeMerc(place, player);
            return;
        }
        else if (spacesGM.ElementAt(place).merc > 0)
        {
            hit -= spacesGM.ElementAt(place).merc;
            spacesGM.ElementAt(place).merc = 0;
            GM2.onChangeMerc(place, player);
        }
        if (spacesGM.ElementAt(place).cavalry >= hit)
        {
            spacesGM.ElementAt(place).cavalry -= hit;
            hit = 0;
            GM2.onChangeCav(place, player);
            return;
        }
        else if (spacesGM.ElementAt(place).cavalry > 0)
        {
            hit -= spacesGM.ElementAt(place).cavalry;
            spacesGM.ElementAt(place).cavalry = 0;
            GM2.onChangeCav(place, player);
        }
        if (spacesGM.ElementAt(place).regular > hit)
        {
            spacesGM.ElementAt(place).regular -= hit;
            regulars[place] -= hit;
            hit = 0;
            GM2.onChangeReg(place, player);
            return;
        }
        else if (spacesGM.ElementAt(place).regular > 0)
        {
            if (player == mvmtPlayer)
            {
                attackerElim = true;
            }
            else
            {
                defenderElim = true;
            }
            hit -= spacesGM.ElementAt(place).regular;
            regulars[place] = 0;
            spacesGM.ElementAt(place).regular = 0;
            spacesGM.ElementAt(place).regularPower = -1;
            GM2.onChangeReg(place, player);
        }
    }
}
