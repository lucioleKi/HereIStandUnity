using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM3;
using static GraphUtils;
using TMPro;
using UnityEngine.UI;


public class GM2 : MonoBehaviour
{
    public static GM2 instance;
    public static GM2 Instance
    {
        get
        {
            if (instance == null)
            {
                UnityEngine.Debug.Log("GM2 not initiated.");
            }
            return instance;
        }
    }

    public static string chosenCard = "";

    public delegate void SimpleHandler();
    public static SimpleHandler on8;
    public static SimpleHandler onVP;
    public static SimpleHandler onPhase2;
    public static SimpleHandler onPhase3;
    public static SimpleHandler onPhase4;
    public static SimpleHandler onPhase5;
    public static SimpleHandler onPhase6;
    public static SimpleHandler onHighlightSelected;
    public static SimpleHandler onChangeDip;
    public static SimpleHandler onChangePhase;
    public static SimpleHandler onChosenCard;
    public static SimpleHandler onPlayerChange;
    public static SimpleHandler onMoveHome25;
    public static SimpleHandler onSpaceLayer;
    public static SimpleHandler onRegLayer;
    public static SimpleHandler onLeaderLayer;
    public static SimpleHandler onNoLayer;
    public static SimpleHandler onLeaderULayer;
    public static SimpleHandler onRemoveHighlight;
    public static SimpleHandler onChangeSegment;

    public delegate void Int2Handler(int index1, int index2);

    //(card index = id - 1, power)
    public static Int2Handler onChangeReg;
    public static Int2Handler onChangeMerc;
    public static Int2Handler onChangeCav;
    public static Int2Handler onChangeSquadron;
    public static Int2Handler onChangeLeader;
    public static Int2Handler onChangeRuler;
    public delegate void Int3Handler(int index1, int index2, int index3);
    public static Int3Handler onAddSpace;
    public static Int3Handler onFlipSpace;

    public delegate void Int1Handler(int index);
    public static Int1Handler onRemoveSpace;
    public static Int1Handler onAddReformer;
    public static Int1Handler onConfirmDipForm;
    public static Int1Handler onConfirmPeace;
    public static Int1Handler onCPChange;
    public static Int1Handler onMandatory;
    public static Int1Handler onChangeCorsair;
    public static Int1Handler onHighlightRectangles;
    public delegate void List1Handler(List<int> index);
    public static List1Handler onHighlight;


    public static bool[] boolStates;
    //0: waitCard for HIS003. 1: phaseEnd. 2: theological debate (CP action). 3: is piracy allowed. 4: Henry VIII marries Anne Boleyn
    //public static bool waitCard = false;
    public static int highlightSelected = -1;
    public static int leaderSelected = -1;
    //public static bool phaseEnd = false;
    public static int currentCP = 0;
    public static int[] secretCP = new int[6];

    GM3 gm3;

    //
    void OnEnable()
    {
        boolStates = new bool[10];
        onMandatory += mandatory;
        onPhase2 += phase2;
        onPhase3 += phase3;
        onPhase4 += phase4;
        onPhase5 += phase5;
        onPhase6 += phase6;
    }


    void OnDisable()
    {
        onMandatory -= mandatory;
        onPhase2 -= phase2;
        onPhase3 -= phase3;
        onPhase4 -= phase4;
        onPhase5 -= phase5;
        onPhase6 -= phase6;
    }

    /*if (onMoveHome25 != null)
        {
            UnityEngine.Debug.Log("Here");
            onMoveHome25?.Invoke(1, 2);
}
    */





    void mandatory(int index)
    {
        if (index > 8)
        {
            discardCard(index);
        }
        switch (index)
        {
            case 1:
                StartCoroutine(gm3.HIS001B());
                break;
            case 2:
                StartCoroutine(gm3.HIS002());
                break;
            case 4:
                StartCoroutine(gm3.HIS004());
                break;
            case 6:
                StartCoroutine(gm3.HIS006());
                break;
            case 8:
                StartCoroutine(gm3.HIS008());
                break;
            case 9:
                gm3.HIS009();
                break;
            case 10:
                gm3.HIS010();
                break;
            case 14:
                gm3.HIS014();
                break;
            case 16:
                gm3.HIS016();
                break;
            case 18:
                gm3.HIS018();
                break;
            case 19:
                gm3.HIS019();
                break;
            case 20:
                gm3.HIS020();
                break;
            case 21:
                gm3.HIS021();
                break;
            case 22:
                gm3.HIS022();
                break;
            case 65:
                StartCoroutine(gm3.HIS065());
                break;
            default:
                break;
        }
        //player gets 2 CP after the event
    }

