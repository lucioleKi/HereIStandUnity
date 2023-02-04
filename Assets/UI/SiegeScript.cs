using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnumSpaceScript;
using static GraphUtils;
using static DeckScript;
using static GM2;

public class SiegeScript : MonoBehaviour
{
    public Button btn;
    public int mvmtPlayer;
    public int status;
    public int btnStatus;
    public int initial;
    public int destination;
    public bool hasLeader;
    public int siegedPlayer;
    public int attackerDice;
    public int defenderDice;
    public int attackerHit;
    public int defenderHit;
    public bool attackerElim;
    public bool defenderElim;
    public List<int> tempTrace;
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
                status = 3;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 1:
                //skip HIS-032
                status = 4;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 2:
                //skip HIS-033 and HIS-036
                status = 5;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 3:
                //attacker skip HIS-028
                status = 8;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = siegedPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 4:
                //defender skip HIS-029
                status = 9;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 5:
                //skip HIS-035
                status = 11;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
        }
    }

    public void post()
    {
        GM2.boolStates[30] = true;
        btn.interactable = false;
        mvmtPlayer = GM1.player;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        status = 0;
        btnStatus = -1;
        initial = -1;
        destination = -1;
        hasLeader = false;
        attackerDice = 0;
        defenderDice = 0;
        attackerElim = false;
        defenderElim = false;
        required2();
    }

    void reset()
    {
        GM1.player = mvmtPlayer;
        GM2.onPlayerChange();
        btn.interactable = false;
        btnStatus = -1;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        status = -1;
        initial = -1;
        destination = -1;
        hasLeader = false;
        GM2.boolStates[30] = false;
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        GM2.onCPChange(textScript.displayCP - 1);
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.reset();

        //HIS105 treachery
        if (GM2.boolStates[35])
        {
            GM2.boolStates[35] = false;
            chosenCard = "";
            onChosenCard();
            if (GM1.phase == 6)
            {
                GM1.nextImpulse();
            }
        }

    }

    public void required2()
    {
        switch (status)
        {
            case 0:
                StartCoroutine(wait1511());
                break;
            case 1:
                StartCoroutine(wait1512());
                break;
            case 2:
                //HIS-031
                check13141();
                break;
            case 3:
                //HIS-032
                check13142();
                break;
            case 4:
                attackerResponseCards();
                break;
            case 5:
                battleDice();
                break;
            case 6:
                //end state
                StartCoroutine(moveClear());

                break;
            case 7:
                attackerCombatCards();
                break;
            case 8:
                defenderCombatCards();
                break;
            case 9:
                StartCoroutine(roll153());
                break;
            case 10:
                //HIS-035
                check35();
                break;
            case 11:
                casualties(mvmtPlayer, initial, defenderHit);
                casualties(siegedPlayer, destination, attackerHit);
                evaluate();
                break;

        }
    }

    IEnumerator wait1511()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Declare Formation");
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        inputNumberObject.post();
        List<int> trace = new List<int>();
        if (GM2.chosenCard == "HIS-042")
        {
            for (int i = 0; i < 134; i++)
            {
                if (spacesGM.ElementAt(i).leader1 == 15 || spacesGM.ElementAt(i).leader2 == 15)
                {
                    trace.Add(i);
                    break;
                }
            }
        }
        else
        {
            trace = checkSiegeFrom(GM1.player);
        }
        GM2.highlightSelected = -1;
        leaderSelected = -1;
        GM2.onRegLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1 || string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text) || GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text == "0")
        {
            if (string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text) && GM2.highlightSelected != -1)
            {
                GM2.highlightSelected = -1;
                GM2.onRegLayer();
                GM2.onHighlight(trace);
            }
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

    IEnumerator wait1512()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Declare siege destination");
        //InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        //inputNumberObject.post();
        List<int> trace = new List<int>();

        //HIS042 in effect
        if (GM2.chosenCard == "HIS-042")
        {
            for (int j = 0; j < spaces.ElementAt(initial).adjacent.Count(); j++)
            {

                if (spaces.ElementAt(spaces.ElementAt(initial).adjacent[j]).spaceType != (SpaceType)0 && spacesGM.ElementAt(spaces.ElementAt(initial).adjacent[j]).controlPower != 0)
                {
                    trace.Add(spaces.ElementAt(initial).adjacent[j]);
                }
            }
        }
        else
        {
            trace = checkSiege(GM1.player);
        }
        GM2.highlightSelected = -1;
        //leaderSelected = -1;
        GM2.onRegLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        if (!spacesGM.ElementAt(highlightSelected).sieged)
        {
            spacesGM.ElementAt(highlightSelected).sieged = true;
        }
        currentTextObject.reset();
        destination = GM2.highlightSelected;
        GM2.highlightSelected = -1;
        siegedPlayer = spacesGM.ElementAt(destination).controlPower;
        status = 2;
        required2();


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
            status = 3;
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
                status = 4;
                required2();
            }
        }
        else
        {
            status = 4;
            required2();
        }

    }

    void attackerResponseCards()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Play Response Cards");
        btn.interactable = true;
        btnStatus = 2;
    }

    void battleDice()
    {
        int permitted = 0;
        //todo: include 2 leaders
        if (spacesGM.ElementAt(destination).regular == 0 && spacesGM.ElementAt(destination).merc == 0 && spacesGM.ElementAt(destination).cavalry == 0)
        {
            defenderElim = true;
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

                if (attackerDice > spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc)
                {
                    attackerDice = spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc;
                }
            }

        }
        else
        {
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

                attackerDice += int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
                if (attackerDice > permitted)
                {
                    attackerDice = permitted;
                }

                if (attackerDice > spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc)
                {
                    attackerDice = spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc;
                }
                attackerDice = (attackerDice + 1) / 2;
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
        defenderDice += spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc + 1;
        status = 7;
        required2();
    }

    void attackerCombatCards()
    {
        bool has28 = false;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Play Combat Cards");
        List<CardObject> temp = null;
        switch (mvmtPlayer)
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
            if (temp.ElementAt(j).id == 28)
            {
                has28 = true;
            }
        }
        if (has28)
        {
            UnityEngine.Debug.Log(has28);
            GM2.chosenCard = "HIS-028";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 5;
        }
        else
        {
            if (siegedPlayer < 6)
            {
                GM1.player = siegedPlayer;
                GM2.onPlayerChange();
            }

            status = 8;
            required2();
        }

    }

    void defenderCombatCards()
    {
        bool has27 = false;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Play Combat Cards");
        List<CardObject> temp = new List<CardObject>();
        switch (siegedPlayer)
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
            if (temp.ElementAt(j).id == 27)
            {
                has27 = true;
            }
        }
        if (has27)
        {
            UnityEngine.Debug.Log(has27);
            GM2.chosenCard = "HIS-027";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 6;
        }
        else
        {
            GM1.player = mvmtPlayer;
            GM2.onPlayerChange();
            status = 9;
            UnityEngine.Debug.Log("status 9");
            required2();
        }
    }

    IEnumerator moveClear()
    {
        yield return new WaitForEndOfFrame();
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
            UnityEngine.Debug.Log(spacesGM.ElementAt(initial).regular);
            UnityEngine.Debug.Log(spacesGM.ElementAt(initial).merc);
            if (command > spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc)
            {
                command = spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc;
            }
        }
        UnityEngine.Debug.Log(command);

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
            yield break;
        }

        spacesGM.ElementAt(destination).regular = spacesGM.ElementAt(destination).regular + command;
        UnityEngine.Debug.Log(spacesGM.ElementAt(destination).regular);
        spacesGM.ElementAt(initial).regular = spacesGM.ElementAt(initial).regular - command;
        onChangeReg(destination, GM1.player);
        onChangeReg(initial, GM1.player);

        if (spacesGM.ElementAt(initial).regular == 0 && spacesGM.ElementAt(initial).merc == 0 && spacesGM.ElementAt(initial).cavalry == 0 && spaces.ElementAt(initial).spaceType == 0)
        {
            spacesGM.ElementAt(initial).regularPower = -1;
        }
        reset();
    }

    IEnumerator roll153()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;

        attackerHit = 0;
        defenderHit = 0;
        for (int i = 0; i < defenderDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);
            if (randomIndex >= 5)
            {
                defenderHit++;
            }
        }
        attackerDice -= defenderHit;

        for (int i = 0; i < attackerDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);
            if (randomIndex >= 5)
            {
                attackerHit++;
            }
        }
        currentTextObject.post("Attacker hit: " + attackerHit.ToString() + " out of " + attackerDice.ToString() + ".\nDefender hit: " + defenderHit.ToString() + " out of " + defenderDice.ToString() + ".");
        yield return new WaitForSeconds(3);
        status = 10;
        required2();
    }

    void check35()
    {
        int has35 = -1;
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
                if (temp.ElementAt(j).id == 35)
                {
                    has35 = i;
                }
            }
        }
        if (has35 != -1 && has35 != GM1.player)
        {
            UnityEngine.Debug.Log(has35);
            GM1.player = has35;
            GM2.onPlayerChange();
            GM2.chosenCard = "HIS-035";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 5;

        }
        else
        {
            status = 11;
            required2();
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

    void evaluate()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        if (attackerElim && hasLeader)
        {
            switch (siegedPlayer)
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
        if (defenderElim && attackerHit > 0 && !attackerElim)
        {
            UnityEngine.Debug.Log("Successful Assault");
            currentTextObject.post("Successful Assault");
            spacesGM.ElementAt(destination).sieged = false;
            int marker = 1;
            //if(spaces.ElementAt(destination).spaceType==3|| spaces.ElementAt(destination).spaceType == 5)
            //{
            spacesGM.ElementAt(destination).controlPower = mvmtPlayer;

            if (GM1.religiousInfluence[destination] == 0)
            {
                marker = 3;
            }
            else
            {
                marker = 4;
            }
            //}
            GM2.onRemoveSpace(destination);
            GM2.onAddSpace(destination, mvmtPlayer, marker);
            GM1.cardTracks[mvmtPlayer]++;
            if (siegedPlayer < 6)
            {
                GM1.cardTracks[siegedPlayer]--;
            }
            GM1.updateVP();
            GM2.onVP();

            status = 6;
            required2();
        }
        else if (GM2.boolStates[35] && (spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc + spacesGM.ElementAt(initial).cavalry > spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc + spacesGM.ElementAt(destination).cavalry))

        {
            casualties(siegedPlayer, destination, spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc + spacesGM.ElementAt(destination).cavalry);
            //all leaders are captured
            if (defenderElim && spacesGM.ElementAt(destination).leader1 > 0 && spacesGM.ElementAt(destination).leader2 > 0)
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
            currentTextObject.post("Successful Assault");
            spacesGM.ElementAt(destination).sieged = false;
            int marker = 1;
            //if(spaces.ElementAt(destination).spaceType==3|| spaces.ElementAt(destination).spaceType == 5)
            //{
            spacesGM.ElementAt(destination).controlPower = mvmtPlayer;
            if (GM1.religiousInfluence[destination] == 0)
            {
                marker = 3;
            }
            else
            {
                marker = 4;
            }
            //}
            GM2.onRemoveSpace(destination);
            GM2.onAddSpace(destination, mvmtPlayer, marker);
            GM1.cardTracks[mvmtPlayer]++;
            if (siegedPlayer < 6)
            {
                GM1.cardTracks[siegedPlayer]--;
            }

            GM1.updateVP();
            GM2.onVP();

            status = 6;
            required2();
        }
        else
        {
            currentTextObject.post("Unsuccessful Assault");
            if (spacesGM.ElementAt(initial).regular + spacesGM.ElementAt(initial).merc + spacesGM.ElementAt(initial).cavalry <= spacesGM.ElementAt(destination).regular + spacesGM.ElementAt(destination).merc + spacesGM.ElementAt(destination).cavalry)
            {
                spacesGM.ElementAt(destination).sieged = false;
            }
            GM2.boolStates[30] = false;
            reset();
        }
    }
}
