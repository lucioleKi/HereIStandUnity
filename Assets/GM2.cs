using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using TMPro;
using System.ComponentModel.Design;

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
    public static SimpleHandler onHighlightSelected;
    public static SimpleHandler onChangeDip;
    public static SimpleHandler onChangePhase;
    public static SimpleHandler onChosenCard;
    public static SimpleHandler onPlayerChange;

    public delegate void Int2Handler(int index1, int index2);
    public static Int2Handler onMoveHome25;
    //(card index = id - 1, power)
    public static Int2Handler onChangeReg;
    public delegate void Int3Handler(int index1, int index2, int index3);
    public static Int3Handler onAddSpace;

    public delegate void Int1Handler(int index);
    public static Int1Handler onRemoveSpace;
    public static Int1Handler onAddReformer;
    public static Int1Handler onConfirmDipForm;
    public static Int1Handler onCPChange;
    public static Int1Handler onMandatory;

    public delegate void List1Handler(List<int> index);
    public static List1Handler onHighlight;
    public static List1Handler onRemoveHighlight;

    public static int highlightSelected = -1;
    public static int leaderSelected = -1;
    public static bool phaseEnd = false;
    public static int currentCP = 0;


    //
    void OnEnable()
    {
        onMandatory += mandatory;
        onPhase2 += phase2;
        onPhase3 += phase3;
        onPhase5 += phase5;
    }


    void OnDisable()
    {
        onMandatory -= mandatory;
        onPhase2 -= phase2;
        onPhase3 -= phase3;
        onPhase5 -= phase5;
    }

    /*if (onMoveHome25 != null)
        {
            UnityEngine.Debug.Log("Here");
            onMoveHome25?.Invoke(1, 2);
}
    */

    //todo: make this generic
    IEnumerator waitHighlight()
    {

        for (int i = 0; i < 5; i++)
        {
            UnityEngine.Debug.Log("start");
            List<int> pickSpaces = highlightReformation();
            highlightSelected = -1;
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
        highlightSelected= -1;
        chosenCard = "";
        onChosenCard();
    }


    void mandatory(int index)
    {
        switch (index)
        {
            case 8:

                activeReformers.Add(reformers.ElementAt(0));
                GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/Reformer4/Luther"), new Vector3(spaces.ElementAt(0).posX + 965, spaces.ElementAt(0).posY + 545, 0), Quaternion.identity);
                tempObject.transform.SetParent(GameObject.Find("Reformers").transform);
                tempObject.name = "Luther";
                tempObject.SetActive(true);
                religiousInfluence[0] = (Religion)1;
                onMoveHome25(0, 1);
                regulars[134] = 0;
                onChangeReg(134, 5);
                regulars[0] = 2;
                onChangeReg(0, 5);
                StartCoroutine(waitHighlight());
                //remove Luther's 95 theses from backend decks
                cards.RemoveAt(7);
                hand5.RemoveAt(0);

                break;
        }
    }

    void reformAttempt()
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
        UnityEngine.Debug.Log(reformerDice);
        UnityEngine.Debug.Log(papalDice);
        //4. roll dice
        int dice1 = 0;
        for (int i = 0; i < reformerDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 6);

            if (dice1 < randomIndex)
            {
                dice1 = randomIndex;
            }
            if (dice1 == 6)
            {
                UnityEngine.Debug.Log("6!");
                religiousInfluence[target] = (Religion)1;
                //send signal to various parties
                return;
            }
        }


        //5. add up papal dice



        //6. roll papal dice
        int dice2 = 0;
        for (int i = 0; i < papalDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 6);

            if (dice2 < randomIndex)
            {
                dice2 = randomIndex;
            }
        }

        //7. determine result
        if (dice1 > dice2)
        {
            UnityEngine.Debug.Log("win");
            religiousInfluence[target] = (Religion)1;
            onMoveHome25(0, 1);
            if ((int)targetSpace.spaceType == 4)
            {
                regulars[134 + target] = 0;
                onChangeReg(134 + target, 5);
                regulars[target] = 1;
                onChangeReg(target, 5);


            }
            //send signal to various parties
            return;
        }
        else
        {
            UnityEngine.Debug.Log("lose");
            return;
        }
    }

    List<int> highlightReformation()
    {
        //todo: make port
        List<int> highlightReformations = new List<int>();
        for (int i = 0; i < spaces.Count(); i++)
        {
            if ((int)religiousInfluence[i] == 1)
            {
                continue;
            }
            for (int j = 0; j < spaces.ElementAt(i).adjacent.Count(); j++)
            {

                if (religiousInfluence[spaces.ElementAt(i).adjacent.ElementAt(j) - 1] == (Religion)1)
                {

                    highlightReformations.Add(i);
                    break;
                }
            }
            if (!highlightReformations.Contains(i))
            {
                for (int j = 0; i < spaces.ElementAt(i).pass.Count(); j++)
                {
                    if (religiousInfluence[spaces.ElementAt(i).pass.ElementAt(j)] == (Religion)1)
                    {
                        highlightReformations.Add(i);
                        break;
                    }
                }
            }
            if (!highlightReformations.Contains(i))
            {
                for (int j = 0; j < activeReformers.Count(); j++)
                {
                    if (activeReformers.ElementAt(j).space == i)
                    {
                        highlightReformations.Add(i);
                        break;
                    }
                }
            }


        }
        return highlightReformations;
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

        DipForm tempForm = ScriptableObject.CreateInstance<DipForm>();
        //tempForm.init();


        for (int i = 0; i < 6; i++)
        {
            UnityEngine.Debug.Log("wait" + i.ToString());


            while (!tempForm.completed[i])
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }

            UnityEngine.Debug.Log("endwait");


            //onRemoveHighlight(converted);
        }
        tempForm.verifyDip();
        negotiationSegment(tempForm);
        onChangeDip();

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

    /*IEnumerator waitPeaceForm()
    {

        for (int i = 0; i < 6; i++)
        {

        }
        if (true)
        {
            yield return null;
        }
       
        //automatic: 2, 3
        //4: highlight 2 units to remove them from map
        //5, 7: highlight, choose to regain home keys and other spaces
        //
    }*/

    void phase3()
    {

        //StartCoroutine(waitDipForm());
        if (turn != 1)
        {
            //StartCoroutine(waitPeaceForm());

        }
    }

    IEnumerator waitDeployment()
    {

        for (int i = 0; i < 5; i++)
        {
            UnityEngine.Debug.Log("spring deployment: "+ i.ToString());
            UnityEngine.Debug.Log("click commander ");
            //todo: reset click after one valid choice
            
            List<int> trace = findTrace();
            highlightSelected = -1;
            onHighlight(trace);
            onHighlightSelected += springDeploy;
            while (leaderSelected == -1||highlightSelected == -1)
            {
                yield return null;
            }
            GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text = "";
            player = i + 1;
            onPlayerChange();
        }
        

        
    }

    void phase5()
    {
        //spring deployment
        StartCoroutine(waitDeployment());

    }

    List<int> findTrace()
    {
        bool[] traceable = new bool[134];
        Array.Clear(traceable, 0, 134);
        List<int> searchIndex = new List<int>();
        List<int> trace = new List<int>();
        switch (player)
        {
            case 0:
                searchIndex.Add(98);
                break;
            case 1:
                searchIndex.Add(84);
                searchIndex.Add(22);
                break;
            case 2:
                searchIndex.Add(28);
                break;
            case 3:
                searchIndex.Add(42);
                break;
            case 4:
                searchIndex.Add(66);
                break;
            default:
                break;
        }
        while (searchIndex.Count() > 0)
        {

            traceable[searchIndex.ElementAt(0) - 1] = true;
            //UnityEngine.Debug.Log(spaces.ElementAt(searchIndex.ElementAt(0) - 1).name);
            for (int j = 0; j < spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent.Count(); j++)
            {

                if (!traceable[spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent[j] - 1]&& spacesGM.ElementAt(searchIndex.ElementAt(0) - 1).controlPower== spacesGM.ElementAt(spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent[j] - 1).controlPower)
                {
                    searchIndex.Add(spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent[j]);
                }


            }
            searchIndex.RemoveAt(0);
            
        }
        traceable[97] = false;
        traceable[83] = false;
        traceable[21] = false;
        traceable[27] = false;
        traceable[41] = false;
        traceable[65] = false;
        for (int i = 0; i < traceable.Length; i++)
        {
            if (traceable[i])
            {
                trace.Add(i);
            }
        }
        return trace;
    }

    void springDeploy()
    {
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
        if (leaderSelected!=spacesGM.ElementAt(capital-1).leader1&& leaderSelected != spacesGM.ElementAt(capital - 1).leader2)
        {
            UnityEngine.Debug.Log("no valid leader");
            leaderSelected = 0;
            return;
        }
        //UnityEngine.Debug.Log(leaderSelected);
        int command = leaders.ElementAt(leaderSelected-1).command;
       
        
        if (!string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
        {
            
            if(command > int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
            {
                command = int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
            }
            
            if (command > regulars[capital - 1])
            {
                command = regulars[capital - 1];
            }
        }
        else
        {
            command = 0;
        }
        
        spacesGM.ElementAt(highlightSelected).regular = spacesGM.ElementAt(highlightSelected).regular + command;
        spacesGM.ElementAt(capital - 1).regular = spacesGM.ElementAt(capital - 1).regular - command;
        regulars[highlightSelected] = regulars[highlightSelected] + command;
        regulars[capital - 1] = regulars[capital - 1] - command;
        onChangeReg(highlightSelected, player);
        onChangeReg(capital - 1, player);
        
        
    }

    

    void Awake()
    {
        instance = this;


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
