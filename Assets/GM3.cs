using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM2;
using static GraphUtils;

public class GM3
{
    public static GM3 instance;
    public static GM3 Instance
    {
        get
        {
            if (instance == null)
            {
                UnityEngine.Debug.Log("GM3 not initiated.");
            }
            return instance;
        }
    }

    public void HIS001A()
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        NavalMvmt navalMvmt = GameObject.Find("ProcedureButton").GetComponent("NavalMvmt") as NavalMvmt;
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);
            if (randomIndex >= 5)
            {
                if (GM2.boolStates[28])
                {
                    if (landMvmt.mvmtPlayer == 0)
                    {
                        landMvmt.attackerHit++;
                    }
                    else
                    {
                        landMvmt.defenderHit++;
                    }
                }
                else
                {
                    if (navalMvmt.mvmtPlayer == 0)
                    {
                        navalMvmt.attackerHit++;
                    }
                    else
                    {
                        navalMvmt.defenderHit++;
                    }
                }

            }
        }
        hand0.RemoveAt(0);
        if (GM2.boolStates[28])
        {
            GM1.player = landMvmt.mvmtPlayer;
        }
        else
        {
            GM1.player = navalMvmt.mvmtPlayer;
        }

        GM2.onPlayerChange();
        if (GM2.boolStates[28])
        {
            landMvmt.status = 14;
            landMvmt.required2();
        }
        else
        {
            navalMvmt.status = 9;
            navalMvmt.required2();
        }

    }

    public IEnumerator HIS001B()
    {
        List<int> trace = new List<int>();

        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).controlPower == 0 & i != 111)
            {
                trace.Add(i);

            }
        }
        for (int i = 0; i < 4; i++)
        {
            currentTextObject.pauseColor();
            currentTextObject.post("Pick " + (4 - i).ToString() + " highlighted target spaces to add 1 regular.");
            highlightSelected = -1;
            onRegLayer();
            onHighlight(trace);
            while (player != 0 || highlightSelected == -1)//if player is not 1 this wouldn't work
            {
                yield return null;
            }
            regulars[highlightSelected]++;
            spacesGM.ElementAt(highlightSelected).regular++;
            onChangeReg(highlightSelected, 0);
        }
        highlightSelected = -1;
        yield return new WaitForSeconds(3);

        currentTextObject.reset();
        hand0.RemoveAt(0);
    }

    public IEnumerator HIS002()
    {
        List<int> trace = new List<int>();
        int CharlesPos = -1;

        highlightSelected = -1;
        InputToggleObject inputToggleObject = GameObject.Find("InputToggle").GetComponent("InputToggleObject") as InputToggleObject;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;

        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).controlPower == 1)
            {
                trace.Add(i);
                if (spacesGM.ElementAt(i).leader1 == 2 || spacesGM.ElementAt(i).leader1 == 4)
                {
                    CharlesPos = i;
                    if (spacesGM.ElementAt(i).leader2 == 2 || spacesGM.ElementAt(i).leader2 == 4)
                    {
                        currentTextObject.pauseColor();
                        currentTextObject.post("Also move Duke of Alva?");
                        inputToggleObject.post();
                    }
                }
            }
        }
        LayerScript layerObject = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
        layerObject.highlight1Leader("leader_2");
        onHighlight(trace);
        while (player != 1 || highlightSelected == -1)
        {
            yield return null;
        }
        layerObject.reset1Leader();
        if (CharlesPos != -1)
        {
            spacesGM.ElementAt(CharlesPos).removeLeader(2);

            if (inputToggleObject.GetComponent<Toggle>().isOn)
            {
                currentTextObject.restartColor();
                spacesGM.ElementAt(CharlesPos).removeLeader(4);
            }

            spacesGM.ElementAt(highlightSelected).addLeader(2);
            UnityEngine.Debug.Log(highlightSelected.ToString());
            onChangeLeader(highlightSelected, 2);
            if (inputToggleObject.GetComponent<Toggle>().isOn)
            {
                spacesGM.ElementAt(highlightSelected).addLeader(4);
                onChangeLeader(highlightSelected, 4);
            }

        }
        yield return new WaitForSeconds(3);

        currentTextObject.reset();

        inputToggleObject.reset();
        hand1.RemoveAt(0);
    }

    public IEnumerator HIS003()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        List<int> powers = new List<int> { 1, 3, 8 };
        highlightSelected = -1;
        onHighlightDip(powers);
        while (highlightSelected==-1)
        {
            yield return null;
        }
        if(highlightSelected==8)
        {
            GM1.player = 3;
            GM2.onPlayerChange();
            StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
            GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Intervene";
            GM2.onSkipCard(3);
        }
        else if (highlightSelected == 1)
        {
            hand2.RemoveAt(0);
            chosenCard = "";
            onChosenCard();
            diplomacyState[1, 2] = 1;
            GM2.onChangeDip();
            GM2.onCPChange(5);
        }
        else
        {
            hand2.RemoveAt(0);
            chosenCard = "";
            onChosenCard();
            diplomacyState[2, 3] = 1;
            GM2.onChangeDip();
            GM2.onCPChange(5);
        }
        highlightSelected = -1;
        currentTextObject.reset();
        
    }

    public IEnumerator HIS004()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        int randomIndex = UnityEngine.Random.Range(1, 7);
        int bonus = 0;
        if (spacesGM.ElementAt(76).controlPower == 2)
        {
            bonus = bonus + 2;
        }
        if (spacesGM.ElementAt(77).controlPower == 2)
        {
            bonus++;
        }
        int italian = 0;
        bool frenchHome = false;
        bool foreignReg = false;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).controlPower == 2 && spaces.ElementAt(i).language == (Language)3)
            {
                italian++;
            }
            if (spacesGM.ElementAt(i).controlPower != 3 && spaces.ElementAt(i).homePower == (PowerType2)3 && (spaces.ElementAt(i).spaceType == (SpaceType)2 || spaces.ElementAt(i).spaceType == (SpaceType)3))
            {
                frenchHome = true;
            }
            if (spacesGM.ElementAt(i).controlPower != 3 && spaces.ElementAt(i).homePower == (PowerType2)3 && (spaces.ElementAt(i).spaceType == (SpaceType)2 || spaces.ElementAt(i).spaceType == (SpaceType)3) && spacesGM.ElementAt(i).regular > 0)
            {
                foreignReg = true;
            }
        }
        if (italian >= 3)
        {
            bonus = bonus + 2;
        }
        //no touranament scenario for now
        if (frenchHome)
        {
            bonus--;
        }
        if (foreignReg)
        {
            bonus = bonus - 2;
        }
        int total = randomIndex + bonus;
        if (total > 2)
        {
            //updateVP
            GM1.chateauxC++;
            GM1.updateVP();
            GM2.onVP();

        }
        hand3.RemoveAt(0);
        if (bonus < 0)
        {
            if (total < 3 || total > 7)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + bonus.ToString() + "=" + total.ToString() + "\nDraw 2 cards, then discard 1.");

                hand3.AddRange(activeCards.GetRange(0, 2));
                activeCards.RemoveRange(0, 2);
                GM2.boolStates[0] = true;
            }
            else if (total == 3 || total == 4)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card, then discard 1.");
                GM2.boolStates[0] = true;
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            else if (total > 5)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card.");
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }

        }
        else
        {
            if (total < 3 || total > 7)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + "+" + bonus.ToString() + "=" + total.ToString() + "\nDraw 2 cards, then discard 1.");
                hand3.AddRange(activeCards.GetRange(0, 2));
                activeCards.RemoveRange(0, 2);
                GM2.boolStates[0] = true;
            }
            else if (total == 3 || total == 4)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + "+" + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card, then discard 1.");
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
                GM2.boolStates[0] = true;
            }
            else if (total > 5)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + "+" + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card.");
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
        }
        while (GM2.boolStates[0])
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        currentTextObject.restartColor();
        currentTextObject.reset();
        chosenCard = "";
        onChosenCard();


    }

    public IEnumerator HIS006()
    {
        InputToggleObject inputToggleObject = GameObject.Find("InputToggle").GetComponent("InputToggleObject") as InputToggleObject;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        currentTextObject.pauseColor();
        currentTextObject.post("Specify your own debater?\nSpecify language zone.");
        inputToggleObject.post();
        inputNumberObject.post();
        DebateNScript debateNScript = GameObject.Find("DebateNext").GetComponent("DebateNScript") as DebateNScript;
        debateNScript.post();
        while (debateNScript.btn.interactable == true)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        currentTextObject.reset();
        inputNumberObject.reset();
        inputToggleObject.reset();
        currentTextObject.restartColor();
        chosenCard = "";
        onChosenCard();
        hand4.RemoveAt(0);
    }

    public IEnumerator HIS008()
    {
        GM1.enq1("Protestant player to pick 5 target spaces");
        activeReformers.Add(reformers.ElementAt(0));
        GameObject tempObject = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Objects/Reformer4/Luther"), new Vector3(spaces.ElementAt(0).posX + 965, spaces.ElementAt(0).posY + 545, 0), Quaternion.identity);
        tempObject.transform.SetParent(GameObject.Find("Reformers").transform);
        tempObject.name = "Luther";
        tempObject.SetActive(true);
        highlightSelected = 0;
        changeReligion();
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        currentTextObject.post("Pick 5 highlighted target spaces");
        for (int i = 0; i < 5; i++)
        {



            List<int> pickSpaces = highlightReformation();
            highlightSelected = -1;
            onNoLayer();
            onHighlight(pickSpaces);

            onHighlightSelected += reformAttempt;
            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }

            UnityEngine.Debug.Log("end");
            //onRemoveHighlight(converted);
        }
        GM1.deq1(1);
        GM1.enq2("Any player to go to phase 2");
        yield return new WaitForSeconds(3);
        currentTextObject.reset();
        currentTextObject.restartColor();
        highlightSelected = -1;
        chosenCard = "";
        onChosenCard();
        //remove Luther's 95 theses from backend decks
        //cards.RemoveAt(7);
        hand5.RemoveAt(0);
        GM2.onPhaseEnd();
    }

    public void HIS009()
    {

        regulars[111] = 2;
        regularsPower[111] = 0;
        spacesGM.ElementAt(111).controlMarker = 3;
        spacesGM.ElementAt(111).controlPower = 0;
        spacesGM.ElementAt(111).regular = 2;
        spacesGM.ElementAt(111).corsair = 2;
        spacesGM.ElementAt(111).leader1 = 17;
        onChangeReg(111, 0);
        onChangeCorsair(111);
        onChangeLeader(111, 17);
        //mandatory event by turn 3
        GM2.boolStates[3] = true;
    }

    public void HIS010()
    {
        GM1.updateRuler(4, 10);
        onChangeRuler(4, 10);
        //mandatory event by turn 2
        GM2.boolStates[5] = true;
    }

    public void HIS014()
    {
        GM1.updateRuler(4, 14);
        onChangeRuler(4, 14);
        //mandatory event by turn 4
        GM2.boolStates[7] = true;
    }

    public void HIS016()
    {
        GM1.updateRuler(5, 16);
        onChangeRuler(5, 16);
    }

    public void HIS018()
    {

        int pos = -1;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).leader1 == 17 || spacesGM.ElementAt(i).leader2 == 17)
            {
                pos = i;
                spacesGM.ElementAt(i).removeLeader(17);
                spacesGM.ElementAt(i).addLeader(18);
                break;
            }
        }
        if (pos != -1)
        {
            onChangeLeader(-1, 17);
            onChangeLeader(pos, 18);
        }
        //todo: remove this card from deck
    }

    public void HIS019()
    {
        GM1.updateRuler(2, 19);
        onChangeRuler(2, 19);
        int pos = -1;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).leader1 == 8 || spacesGM.ElementAt(i).leader2 == 8)
            {
                pos = i;
                spacesGM.ElementAt(i).removeLeader(8);
                spacesGM.ElementAt(i).addLeader(3);
                break;
            }
        }
        UnityEngine.Debug.Log("position" + pos.ToString());
        if (pos != -1)
        {
            onChangeLeader(-1, 8);
            onChangeLeader(pos, 3);
            //todo: debug overlapping leaders
        }
        else
        {
            onChangeLeader(27, 3);
        }
    }

    public void HIS020()
    {
        GM1.updateRuler(3, 20);
        onChangeRuler(3, 20);
        int pos = -1;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).leader1 == 6 || spacesGM.ElementAt(i).leader2 == 6)
            {
                pos = i;
                spacesGM.ElementAt(i).removeLeader(6);
                spacesGM.ElementAt(i).addLeader(7);
                break;
            }
        }
        if (pos != -1)
        {
            onChangeLeader(-1, 6);
            onChangeLeader(pos, 7);
        }
        else
        {
            onChangeLeader(41, 7);
        }
    }

    public void HIS021()
    {
        if (GM1.rulers[2].name == "Henry VIII")
        {
            int pos = -1;
            for (int i = 0; i < spacesGM.Count(); i++)
            {
                if (spacesGM.ElementAt(i).leader1 == 8 || spacesGM.ElementAt(i).leader2 == 8)
                {
                    pos = i;
                    spacesGM.ElementAt(i).removeLeader(8);
                    spacesGM.ElementAt(i).addLeader(3);
                    break;
                }
            }
            UnityEngine.Debug.Log("position" + pos.ToString());
            if (pos != -1)
            {
                onChangeLeader(-1, 8);
                onChangeLeader(pos, 3);

            }
            else
            {
                onChangeLeader(27, 3);
            }
        }



        GM1.updateRuler(2, 21);
        onChangeRuler(2, 21);
        //todo: other functionalities
    }

    public void HIS022()
    {
        GM1.updateRuler(4, 22);
        onChangeRuler(4, 22);
    }

    public void HIS023()
    {
        GM1.updateRuler(2, 23);
        onChangeRuler(2, 23);
    }

    public void HIS024()
    {

        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        NavalMvmt navalMvmt = GameObject.Find("ProcedureButton").GetComponent("NavalMvmt") as NavalMvmt;
        if (GM2.boolStates[28])
        {
            if (GM1.player == landMvmt.mvmtPlayer)
            {
                landMvmt.attackerDice += 2;
            }
            else
            {
                landMvmt.defenderDice += 2;
            }
        }
        else if (GM2.boolStates[31])
        {
            if (GM1.player == navalMvmt.mvmtPlayer)
            {
                navalMvmt.attackerDice[0] += 2;
            }
            else
            {
                navalMvmt.defenderDice[0] += 2;
            }
        }

        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 24);
        if (GM2.boolStates[28])
        {
            landMvmt.required2();
        }
        else if (GM2.boolStates[31])
        {
            navalMvmt.status = 7;
            navalMvmt.btn.interactable = false;
            navalMvmt.required2();
        }

    }

    public void HIS025()
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        if (GM1.player == landMvmt.mvmtPlayer)
        {
            if (GM1.player == 0 || GM1.player == 3)
            {
                landMvmt.attackerDice += 3;
            }
            else
            {
                landMvmt.attackerDice += 2;
            }

        }
        else
        {
            if (GM1.player == 0 || GM1.player == 3)
            {
                landMvmt.defenderDice += 3;
            }
            else
            {
                landMvmt.defenderDice += 2;
            }
        }
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 25);
        landMvmt.required2();
    }

    public void HIS026()
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        int changeNumber = 0;
        if (GM1.player == landMvmt.mvmtPlayer)
        {
            changeNumber = (spacesGM.ElementAt(landMvmt.destination).merc + 1) / 2;
            spacesGM.ElementAt(landMvmt.destination).merc -= changeNumber;
            spacesGM.ElementAt(landMvmt.initial).merc += changeNumber;
        }
        else
        {
            changeNumber = (spacesGM.ElementAt(landMvmt.initial).merc + 1) / 2;
            spacesGM.ElementAt(landMvmt.destination).merc += changeNumber;
            spacesGM.ElementAt(landMvmt.initial).merc -= changeNumber;
        }
        onChangeMerc(landMvmt.initial, spacesGM.ElementAt(landMvmt.initial).regularPower);
        onChangeMerc(landMvmt.destination, spacesGM.ElementAt(landMvmt.destination).regularPower);
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 26);
        landMvmt.required2();
    }

    public void HIS027()
    {
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        spacesGM.ElementAt(siegeScript.initial).merc = 0;
        onChangeMerc(siegeScript.initial, spacesGM.ElementAt(siegeScript.initial).regularPower);
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 27);
        GM1.player = siegeScript.mvmtPlayer;
        GM2.onPlayerChange();
        if (spacesGM.ElementAt(siegeScript.initial).regular + spacesGM.ElementAt(siegeScript.initial).cavalry <= spacesGM.ElementAt(siegeScript.initial).regular + spacesGM.ElementAt(siegeScript.initial).merc)
        {
            siegeScript.status = 6;

        }
        siegeScript.required2();
    }

    public void HIS028()
    {
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        siegeScript.attackerDice += 3;
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 28);
        GM1.player = siegeScript.mvmtPlayer;
        GM2.onPlayerChange();
        siegeScript.required2();
    }

    public void HIS029()
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        landMvmt.has29 = GM1.player;
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 29);
        landMvmt.required2();
    }

    public void HIS030()
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        landMvmt.has29 = GM1.player;
        if (GM1.player != 1)
        {
            if (GM1.player == landMvmt.mvmtPlayer)
            {
                landMvmt.defenderDice = Math.Max(0, landMvmt.defenderDice - 3);
            }
            else
            {
                landMvmt.attackerDice = Math.Max(0, landMvmt.attackerDice - 3);
            }
        }
        else
        {
            landMvmt.has30 = true;

        }
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 30);
        landMvmt.required2();
    }

    public void HIS031()
    {
        GM2.boolStates[29] = true;
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        NavalMvmt navalMvmt = GameObject.Find("ProcedureButton").GetComponent("NavalMvmt") as NavalMvmt;
        if (landMvmt.status == 3)
        {
            GM2.intStates[0] = landMvmt.mvmtPlayer;
        }
        else if (GM2.boolStates[30])
        {
            GM2.intStates[0] = siegeScript.mvmtPlayer;
        }
        else
        {
            GM2.intStates[0] = navalMvmt.mvmtPlayer;
        }

        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        if (textScript.displayCP >= 1)
        {
            GM2.onCPChange(textScript.displayCP - 1);
            HighlightCPScript highlightCPScript = GameObject.Find("HighlightCPDisplay").GetComponent("HighlightCPScript") as HighlightCPScript;
            highlightCPScript.removeHighlight();
        }
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 31);
        if (landMvmt.status == 3)
        {
            GM1.player = landMvmt.mvmtPlayer;
            GM2.onPlayerChange();
            landMvmt.status = 4;
            landMvmt.required2();
        }
        else if (GM2.boolStates[30])
        {
            GM1.player = siegeScript.mvmtPlayer;
            GM2.onPlayerChange();
            siegeScript.status = 3;
            siegeScript.required2();
        }
        else
        {
            GM1.player = navalMvmt.mvmtPlayer;
            GM2.onPlayerChange();
            navalMvmt.status = 3;
            navalMvmt.required2();
        }

    }

    public void HIS032()
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        landMvmt.hasLeader = false;
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        if (textScript.displayCP >= 1)
        {
            GM2.onCPChange(textScript.displayCP - 1);
            HighlightCPScript highlightCPScript = GameObject.Find("HighlightCPDisplay").GetComponent("HighlightCPScript") as HighlightCPScript;
            highlightCPScript.removeHighlight();
        }
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 32);
        if (landMvmt.status == 3)
        {
            GM1.player = landMvmt.mvmtPlayer;
            GM2.onPlayerChange();
            landMvmt.status = 5;
            landMvmt.required2();
        }
        else
        {
            GM1.player = siegeScript.mvmtPlayer;
            GM2.onPlayerChange();
            siegeScript.status = 4;
            siegeScript.required2();
        }
    }

    public IEnumerator HIS033()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        if (GM1.player == 1)
        {

            currentTextObject.post("Add 4 mercenaries");
            for (int i = 0; i < 4; i++)
            {
                List<int> pickSpaces = findClearFormation(GM1.player);
                highlightSelected = -1;
                onMercLayer();
                onHighlight(pickSpaces);
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }
                spacesGM.ElementAt(highlightSelected).merc++;
                onChangeMerc(highlightSelected, GM1.player);
                if (siegeScript.status == 4 && highlightSelected == siegeScript.initial)
                {
                    siegeScript.attackerDice++;
                }
            }

            highlightSelected = -1;
        }
        else if (GM1.player == 0)
        {
            currentTextObject.post("Add 2 mercenaries");
            for (int i = 0; i < 2; i++)
            {
                List<int> pickSpaces = new List<int>();
                for (int j = 0; j < 134; i++)
                {
                    if (spacesGM.ElementAt(j).merc > 0)
                    {
                        pickSpaces.Add(j);
                    }
                }
                highlightSelected = -1;
                onMercLayer();
                onHighlight(pickSpaces);
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }
                if (spacesGM.ElementAt(highlightSelected).merc == 1)
                {
                    spacesGM.ElementAt(highlightSelected).merc = 0;
                }
                else
                {
                    spacesGM.ElementAt(highlightSelected).merc--;
                }
                onChangeMerc(highlightSelected, spacesGM.ElementAt(highlightSelected).regularPower);
                if (siegeScript.status == 4 && highlightSelected == siegeScript.initial)
                {
                    siegeScript.attackerDice++;
                }
            }

            highlightSelected = -1;
        }
        else
        {
            currentTextObject.post("Add 2 mercenaries");
            for (int i = 0; i < 2; i++)
            {
                List<int> pickSpaces = findClearFormation(GM1.player);
                highlightSelected = -1;
                onMercLayer();
                onHighlight(pickSpaces);
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }
                spacesGM.ElementAt(highlightSelected).merc++;
                onChangeMerc(highlightSelected, GM1.player);
                if (siegeScript.status == 4 && highlightSelected == siegeScript.initial)
                {
                    siegeScript.attackerDice++;
                }
            }

            highlightSelected = -1;
        }
        currentTextObject.reset();
        currentTextObject.restartColor();
        DeckScript.discardById(GM1.player, 33);
        chosenCard = "";
        onChosenCard();
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;

        if (landMvmt.status == 7)
        {
            landMvmt.status = 8;
            landMvmt.required2();
        }
    }

    public IEnumerator HIS034()
    {
        //todo: first option
        NavalMvmt navalMvmt = GameObject.Find("ProcedureButton").GetComponent("NavalMvmt") as NavalMvmt;
        List<int> trace = new List<int> { navalMvmt.initial[0], navalMvmt.destination[0] };
        highlightSelected = -1;
        onNavalLayer();
        onHighlight(trace);
        while (highlightSelected == -1)
        {
            //UnityEngine.Debug.Log("here");
            yield return null;
        }

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);
            if (randomIndex >= 5)
            {
                if (highlightSelected == navalMvmt.initial[0])
                {
                    navalMvmt.attackerHit++;
                }
                else
                {
                    navalMvmt.defenderHit++;


                }
            }
        }
        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 34);
        GM1.player = navalMvmt.mvmtPlayer;
        GM2.onPlayerChange();
        navalMvmt.status = 10;
        navalMvmt.required2();
    }

    public void HIS035()
    {
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        siegeScript.attackerDice += 2;
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);
            if (randomIndex >= 2)
            {
                siegeScript.attackerHit++;
            }
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Attacker hit: " + siegeScript.attackerHit.ToString() + " out of " + siegeScript.attackerDice.ToString() + ".\nDefender hit: " + siegeScript.defenderHit.ToString() + " out of " + siegeScript.defenderDice.ToString() + ".");

        chosenCard = "";
        onChosenCard();
        DeckScript.discardById(GM1.player, 35);
        GM1.player = siegeScript.mvmtPlayer;
        GM2.onPlayerChange();
        siegeScript.required2();
    }

    public IEnumerator HIS036()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        SiegeScript siegeScript = GameObject.Find("ProcedureButton").GetComponent("SiegeScript") as SiegeScript;
        if (GM1.player == 0 || GM1.player == 3)
        {
            currentTextObject.post("Add 4 mercenaries");
            for (int i = 0; i < 4; i++)
            {
                List<int> pickSpaces = findClearFormation(3);
                highlightSelected = -1;
                onMercLayer();
                onHighlight(pickSpaces);
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }
                spacesGM.ElementAt(highlightSelected).merc++;
                onChangeMerc(highlightSelected, 3);
                if (siegeScript.status == 4 && highlightSelected == siegeScript.initial)
                {
                    siegeScript.attackerDice++;
                }
            }

            highlightSelected = -1;
        }
        else
        {
            currentTextObject.post("Add 2 mercenaries");
            for (int i = 0; i < 2; i++)
            {
                List<int> pickSpaces = findClearFormation(GM1.player);
                highlightSelected = -1;
                onMercLayer();
                onHighlight(pickSpaces);
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }

                spacesGM.ElementAt(highlightSelected).merc++;

                onChangeMerc(highlightSelected, GM1.player);
                if (siegeScript.status == 4 && highlightSelected == siegeScript.initial)
                {
                    siegeScript.attackerDice++;
                }
            }

            highlightSelected = -1;
        }
        currentTextObject.reset();
        currentTextObject.restartColor();
        DeckScript.discardById(GM1.player, 36);
        chosenCard = "";
        onChosenCard();
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;

        if (landMvmt.status == 8)
        {
            landMvmt.status = 9;
            landMvmt.required2();
        }
    }

    public IEnumerator HIS065()
    {
        if (DeckScript.debaters.ElementAt(12).status == (DebaterStatus)1)
        {
            DeckScript.debaters.ElementAt(12).status = (DebaterStatus)2;
            CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
            currentTextObject.pauseColor();
            currentTextObject.post("Pick 6 highlighted target spaces");
            for (int i = 0; i < 5; i++)
            {


                List<int> pickSpaces = highlightReformation();
                highlightSelected = -1;
                onSpaceLayer();
                onHighlight(pickSpaces);

                onHighlightSelected += reformAttempt;
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }

                UnityEngine.Debug.Log("end");
                //onRemoveHighlight(converted);
            }
            yield return new WaitForSeconds(3);
            DeckScript.discardById(GM1.player, 65);
            currentTextObject.reset();
            currentTextObject.restartColor();
            highlightSelected = -1;
            chosenCard = "";
            onChosenCard();
        }
    }

    public IEnumerator HIS078()
    {
        for (int i = 0; i < 2; i++)
        {
            HashSet<int> searchIndex = new HashSet<int>();
            HashSet<int> pickSpaces = new HashSet<int>();
            searchIndex.Add(0);
            while (pickSpaces.Count == 0)
            {
                HashSet<int> temp = searchIndex;
                searchIndex.Clear();
                for (int k = 0; k < searchIndex.Count(); k++)
                {
                    for (int j = 0; j < spaces.ElementAt(searchIndex.ElementAt(k)).adjacent.Count(); j++)
                    {

                        if (!spacesGM.ElementAt(spaces.ElementAt(searchIndex.ElementAt(k)).adjacent[j] - 1).unrest && religiousInfluence[spaces.ElementAt(searchIndex.ElementAt(k)).adjacent[j] - 1] == 0)
                        {
                            pickSpaces.Add(spaces.ElementAt(searchIndex.ElementAt(k)).adjacent[j] - 1);
                        }
                        else
                        {
                            searchIndex.Add(spaces.ElementAt(searchIndex.ElementAt(k)).adjacent[j] - 1);
                        }


                    }
                }


            }
            highlightSelected = -1;
            onSpaceLayer();
            onHighlight(pickSpaces.ToList());

            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }
            GM2.changeReligion();
        }
        highlightSelected = -1;
        DeckScript.discardById(GM1.player, 78);
        foreach (CardObject card in DeckScript.discardCards)
        {
            if (card.id == 37)
            {
                DeckScript.discardCards.Remove(card);
                DeckScript.hand5.Add(card);
            }
        }
        chosenCard = "";
        onChosenCard();
    }

    public IEnumerator HIS080()
    {
        for (int i = 0; i < 2; i++)
        {
            List<int> pickSpaces = findUnoccupied(findTrace(3));
            highlightSelected = -1;
            onSpaceLayer();
            onHighlight(pickSpaces);

            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }
            spacesGM.ElementAt(highlightSelected).unrest = true;
            GM2.onChangeUnrest(highlightSelected);
        }
        highlightSelected = -1;
        DeckScript.discardById(GM1.player, 80);
        chosenCard = "";
        onChosenCard();
    }

    public IEnumerator HIS081()
    {
        int randomUpper = DeckScript.hand5.Count();
        int CP;
        int randomIndex;
        if (DeckScript.hand5.ElementAt(0).id == 7)
        {
            randomIndex = UnityEngine.Random.Range(1, randomUpper);
        }
        else
        {
            randomIndex = UnityEngine.Random.Range(0, randomUpper);
        }
        CP = DeckScript.hand5.ElementAt(randomIndex).CP;
        DeckScript.discardById(5, DeckScript.hand5.ElementAt(randomIndex).id);
        GM1.StPeters[0] += CP;
        if (GM1.StPeters[0] >= 5)
        {
            GM1.StPeters[1]++;
            GM1.StPeters[0] -= 5;
        }
        GM1.updateVP();
        GM2.onVP();
        yield return new WaitForSeconds(3);
        DeckScript.discardById(GM1.player, 81);
        chosenCard = "";
        onChosenCard();
    }

    public IEnumerator HIS082()
    {
        int index = 2;
        if (GM1.diplomacyState[0, 1] != 1 && GM1.diplomacyState[0, 2] != 1 && GM1.diplomacyState[0, 3] != 1 && GM1.diplomacyState[0, 4] != 1)
        {
            index = 4;
        }
        for (int i = 0; i < index; i++)
        {


            List<int> pickSpaces = findUnoccupied(findTrace(0));
            highlightSelected = -1;
            onSpaceLayer();
            onHighlight(pickSpaces);

            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }
            spacesGM.ElementAt(highlightSelected).unrest = true;
            GM2.onChangeUnrest(highlightSelected);
        }
        highlightSelected = -1;
        DeckScript.discardById(GM1.player, 82);
        chosenCard = "";
        onChosenCard();
    }


    public void HIS083()
    {
        regulars[115] += 4;
        spacesGM.ElementAt(115).regular += 4;
        onChangeReg(115, spacesGM.ElementAt(115).controlPower);
        DeckScript.removeCard(GM1.player, 83);

        chosenCard = "";
        onChosenCard();
    }

    public IEnumerator HIS085()
    {
        DeckScript.debaters.ElementAt(12).status = (DebaterStatus)2;
        DebatersScript debatersScript = GameObject.Find("DebaterDisplay").GetComponent("DebatersScript") as DebatersScript;
        debatersScript.updateDebater();
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        currentTextObject.post("Pick 5 highlighted target spaces");
        for (int i = 0; i < 5; i++)
        {



            List<int> pickSpaces = highlightReformation();
            highlightSelected = -1;
            onNoLayer();
            onHighlight(pickSpaces);

            onHighlightSelected += reformAttempt;
            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }

            UnityEngine.Debug.Log("end");
            //onRemoveHighlight(converted);
        }
        highlightSelected = -1;
        yield return new WaitForSeconds(3);
        currentTextObject.reset();
        currentTextObject.restartColor();
        DeckScript.removeCard(GM1.player, 85);

        chosenCard = "";
        onChosenCard();
    }

    public IEnumerator HIS088()
    {
        for (int i = 0; i < 5; i++)
        {


            List<int> pickSpaces = findUnoccupied(findLanguage(2));
            highlightSelected = -1;
            onSpaceLayer();
            onHighlight(pickSpaces);

            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }
            spacesGM.ElementAt(highlightSelected).unrest = true;
            GM2.onChangeUnrest(highlightSelected);
        }
        highlightSelected = -1;
        DeckScript.discardById(GM1.player, 88);
        chosenCard = "";
        onChosenCard();
    }

    public IEnumerator HIS094()
    {
        for (int i = 0; i < 3; i++)
        {


            List<int> pickSpaces = findUnoccupied(findLanguage(4));
            highlightSelected = -1;
            onSpaceLayer();
            onHighlight(pickSpaces);

            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }
            spacesGM.ElementAt(highlightSelected).unrest = true;
            GM2.onChangeUnrest(highlightSelected);
        }
        highlightSelected = -1;
        DeckScript.discardById(GM1.player, 94);
        chosenCard = "";
        onChosenCard();
    }

    public void HIS109()
    {
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
        DeckScript.discardById(GM1.player, 109);
        chosenCard = "";
        onChosenCard();
    }
}