    public static void theologicalDebate()
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


    }

    public static void reformAttempt()
    {
        onHighlightSelected -= reformAttempt;
        //1. pick target space


        UnityEngine.Debug.Log(highlightSelected);
        int target = highlightSelected;

        SpaceObject targetSpace = spaces.ElementAt(target);
        //2. add up reformer dice
        int reformerDice = 1;
        int papalDice = 1;
        //+2 if reformer in target space, +1 if reformer adjacent
        for (int i = 0; i < activeReformers.Count(); i++)
        {
            if (activeReformers.ElementAt(i).space == target)
            {
                reformerDice = reformerDice + 2;
            }
            else
            {

                SpaceObject tempSpace = spaces.ElementAt(activeReformers.ElementAt(i).space);
                for (int j = 0; j < tempSpace.adjacent.Count(); j++)
                {
                    if (tempSpace.adjacent.ElementAt(j) == targetSpace.id)
                    {
                        reformerDice++;
                    }
                }
            }
        }

        //+1 for every adjacent under protestant/catholic religious influence 
        for (int i = 0; i < spaces.ElementAt(target).adjacent.Count; i++)
        {
            if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)] == (Religion)1)
            {
                reformerDice++;
            }
            else if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)] == (Religion)0)
            {
                papalDice++;
            }
        }

        //+2 if protestant land units, +1 if land units adjacent
        if (regulars[target] > 0 && spacesGM.ElementAt(target).controlPower == 5)
        {
            reformerDice = reformerDice + 2;
        }
        else if (regulars[target] > 0 && spacesGM.ElementAt(target).controlPower == 4)
        {
            papalDice = papalDice + 2;
        }
        for (int i = 0; i < targetSpace.adjacent.Count(); i++)
        {
            if (regulars[targetSpace.adjacent.ElementAt(i)] > 0 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 5)
            {
                reformerDice++;
            }
            else if (regulars[targetSpace.adjacent.ElementAt(i)] > 0 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 4)
            {
                papalDice++;
            }
        }


        //3. add bonus dice
        //todo: printing press event and debater bonus
        if (turn == 1 && phase == 1)
        {
            reformerDice++;
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;


        //4. roll dice
        int dice1 = 0;
        for (int i = 0; i < reformerDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);

            if (dice1 < randomIndex)
            {
                dice1 = randomIndex;
            }
            if (dice1 == 6)
            {
                //UnityEngine.Debug.Log("6!");
                changeReligion();
                currentTextObject.post("Reformer dices: " + reformerDice.ToString() + ". Highest: 6. \nAutomatic success.");
                //send signal to various parties
                return;
            }
        }


        //5. add up papal dice



        //6. roll papal dice
        int dice2 = 0;
        for (int i = 0; i < papalDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);

            if (dice2 < randomIndex)
            {
                dice2 = randomIndex;
            }
        }

        //7. determine result
        if (dice1 >= dice2)
        {
            UnityEngine.Debug.Log("win");
            changeReligion();
            currentTextObject.post("Reformer dices: " + reformerDice.ToString() + ". Highest: " + dice1.ToString() + ".\nPapal dices: " + papalDice.ToString() + ". Highest: " + dice2.ToString() + "\nSuccessful reformation attempt.");

            //send signal to various parties

            return;
        }
        else
        {
            UnityEngine.Debug.Log("lose");
            currentTextObject.post("Reformer dices: " + reformerDice.ToString() + ". Highest: " + dice1.ToString() + ".\nPapal dices: " + papalDice.ToString() + ". Highest: " + dice2.ToString() + "\nFailed reformation attempt.");
            return;
        }
    }





    

    public static void reformCAttempt()
    {
        onHighlightSelected -= reformCAttempt;
        //1. pick target space


        UnityEngine.Debug.Log(highlightSelected);
        int target = highlightSelected;

        SpaceObject targetSpace = spaces.ElementAt(target);
        //2. add up reformer dice
        int reformerDice = 1;
        int papalDice = 1;
        //+2 if reformer in target space, +1 if reformer adjacent
        for (int i = 0; i < activeReformers.Count(); i++)
        {
            if (activeReformers.ElementAt(i).space == target)
            {
                reformerDice = reformerDice + 2;
            }
            else
            {

                SpaceObject tempSpace = spaces.ElementAt(activeReformers.ElementAt(i).space);
                for (int j = 0; j < tempSpace.adjacent.Count(); j++)
                {
                    if (tempSpace.adjacent.ElementAt(j) == targetSpace.id)
                    {
                        reformerDice++;
                    }
                }
            }
        }

        //+1 for every adjacent under protestant/catholic religious influence 
        for (int i = 0; i < spaces.ElementAt(target).adjacent.Count; i++)
        {
            if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)] == (Religion)1)
            {
                reformerDice++;
            }
            else if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)] == (Religion)0)
            {
                papalDice++;
            }
        }

        //+2 if protestant land units, +1 if land units adjacent
        if (regulars[target] > 0 && spacesGM.ElementAt(target).controlPower == 5)
        {
            reformerDice = reformerDice + 2;
        }
        else if (regulars[target] > 0 && spacesGM.ElementAt(target).controlPower == 4)
        {
            papalDice = papalDice + 2;
        }
        for (int i = 0; i < targetSpace.adjacent.Count(); i++)
        {
            if (regulars[targetSpace.adjacent.ElementAt(i)] > 0 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 5)
            {
                reformerDice++;
            }
            else if (regulars[targetSpace.adjacent.ElementAt(i)] > 0 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 4)
            {
                papalDice++;
            }
        }


        //3. add bonus dice

        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;


        //4. roll papal dice
        int dice2 = 0;
        for (int i = 0; i < papalDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);

            if (dice2 < randomIndex)
            {
                dice2 = randomIndex;
            }
            if (dice2 == 6 && (GM1.rulers[5].name == "Paul III" || GM1.rulers[5].name == "Julius III"))
            {
                //UnityEngine.Debug.Log("6!");
                changeReligion();
                currentTextObject.post("Catholic dices: " + papalDice.ToString() + ". Highest: 6. \nAutomatic success.");
                //send signal to various parties
                return;
            }
        }

        //7. roll protestant dice
        int dice1 = 0;
        for (int i = 0; i < reformerDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);

            if (dice1 < randomIndex)
            {
                dice1 = randomIndex;
            }

        }



        //8. determine result
        if (dice2 >= dice1)
        {
            UnityEngine.Debug.Log("win");
            changeReligion();
            currentTextObject.post("Catholic dices: " + papalDice.ToString() + ". Highest: " + dice2.ToString() + ".\nProtestant dices: " + reformerDice.ToString() + ". Highest: " + dice1.ToString() + "\nSuccessful counter-reformation.");

            //send signal to various parties

            return;
        }
        else
        {
            UnityEngine.Debug.Log("lose");
            currentTextObject.post("Catholic dices: " + papalDice.ToString() + ". Highest: " + dice2.ToString() + ".\nProtestant dices: " + reformerDice.ToString() + ". Highest: " + dice1.ToString() + "\nSuccessful counter-reformation.");
            return;
        }
    }

    

    void phase2()
    {
        instanceDeck.addActive(turn);
        activeCards.AddRange(discardCards);
        discardCards.Clear();
        instanceDeck.Shuffle();
        for (int i = 0; i < 6; i++)
        {
            List<CardObject> tempHand = new List<CardObject>();
            int temp = drawNumber(i);
            switch (i)
            {
                case 0:
                    hand0.Add(cards.ElementAt(0));
                    hand0.AddRange(activeCards.GetRange(0, temp));

                    break;
                case 1:
                    hand1.Add(cards.ElementAt(1));
                    hand1.AddRange(activeCards.GetRange(0, temp));
                    break;
                case 2:
                    hand2.Add(cards.ElementAt(2));
                    hand2.AddRange(activeCards.GetRange(0, temp));
                    break;
                case 3:
                    hand3.Add(cards.ElementAt(3));
                    hand3.AddRange(activeCards.GetRange(0, temp));
                    break;
                case 4:
                    hand4.Add(cards.ElementAt(4));
                    hand4.Add(cards.ElementAt(5));
                    hand4.AddRange(activeCards.GetRange(0, temp));
                    break;
                case 5:
                    hand5.Add(cards.ElementAt(6));
                    hand5.AddRange(activeCards.GetRange(0, temp));
                    break;
            }
            activeCards.RemoveRange(0, temp);
        }
    }

    int drawNumber(int playerIndex)
    {
        int count = powerObjects[playerIndex].ruler.cardBonus;

        if (playerIndex == 5)
        {

            for (int i = 0; i < 6; i++)
            {
                if (religiousInfluence[i] == (Religion)1)
                {
                    count++;
                }
            }
            //UnityEngine.Debug.Log(count);
            if (count > 3)
            {
                return 5;
            }
            else
            {
                return 4;
            }
        }
        else
        {
            for (int i = 0; i < spacesGM.Count(); i++)
            {
                if (spacesGM.ElementAt(i).controlPower == playerIndex && spacesGM.ElementAt(i).controlMarker == 3)
                {

                    count++;
                }
            }
            //UnityEngine.Debug.Log(count);
            if (count > 1)
            {
                return count;
            }
            else
            {
                return 1;
            }

        }
    }

    IEnumerator waitDipForm()
    {

        DipForm tempForm = GameObject.Find("CanvasDiplomacy").GetComponent("DipForm") as DipForm;
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;

        GM1.player = 0;
        onPlayerChange();
        for (int i = 0; i < 6; i++)
        {
            UnityEngine.Debug.Log("wait" + i.ToString());


            while (!tempForm.completed[i])
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }
            if (i < 5)
            {
                GM1.player++;
                onPlayerChange();
            }
            else
            {
                GM1.player = 0;
                onPlayerChange();
            }

            UnityEngine.Debug.Log("endwait");


        }
        tempForm.verifyDip();
        negotiationSegment(tempForm);
        onChangeDip();
        segment++;
        onChangeSegment();
        phase3();
        yield break;


    }

    void negotiationSegment(DipForm tempForm)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = i; j < 6; j++)
            {
                if (tempForm.dipStatus[i, j] != 0)
                {
                    diplomacyState[i, j] = tempForm.dipStatus[i, j];
                }


            }
        }
    }



    IEnumerator waitPeaceForm()
    {

        DipForm tempForm = GameObject.Find("CanvasDiplomacy").GetComponent("DipForm") as DipForm;
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
        GM1.player = 0;
        onPlayerChange();
        for (int i = 0; i < 6; i++)
        {
            while (!tempForm.completed[i])
            {
                yield return null;
            }
            if (i < 5)
            {
                GM1.player++;
                onPlayerChange();
            }
            else
            {
                GM1.player = 0;
                onPlayerChange();
            }

            UnityEngine.Debug.Log("endwait");


            //onRemoveHighlight(converted);
        }
        segment++;
        onChangeSegment();
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
        yield break;
        //automatic: 2, 3
        //4: highlight 2 units to remove them from map
        //5, 7: highlight, choose to regain home keys and other spaces
        //
    }

    void phase3()
    {
        switch (segment)
        {
            case 1:
                StartCoroutine(waitDipForm());
                break;
            case 2:
                StartCoroutine(waitPeaceForm());
                break;
            default:
                break;
        }








    }

    void phase4()
    {
        StartCoroutine(DietofWorms());
    }

    IEnumerator DietofWorms()
    {
        player = 1;
        onPlayerChange();
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Select commitment card if you are involved");
        Array.Clear(secretCP, 0, 6);
        while (secretCP[1] == 0 || secretCP[4] == 0 || secretCP[5] == 0)
        {
            yield return null;
        }
        int result = evaluateDiet();
        if (result > 10)
        {
            //only player 5
            for (int i = 0; i < result - 10; i++)
            {
                highlightSelected = -1;
                List<int> canConvert = dietSpaces(true);
                UnityEngine.Debug.Log("can convert" + canConvert.Count().ToString());
                onNoLayer();
                onHighlight(canConvert);

                onHighlightSelected += changeReligion;
                while (highlightSelected == -1)
                {

                    yield return null;
                }
            }

        }
        else if (result > 0)
        {
            //only player 4
            for (int i = 0; i < result; i++)
            {
                highlightSelected = -1;
                List<int> canConvert = dietSpaces(false);
                onNoLayer();
                onHighlight(canConvert);

                onHighlightSelected += changeReligion;
                while (highlightSelected == -1)
                {

                    yield return null;
                }
            }

        }

    }

    int evaluateDiet()
    {

        int hit1 = 0;
        int hit2 = 0;
        for (int i = 0; i < secretCP[5] + 4; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 6);

            if (randomIndex > 4)
            {
                hit1++;
            }
        }
        for (int i = 0; i < secretCP[1] + secretCP[4]; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 6);

            if (randomIndex > 4)
            {
                hit2++;
            }
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        if (hit1 > hit2)
        {
            currentTextObject.post("Dices: Protestant " + secretCP[5].ToString() + ". Hapsburg " + secretCP[1].ToString() + ". Papal " + secretCP[4].ToString() + "" + "\nProtestant hit: " + hit1.ToString() + ". Catholic hit: " + hit2.ToString() + "\nProtestant victory. Flip " + (hit1 - hit2).ToString() + " space(s).");
            return 10 + hit1 - hit2;
        }
        else if (hit1 < hit2)
        {
            currentTextObject.post("Dices: Protestant " + secretCP[5].ToString() + ". Hapsburg " + secretCP[1].ToString() + ". Papal " + secretCP[4].ToString() + "" + "\nProtestant hit: " + hit1.ToString() + ". Catholic hit: " + hit2.ToString() + "\nCatholic victory. Flip " + (hit2 - hit1).ToString() + " space(s).");
            return hit2 - hit1;
        }
        else
        {
            currentTextObject.post("Dices: Protestant " + secretCP[5].ToString() + ". Hapsburg " + secretCP[1].ToString() + ". Papal " + secretCP[4].ToString() + "" + "\nProtestant hit: " + hit1.ToString() + ". Catholic hit: " + hit2.ToString() + "\nThe Diet is inconclusive.");
            return 0;
        }

    }

    

    public static void changeReligion()
    {
        onHighlightSelected -= changeReligion;
        if ((int)DeckScript.spaces.ElementAt(highlightSelected).spaceType == 4 && regulars[134 + highlightSelected] != 0)
        {
            regulars[134 + highlightSelected] = 0;
            onChangeReg(134 + highlightSelected, 5);
            if (highlightSelected == 0 || highlightSelected == 5)
            {
                regulars[highlightSelected] = 2;
            }
            else
            {
                regulars[highlightSelected] = 1;
            }
            onChangeReg(highlightSelected, 5);


        }
        if ((int)religiousInfluence[highlightSelected] == 1)
        {
            religiousInfluence[highlightSelected] = (Religion)0;
            DeckScript.spacesGM.ElementAt(highlightSelected).flipControl();
            GM1.protestantSpaces--;
            GM1.updateVP();
            onVP();
            onMoveHome25();
            onFlipSpace(highlightSelected, DeckScript.spacesGM.ElementAt(highlightSelected).controlPower, DeckScript.spacesGM.ElementAt(highlightSelected).controlMarker);
        }
        else
        {
            religiousInfluence[highlightSelected] = (Religion)1;
            DeckScript.spacesGM.ElementAt(highlightSelected).flipControl();
            GM1.protestantSpaces++;
            GM1.updateVP();
            onVP();
            onMoveHome25();
            onFlipSpace(highlightSelected, DeckScript.spacesGM.ElementAt(highlightSelected).controlPower, DeckScript.spacesGM.ElementAt(highlightSelected).controlMarker);
        }

    }

    IEnumerator waitDeployment()
    {
        player = 0;
        onPlayerChange();
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Click a leader.\nEnter number of units:");
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        LayerScript layerObject = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
        for (int i = 0; i < 5; i++)
        {
            UnityEngine.Debug.Log("spring deployment: " + i.ToString());
            UnityEngine.Debug.Log("click commander ");
            //todo: reset click after one valid choice

            inputNumberObject.post();
            List<int> trace = findTrace(i);
            highlightSelected = -1;
            leaderSelected = -1;
            layerObject.highlightLeaderPower();



            onHighlight(trace);
            onHighlightSelected += springDeploy;
            while (player != i || highlightSelected == -1)
            {
                yield return null;
            }
            inputNumberObject.reset();
            player = i + 1;
            onPlayerChange();
        }
        layerObject.resetLeaderPower();
        currentTextObject.reset();


    }

    void phase5()
    {
        //spring deployment
        StartCoroutine(waitDeployment());

    }

    

    

    void springDeploy()
    {
        GM2.onHighlightSelected -= springDeploy;
        int capital = 0;
        switch (player)
        {
            case 0:
                capital = 98;
                break;
            case 1:
                if (leaderSelected == 5)
                {
                    capital = 22;
                }
                else
                {
                    capital = 84;
                }
                break;
            case 2:
                capital = 28;
                break;
            case 3:
                capital = 42;
                break;
            case 4:
                capital = 66;
                break;
        }
        if (leaderSelected != spacesGM.ElementAt(capital - 1).leader1 && leaderSelected != spacesGM.ElementAt(capital - 1).leader2)
        {
            leaderSelected = 0;

        }
        else
        {
            int command = 0;


            if (!string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
            {
                spacesGM.ElementAt(capital - 1).removeLeader(leaderSelected);
                spacesGM.ElementAt(highlightSelected).addLeader(leaderSelected);
                onChangeLeader(highlightSelected, leaderSelected);
                command = int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
                if (command > leaders.ElementAt(leaderSelected - 1).command)
                {
                    command = leaders.ElementAt(leaderSelected - 1).command;
                }

                if (command > regulars[capital - 1])
                {
                    command = regulars[capital - 1];
                }
            }


            spacesGM.ElementAt(highlightSelected).regular = spacesGM.ElementAt(highlightSelected).regular + command;
            spacesGM.ElementAt(capital - 1).regular = spacesGM.ElementAt(capital - 1).regular - command;
            regulars[highlightSelected] = regulars[highlightSelected] + command;
            regulars[capital - 1] = regulars[capital - 1] - command;
            onChangeReg(highlightSelected, player);
            onChangeReg(capital - 1, player);
        }




    }

    void phase6()
    {

    }


    void discardCard(int index)
    {

        for (int i = 0; i < hand0.Count(); i++)
        {
            if (hand0.ElementAt(i).id == index)
            {
                discardCards.Add(hand0.ElementAt(i));
                hand0.RemoveAt(i);
            }
        }
        for (int i = 0; i < hand1.Count(); i++)
        {
            if (hand1.ElementAt(i).id == index)
            {
                discardCards.Add(hand1.ElementAt(i));
                hand1.RemoveAt(i);
            }
        }
        for (int i = 0; i < hand2.Count(); i++)
        {
            if (hand2.ElementAt(i).id == index)
            {
                discardCards.Add(hand2.ElementAt(i));
                hand2.RemoveAt(i);
            }
        }
        for (int i = 0; i < hand3.Count(); i++)
        {
            if (hand3.ElementAt(i).id == index)
            {
                discardCards.Add(hand3.ElementAt(i));
                hand3.RemoveAt(i);
            }
        }
        for (int i = 0; i < hand4.Count(); i++)
        {
            if (hand4.ElementAt(i).id == index)
            {
                discardCards.Add(hand4.ElementAt(i));
                hand4.RemoveAt(i);
            }
        }
        for (int i = 0; i < hand5.Count(); i++)
        {
            if (hand5.ElementAt(i).id == index)
            {
                discardCards.Add(hand5.ElementAt(i));
                hand5.RemoveAt(i);
            }
        }
    }


    void Awake()
    {
        instance = this;
        gm3 = new GM3();

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
