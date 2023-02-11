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
    public static SimpleHandler onVP;
    public static SimpleHandler onPhaseEnd;
    public static SimpleHandler onPhase2;
    public static SimpleHandler onPhase3;
    public static SimpleHandler onPhase4;
    public static SimpleHandler onPhase5;
    public static SimpleHandler onPhase6;
    public static SimpleHandler onPhase7;
    public static SimpleHandler onPhase8;
    public static SimpleHandler onPhase9;
    public static SimpleHandler onHighlightSelected;
    public static SimpleHandler onChangeDip;
    public static SimpleHandler onChangePhase;
    public static SimpleHandler onChosenCard;
    public static SimpleHandler onPlayerChange;
    public static SimpleHandler onMoveHome25;
    public static SimpleHandler onSpaceLayer;
    public static SimpleHandler onRegLayer;
    public static SimpleHandler onMercLayer;
    public static SimpleHandler onLeaderLayer;
    public static SimpleHandler onNavalLayer;
    public static SimpleHandler onNoLayer;
    public static SimpleHandler onLeaderULayer;
    public static SimpleHandler onRemoveHighlight;
    public static SimpleHandler onChangeSegment;
    public static SimpleHandler onDeactivateSkip;
    public static SimpleHandler onDeactivateOther;

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
    public static Int1Handler onSkipCard;
    public static Int1Handler onChangeUnrest;
    public static Int1Handler onOtherBtn;
    public static Int1Handler onAddUni;
    public delegate void List1Handler(List<int> index);
    public static List1Handler onHighlight;
    public static List1Handler onHighlightDip;


    public static bool[] boolStates;
    public static int[] intStates;
    //0: waitCard for HIS003. 1: phaseEnd. 2: in theological debate (CP action). 3: is piracy allowed (turn 3 card 9). 4: Henry VIII marries Anne Boleyn. 5: turn 2 card 10.
    //6: turn 4 card 13. 7: turn 4 card 14. 8: turn 6 card 15.
    //9: Jamestown colonized. 10: Roanoke colonized. 11: Charlesbourg colonized. 12: Montreal colonized. 13: Cuba colonized. 14: Hispaniola colonized. 15: PuertoRico colonized.
    //16: 1 already colonized. 17: 2 already colonized. 18: 3 already colonized. TODO: reset end of turn 
    //19: 1 already explored. 20: 2 already explored. 21: 3 already explored.
    //22: 1 already conquered. 23: 2 already conquered. 24: 3 already conquered.
    //25: 1 charted. 26: 2 charted. 27: 3 charted.
    //28: in land movement procedure (CP action)
    //29: HIS031 has effect
    //30: in siege procedure (CP action)
    //31: in naval movement procedure (CP action)
    //32: HIS076 has effect
    //33: in DOW procedure (segment 5)
    //34: HIS112 Thomas More, no debate in England this turn 
    //35: HIS105 treachery for siege procedure
    //39-44: conquered index
    //45-47: Cabot used
    //48: Cabot dead
    //49: HIS090 has effect
    //50: waiting for HIS087
    
    //51: waiting for HIS074
    //52-58: once excommunicated
    //59: HIS039 has effect
    //60-62: Galleons for 123
    //63-65: Plantations for 123
    //66: Knights of St.John on map
    //0: which power has HIS031 effect
    //1: which explorer for 1
    //2: which explorer for 2
    //3: which explorer for 3
    //4: which conquest for 1
    //5: which power has inca
    //6: which power has aztecs
    //7: which power has maya
    //8: which power has smallpox effect for conquest
    //9: which power can't start another exploration
    //10: which power can't start another conquest
    //11: where is Knights of St. John
    //12: which power has negative1Card from HIS079 Fuggers
    //13: which power has mercator's map
    //14: which power has potosi
    //public static bool waitCard = false;
    public static int highlightSelected = -1;
    public static int leaderSelected = -1;
    public static int currentCP = 0;
    public static int[] secretCP = new int[10];

    GM3 gm3;

    //
    void OnEnable()
    {
        boolStates = new bool[70];
        intStates = new int[20];
        for (int i = 0; i < 20; i++)
        {
            intStates[i] = -1;
        }
        onMandatory += mandatory;
        onPhase2 += phase2;
        onPhase3 += phase3;
        onPhase4 += phase4;
        onPhase5 += phase5;
        onPhase6 += phase6;
        onPhase7 += phase7;
        onPhase8 += phase8;
        onPhase9 += phase9;
    }


    void OnDisable()
    {
        onMandatory -= mandatory;
        onPhase2 -= phase2;
        onPhase3 -= phase3;
        onPhase4 -= phase4;
        onPhase5 -= phase5;
        onPhase6 -= phase6;
        onPhase7 -= phase7;
        onPhase8 -= phase8;
        onPhase9 -= phase9;
    }

    /*if (onMoveHome25 != null)
        {
            UnityEngine.Debug.Log("Here");
            onMoveHome25?.Invoke(1, 2);
}
    */


    public static void resetMap()
    {
        GameObject.Find("Map1").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("ScrollMapElements").GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        GameObject.Find("ScrollMapElements").GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    public static void resetReligious()
    {
        GameObject.Find("Religious").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("ScrollReligiousCard").GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        GameObject.Find("ScrollReligiousCard").GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    public static void resetPower()
    {
        GameObject.Find("PowerDisplay").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("ScrollPowerCard").GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        GameObject.Find("ScrollPowerCard").GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }


    public void mandatory(int index)
    {
        if(GM1.phase == 6 && GM1.impulse == GM1.player)
        {
            onDeactivateSkip();
        }
        
        if (index > 8)
        {
            discardCard(index);
        }
        switch (index)
        {
            case 1:
                if (boolStates[28] || boolStates[31])
                {
                    gm3.HIS001A();
                }
                else
                {
                    StartCoroutine(gm3.HIS001B());
                }
                break;
            case 2:
                StartCoroutine(gm3.HIS002());
                break;
            case 3:
                StartCoroutine(gm3.HIS003A());
                break;
            case 4:
                StartCoroutine(gm3.HIS004());
                break;
            case 5:
                StartCoroutine(gm3.HIS005());
                break;
            case 6:
                StartCoroutine(gm3.HIS006());
                break;
            case 7:
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
            case 11:
                StartCoroutine(gm3.HIS011());
                break;
            case 12:
                gm3.HIS012();
                break;
            case 13:
                gm3.HIS013();
                break;
            case 14:
                gm3.HIS014();
                break;
            case 15:
                StartCoroutine(gm3.HIS015());
                break;
            case 16:
                gm3.HIS016();
                break;
            case 17:
                gm3.HIS017();
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
            case 23:
                gm3.HIS023();
                break;
            case 24:
                gm3.HIS024();
                break;
            case 25:
                gm3.HIS025();
                break;
            case 26:
                gm3.HIS026();
                break;
            case 27:
                gm3.HIS027();
                break;
            case 28:
                gm3.HIS028();
                break;
            case 29:
                gm3.HIS029();
                break;
            case 30:
                gm3.HIS030();
                break;
            case 31:
                gm3.HIS031();
                break;
            case 32:
                gm3.HIS032();
                break;
            case 33:
                StartCoroutine(gm3.HIS033());
                break;
            case 34:
                StartCoroutine(gm3.HIS034());
                break;
            case 35:
                gm3.HIS035();
                break;
            case 36:
                StartCoroutine(gm3.HIS036());
                break;
            case 39:
                gm3.HIS039();
                break;
            case 40:
                StartCoroutine(gm3.HIS040());
                break;
            case 41:
                StartCoroutine(gm3.HIS041());
                break;
            case 42:
                StartCoroutine(gm3.HIS042());
                break;
            case 43:
                StartCoroutine(gm3.HIS043());
                break;
            case 44:
                StartCoroutine(gm3.HIS044());
                break;
            case 45:
                gm3.HIS045();
                break;
            case 46:
                StartCoroutine(gm3.HIS046());
                break;
            case 48:
                StartCoroutine(gm3.HIS048());
                break;
            case 50:
                StartCoroutine(gm3.HIS050());
                break;
            case 51:
                gm3.HIS051();
                break;
            case 52:
                StartCoroutine(gm3.HIS052());
                break;
            case 53:
                StartCoroutine(gm3.HIS053());
                break;
            case 54:
                StartCoroutine(gm3.HIS054());
                break;
            case 55:
                StartCoroutine(gm3.HIS055());
                break;
            case 61:
                StartCoroutine(gm3.HIS061());
                break;
            case 62:
                StartCoroutine(gm3.HIS062());
                break;
            case 63:
                StartCoroutine(gm3.HIS063());
                break;
            case 64:
                StartCoroutine(gm3.HIS064());
                break;
            case 65:
                StartCoroutine(gm3.HIS065());
                break;
            case 66:
                StartCoroutine(gm3.HIS066());
                break;
            case 67:
                StartCoroutine(gm3.HIS067());
                break;
            case 70:
                StartCoroutine(gm3.HIS070());
                break;
            case 72:
                StartCoroutine(gm3.HIS072());
                break;
            case 74:
                StartCoroutine(gm3.HIS074());
                break;
            case 75:
                StartCoroutine(gm3.HIS075());
                break;
            case 76:
                gm3.HIS076();
                break;
            case 77:
                StartCoroutine(gm3.HIS077());
                break;
            case 78:
                StartCoroutine(gm3.HIS078());
                break;
            case 79:
                gm3.HIS079();
                break;
            case 80:
                StartCoroutine(gm3.HIS080());
                break;
            case 81:
                StartCoroutine(gm3.HIS081());
                break;
            case 82:
                StartCoroutine(gm3.HIS082());
                break;
            case 83:
                gm3.HIS083();
                break;
            case 84:
                gm3.HIS084();
                break;
            case 85:
                StartCoroutine(gm3.HIS085());
                break;
            case 86:
                StartCoroutine(gm3.HIS086());
                break;
            case 87:
                StartCoroutine(gm3.HIS087());
                break;
            case 88:
                StartCoroutine(gm3.HIS088());
                break;
            case 89:
                StartCoroutine(gm3.HIS089());
                break;
            case 90:
                StartCoroutine(gm3.HIS090());
                break;
            case 94:
                StartCoroutine(gm3.HIS094());
                break;
            case 97:
                gm3.HIS097();
                break;
            case 98:
                StartCoroutine(gm3.HIS098());
                break;
            case 99:
                gm3.HIS099();
                break;
            case 100:
                StartCoroutine(gm3.HIS100());
                break;
            case 101:
                gm3.HIS101();
                break;
            case 102:
                gm3.HIS102();
                break;
            case 103:
                StartCoroutine(gm3.HIS103());
                break;
            case 104:
                StartCoroutine(gm3.HIS104());
                break;
            //case 105:
            //    gm3.HIS105();
            //    break;
            case 106:
                StartCoroutine(gm3.HIS106());
                break;
            case 107:
                StartCoroutine(gm3.HIS107());
                break;
            case 108:
                gm3.HIS108();
                break;
            case 109:
                gm3.HIS109();
                break;
            case 112:
                if (GM1.player == 2 || GM1.player == 5)
                {
                    StartCoroutine(gm3.HIS112A());
                }
                else
                {
                    StartCoroutine(gm3.HIS112B());
                }
                break;
            case 113:
                gm3.HIS113();
                break;
            case 114:
                gm3.HIS114();
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
        //currentTextObject.pauseColor();
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
            if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)-1] == (Religion)1)
            {
                reformerDice++;
            }
            else if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)-1] == (Religion)0)
            {
                papalDice++;
            }
            //+1 for every adjacent Jesuit university
            if (spacesGM.ElementAt(spaces.ElementAt(target).adjacent.ElementAt(i)-1).uni)
            {
                papalDice++;
            }
        }

        //+2 if has Jesuit university
        if (spacesGM.ElementAt(target).uni)
        {
            papalDice += 2;
        }

        //+2 if protestant land units, +1 if land units adjacent
        if (spacesGM.ElementAt(target).regularPower ==5 && spacesGM.ElementAt(target).controlPower == 5)
        {
            reformerDice = reformerDice + 2;
        }
        else if (spacesGM.ElementAt(target).regularPower == 4 && spacesGM.ElementAt(target).controlPower == 4)
        {
            papalDice = papalDice + 2;
        }
        for (int i = 0; i < targetSpace.adjacent.Count(); i++)
        {
            if (spacesGM.ElementAt(targetSpace.adjacent.ElementAt(i)-1).regularPower == 5 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 5)
            {
                reformerDice++;
            }
            else if (spacesGM.ElementAt(targetSpace.adjacent.ElementAt(i) - 1).regularPower == 4 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 4)
            {
                papalDice++;
            }
        }


        //3. add bonus dice
        //todo: debater bonus
        if (boolStates[49])
        {
            reformerDice++;
        }
        if (turn == 1 && phase == 1)
        {
            reformerDice++;
        }
        if (chosenCard == "HIS-046")
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
            if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)-1] == (Religion)1)
            {
                reformerDice++;
            }
            else if (religiousInfluence[spaces.ElementAt(target).adjacent.ElementAt(i)-1] == (Religion)0)
            {
                papalDice++;
            }
            //+1 for every adjacent Jesuit university
            if (spacesGM.ElementAt(spaces.ElementAt(target).adjacent.ElementAt(i)-1).uni)
            {
                papalDice++;
            }
        }

        //+2 if has Jesuit university
        if (spacesGM.ElementAt(target).uni)
        {
            papalDice += 2;
        }

        //+2 if protestant land units, +1 if land units adjacent
        if (spacesGM.ElementAt(target).regularPower == 5 && spacesGM.ElementAt(target).controlPower == 5)
        {
            reformerDice = reformerDice + 2;
        }
        else if (spacesGM.ElementAt(target).regularPower == 4 && spacesGM.ElementAt(target).controlPower == 4)
        {
            papalDice = papalDice + 2;
        }
        for (int i = 0; i < targetSpace.adjacent.Count(); i++)
        {
            if (spacesGM.ElementAt(targetSpace.adjacent.ElementAt(i) - 1).regularPower == 5 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 5)
            {
                reformerDice++;
            }
            else if (spacesGM.ElementAt(targetSpace.adjacent.ElementAt(i) - 1).regularPower == 4 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 4)
            {
                papalDice++;
            }
        }


        //3. add bonus dice
        //HIS039 has effect
        if (boolStates[59])
        {
            papalDice--;
        }

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
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        switch (segment)
        {
            case 1:
                //add debaters, reformers, leaders, new cards to the deck and shuffle
                DebatersScript debatersScript = GameObject.Find("DebaterDisplay").GetComponent("DebatersScript") as DebatersScript;
                //add reformers
                if (turn >= 2 && GameObject.Find("Zwingli") == null)
                {
                    activeReformers.Add(reformers.ElementAt(1));
                    GameObject tempObject = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Objects/Reformer4/Zwingli"), new Vector3(spaces.ElementAt(25).posX + 965, spaces.ElementAt(25).posY + 545, 0), Quaternion.identity);
                    tempObject.transform.SetParent(GameObject.Find("Reformers").transform);
                    tempObject.name = "Zwingli";
                    tempObject.SetActive(true);
                    highlightSelected = 25;
                    changeReligion();
                }
                else if (turn == 4 || debaters.ElementAt(25).status == (DebaterStatus)6)
                {
                    debaters.ElementAt(25).status = (DebaterStatus)1;
                    debatersScript.updateDebater();
                    activeReformers.Add(reformers.ElementAt(2));
                    GameObject tempObject = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Objects/Reformer4/Calvin"), new Vector3(spaces.ElementAt(63).posX + 965, spaces.ElementAt(63).posY + 545, 0), Quaternion.identity);
                    tempObject.transform.SetParent(GameObject.Find("Reformers").transform);
                    tempObject.name = "Calvin";
                    tempObject.SetActive(true);
                    highlightSelected = 63;
                    changeReligion();
                }
                highlightSelected = -1;
                
                instanceDeck.addActive(turn);
                debatersScript.updateDebater();
                activeCards.AddRange(discardCards);
                discardCards.Clear();
                instanceDeck.Shuffle();
                for(int i=0; i<6; i++)
                {
                    switch (i)
                    {
                        case 0:
                            hand0.Insert(0, cardsLib.ElementAt(0));
                            break;
                        case 1:
                            hand1.Insert(0, cardsLib.ElementAt(1));
                            break;
                        case 2:
                            hand2.Insert(0, cardsLib.ElementAt(2));
                            break;
                        case 3:
                            hand3.Insert(0, cardsLib.ElementAt(3));
                            break;
                        case 4:
                            hand4.Insert(0, cardsLib.ElementAt(5));
                            hand4.Insert(0, cardsLib.ElementAt(4));
                            break;
                        case 5:
                            hand5.Insert(0, cardsLib.ElementAt(6));
                            break;
                    }
                }
                
                segment++;
                phase2();

                resetMap();
                break;
            case 2:
                //new world riches
                StartCoroutine(resolveRiches());                
                
                break;
            case 3:
                //deal cards
                for (int i = 0; i < 6; i++)
                {
                    List<CardObject> tempHand = new List<CardObject>();
                    int temp = drawNumber(i);
                    //reset negative1Card
                    if (intStates[11] == i)
                    {
                        temp--;
                        intStates[11] = -1;
                    }
                    switch (i)
                    {
                        case 0:
                            hand0.AddRange(activeCards.GetRange(0, temp));
                            handMarkerScript.bonus0.Remove("Sprites/jpg/negative1Card");
                            break;
                        case 1:
                            hand1.AddRange(activeCards.GetRange(0, temp));
                            handMarkerScript.bonus1.Remove("Sprites/jpg/negative1Card");
                            break;
                        case 2:
                            hand2.AddRange(activeCards.GetRange(0, temp));
                            handMarkerScript.bonus2.Remove("Sprites/jpg/negative1Card");
                            break;
                        case 3:
                            hand3.AddRange(activeCards.GetRange(0, temp));
                            handMarkerScript.bonus3.Remove("Sprites/jpg/negative1Card");
                            break;
                        case 4:
                            hand4.AddRange(activeCards.GetRange(0, temp));
                            handMarkerScript.bonus4.Remove("Sprites/jpg/negative1Card");
                            break;
                        case 5:
                            hand5.AddRange(activeCards.GetRange(0, temp));
                            handMarkerScript.bonus5.Remove("Sprites/jpg/negative1Card");
                            break;
                    }
                   
                    activeCards.RemoveRange(0, temp);
                }
                onPhaseEnd();
                return;
                break;
            default:
                segment = 1;
                onPhaseEnd();
                break;
        }

        
        onPhaseEnd();
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

    IEnumerator resolveRiches()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        HighlightScript highlightScript = GameObject.Find("HighlightDisplay").GetComponent("HighlightScript") as HighlightScript;
        List<int> indices = new List<int>();
        List<float> x = new List<float>();
        List<float> y = new List<float>();
        List<int> powers = new List<int>();
        string[] potentials = new string[13] { "Jamestown", "Roanoke", "Charlesbourg", "Montreal", "Cuba", "Hispaniola", "PuertoRico", "Inca", "Aztecs", "Maya", "Potosi_1", "Potosi_2", "Potosi_3" };
        for(int i=9; i<16; i++)
        {
            if (boolStates[i])
            {
                indices.Add(i);
                x.Add(GameObject.Find(potentials[i - 9]).transform.position.x);
                y.Add(GameObject.Find(potentials[i - 9]).transform.position.y);
                if (i < 11)
                {
                    powers.Add(2);
                }else if (i > 12)
                {
                    powers.Add(1);
                }
                else
                {
                    powers.Add(3);
                }
            }
        }
        //conquests
        for(int i=5; i<8; i++)
        {
            if (intStates[i]>0)
            {
                indices.Add(i);
                x.Add(GameObject.Find(potentials[i +2]).transform.position.x);
                y.Add(GameObject.Find(potentials[i +2]).transform.position.y);
                powers.Add(intStates[i]);
            }
        }
        //potosi
        if (intStates[14] > 0)
        {
            indices.Add(24);
            x.Add(GameObject.Find(potentials[intStates[14]+9]).transform.position.x);
            y.Add(GameObject.Find(potentials[intStates[14] + 9]).transform.position.y);
            powers.Add(intStates[14]);
        }
        for(int i=0; i<x.Count; i++)
        {
            GM1.player = powers[i];
            onPlayerChange();
            highlightSelected = -1;
            highlightScript.highlightCoordinate(x[i], y[i], powers[i]);
            onNoLayer();
            while (highlightSelected == -1)
            {
                yield return null;
            }
            resolveRichesDices(indices[i], powers[i]);
            yield return new WaitForSeconds(3);
        }
        currentTextObject.reset();
        segment++;
        phase2();

        resetMap();
    }

    void resolveRichesDices(int index, int power)
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        int dice = UnityEngine.Random.Range(1, 7)+ UnityEngine.Random.Range(1, 7);
        //plantation
        if (boolStates[power+62])
        {
            dice++;
        }
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        if ((index > 8 && index < 16 && dice > 8) || (index == 24 && dice > 7) ||(index<9&&dice>7)||(index==5&&dice==6)) {
            //card
            if (power == 1)
            {
                hand1.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            else if (power == 2)
            {
                hand2.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            else if (power == 3)
            {
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            currentTextObject.post("Dice total: " + dice.ToString() + ". Draw 1 card.");
            return;
        }
        if (index > 8 && index < 16)
        {
            //colonies
            if (dice < 5)
            {
                //elim
                switch (index)
                {
                    case 9:
                        GameObject.Find("Jamestown").GetComponent<CanvasGroup>().alpha = 0;
                        break;
                    case 10:
                        GameObject.Find("Roanoke").GetComponent<CanvasGroup>().alpha = 0;
                        break;
                    case 11:
                        GameObject.Find("Charlesbourg").GetComponent<CanvasGroup>().alpha = 0;
                        break;
                    case 12:
                        GameObject.Find("Montreal").GetComponent<CanvasGroup>().alpha = 0;
                        break;
                    case 13:
                        GameObject.Find("Cuba").GetComponent<CanvasGroup>().alpha = 0;
                        break;
                    case 14:
                        GameObject.Find("Hispaniola").GetComponent<CanvasGroup>().alpha = 0;
                        break;
                    case 15:
                        GameObject.Find("PuertoRico").GetComponent<CanvasGroup>().alpha = 0;
                        break;
                }
                boolStates[index] = false;
                currentTextObject.post("Dice total: " + dice.ToString() + ". Colony is eliminated.");
            }
            else if (dice == 8)
            {
                //galleon
                if (power==1 && boolStates[60])
                {
                    hand1.AddRange(activeCards.GetRange(0, 1));
                    activeCards.RemoveRange(0, 1);
                }
                else if (power==2 && boolStates[61])
                {
                    hand2.AddRange(activeCards.GetRange(0, 1));
                    activeCards.RemoveRange(0, 1);
                }
                else if (power==3 && boolStates[62])
                {
                    hand3.AddRange(activeCards.GetRange(0, 1));
                    activeCards.RemoveRange(0, 1);
                }
                currentTextObject.post("Dice total: " + dice.ToString() + ". Draw 1 card.");
            }
            else
            {
                currentTextObject.post("Dice total: " + dice.ToString() + ". No effect.");
            }
        }else if (index == 24)
        {
            if (dice < 4)
            {
                //elim
                GameObject.Find("Potosi_" + power.ToString()).GetComponent<CanvasGroup>().alpha = 0;
                intStates[14] = -1;
                currentTextObject.post("Dice total: " + dice.ToString() + ". Potosi colony is eliminated.");
            }
            else if (dice == 7)
            {
                //galleon
                if (power == 1 && boolStates[60])
                {
                    hand1.AddRange(activeCards.GetRange(0, 1));
                    activeCards.RemoveRange(0, 1);
                }
                else if (power == 2 && boolStates[61])
                {
                    hand2.AddRange(activeCards.GetRange(0, 1));
                    activeCards.RemoveRange(0, 1);
                }
                else if (power == 3 && boolStates[62])
                {
                    hand3.AddRange(activeCards.GetRange(0, 1));
                    activeCards.RemoveRange(0, 1);
                }
                currentTextObject.post("Dice total: " + dice.ToString() + ". Draw 1 card.");
            }
        }else if ((index != 7 && dice < 6) || (index == 7 && dice < 7))
        {
            //depleted 1
            string[] names = new string[3]{"Inca2VP", "Aztecs2VP", "Maya1VP"};
            if (power == 1 && boolStates[60])
            {
                hand1.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
                handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/" + names[index-5]);
            }
            else if (power == 2 && boolStates[61])
            {
                hand2.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
                handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/" + names[index - 5]);
            }
            else if (power == 3 && boolStates[62])
            {
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
                handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/" + names[index - 5]);
            }
            if (index == 7)
            {
                GM1.bonusVPs[power]++;
            }
            else
            {
                GM1.bonusVPs[power]+=2;
            }
            GM1.updateVP();
            onVP();
            currentTextObject.post("Dice total: " + dice.ToString() + ". Conquest is depleted.");
        }
        else if (dice == 7)
        {
            //galleon
            if (power == 1 && boolStates[60])
            {
                hand1.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            else if (power == 2 && boolStates[61])
            {
                hand2.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            else if (power == 3 && boolStates[62])
            {
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            currentTextObject.post("Dice total: " + dice.ToString() + ". Draw 1 card.");
        }
        else
        {
            currentTextObject.post("Dice total: " + dice.ToString() + ". No effect.");
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
            if (i == 0)
            {
                GM1.deq1(1);
            }
            else
            {
                GM1.deq1(0);
                GM1.deq1(1);
            }



        }
        tempForm.verifyDip();
        negotiationSegment(tempForm);
        onChangeDip();
        if (turn != 1)
        {
            segment++;
        }
        else
        {
            segment = 7;
        }

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
                    if (tempForm.dipStatus[i, j] == 1 && diplomacyState[i, j] == 1)
                    {
                        diplomacyState[i, j] = 0;
                    }
                    else
                    {
                        diplomacyState[i, j] = tempForm.dipStatus[i, j];
                    }

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
            bool atWar = false;
            for (int j = 0; j < 6; j++)
            {
                if (diplomacyState[i, j] == 1)
                {
                    atWar = true;
                    break;
                }
            }
            if (atWar)
            {
                while (!tempForm.completed[i])
                {
                    yield return null;
                }
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
            if (i == 0)
            {
                GM1.deq1(1);
            }
            else if (i == 5)
            {
                if (GM1.turn == 1)
                {
                    GM1.enq2("Go to phase 4 - (Any player)");

                }
                else
                {
                    GM1.enq2("Go to phase 5 - (Any player)");
                }
                //deactivate diplomacy button
                GameObject.Find("DiplomacyButton").GetComponent<Button>().interactable = false;
            }
            else
            {
                GM1.deq1(0);
                GM1.deq1(1);
            }
            UnityEngine.Debug.Log("endwait");


            //onRemoveHighlight(converted);
        }
        segment++;
        phase3();
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
        //onPhaseEnd();
        //automatic: 2, 3
        //4: highlight 2 units to remove them from map
        //5, 7: highlight, choose to regain home keys and other spaces
        //
    }

    IEnumerator waitDOW()
    {
        DipForm tempForm = GameObject.Find("CanvasDiplomacy").GetComponent("DipForm") as DipForm;
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
        GM1.player = 0;
        onPlayerChange();

        for (int i = 0; i < 6; i++)
        {
            onNoLayer();
            onSkipCard(5);
            int pointer = 0;
            boolStates[33] = true;
            List<int> trace = tempForm.DOW(GM1.player);
            highlightSelected = -1;
            onHighlightDip(trace);
            while (boolStates[33] && trace.Count > 0)
            {
                if (highlightSelected != -1)
                {
                    tempForm.war[i, pointer] = highlightSelected;
                    trace.Remove(highlightSelected);
                    onSkipCard(5);
                    resetMap();
                    onHighlightDip(trace);
                    onNoLayer();
                    highlightSelected = -1;
                    pointer++;
                }
                yield return null;
            }
            onRemoveHighlight();
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
        }
        LayerScript layerObject = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
        layerObject.changeLayer();
        segment++;
        phase3();
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
        //onPhaseEnd();
    }

    IEnumerator waitDOWCP()
    {
        DipForm tempForm = GameObject.Find("CanvasDiplomacy").GetComponent("DipForm") as DipForm;
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        GM1.player = 0;
        onPlayerChange();
        List<int> required = tempForm.getDOWCP();
        currentTextObject.post("Play at least " + required[0] + "CP to declare war(s)");
        currentCP = 0;
        for (int i = 0; i < 6; i++)
        {
            currentTextObject.post("Play at least " + required[i] + "CP to declare war(s)");

            while (required[i] > 0)
            {

                if (currentCP > 0)
                {
                    required[i] -= currentCP;
                    currentCP = 0;
                    currentTextObject.post("Play at least " + required[i] + "CP to declare war(s)");
                }
                yield return null;
            }
            currentCP = 0;
            for (int j = 0; j < 8; j++)
            {
                if (tempForm.war[i, j] != 10)
                {
                    if (i < tempForm.war[i, j])
                    {
                        GM1.diplomacyState[i, tempForm.war[i, j]] = 1;
                    }
                    else
                    {
                        GM1.diplomacyState[tempForm.war[i, j], i] = 1;
                    }
                    onChangeDip();
                }
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
        }
        currentTextObject.reset();
        onDeactivateSkip();
        tempForm.reset();
        segment++;

        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
        onPhaseEnd();
    }

    void phase3()
    {

        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        DipForm tempForm = GameObject.Find("CanvasDiplomacy").GetComponent("DipForm") as DipForm;
        switch (segment)
        {
            case 1:
                GameObject.Find("DiplomacyButton").GetComponent<Button>().interactable = true;
                GM1.enq1("Complete diplomacy form - (Ottoman)");
                GM1.toDo.Enqueue("Complete diplomacy form - (Hapsburgs)");
                GM1.toDo.Enqueue("Complete diplomacy form - (England)");
                GM1.toDo.Enqueue("Complete diplomacy form - (France)");
                GM1.toDo.Enqueue("Complete diplomacy form - (Papacy)");
                GM1.toDo.Enqueue("Complete diplomacy form - (Protestant)");
                StartCoroutine(waitDipForm());
                break;
            case 2:
                GM1.enq1("Complete peace form - (Ottoman)");
                GM1.enq2("Complete peace form - (Hapsburgs)");
                GM1.toDo.Enqueue("Complete peace form - (England)");
                GM1.toDo.Enqueue("Complete peace form - (France)");
                GM1.toDo.Enqueue("Complete peace form - (Papacy)");
                GM1.toDo.Enqueue("Complete peace form - (Protestant)");
                StartCoroutine(waitPeaceForm());
                handMarkerScript.leadersCaptured();
                break;
            case 3:


                if (handMarkerScript.canRansom.Count > 0)
                {

                    currentTextObject.post("Choose whether to return the captured leader to gain a random card draw.");
                    GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
                    GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
                    GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Ransom";

                    startButton.startOther(6);
                    onSkipCard(1);
                    GM1.player = handMarkerScript.canRansom.ElementAt(0);
                    onPlayerChange();
                }
                else
                {
                    GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
                    GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
                    currentTextObject.reset();
                    segment++;
                    handMarkerScript.canRemoveExcom();
                    phase3();
                }

                break;
            case 4:
                UnityEngine.Debug.Log(handMarkerScript.excom.Count);
                if (handMarkerScript.excom.Count > 0)
                {
                    currentTextObject.post("Choose whether to remove excommunication.");
                    GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
                    GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
                    GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Remove";
                    startButton.startOther(8);
                    onSkipCard(4);
                    GM1.player = handMarkerScript.excom.ElementAt(0);
                    onPlayerChange();
                }
                else
                {
                    GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
                    GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
                    currentTextObject.reset();
                    segment++;
                    phase3();
                }
                break;
            case 5:
                onSkipCard(5);
                StartCoroutine(waitDOW());

                break;
            case 6:
                onDeactivateSkip();
                StartCoroutine(waitDOWCP());
                break;
            default:
                GameObject.Find("DiplomacyButton").GetComponent<Button>().interactable = false;
                currentTextObject.reset();
                onDeactivateSkip();
                tempForm.reset();
                GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
                GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
                onPhaseEnd();
                GM1.enq2("Go to phase 4 - (Any player)");
                break;
        }








    }

    void phase4()
    {
        GM1.enq1("Select commitment card - (Hapsburgs)");
        GM1.enq2("Select commitment card - (Papacy)");
        GM1.toDo.Enqueue("Select commitment card - (Protestant)");
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
        onPhaseEnd();
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

            if (hit2 - hit1 > GM1.protestantSpaces)
            {
                //boundary case where not enough spaces can be converted
                return GM1.protestantSpaces;
            }
            else
            {
                return hit2 - hit1;
            }
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

    public IEnumerator waitDeployment(int has102)
    {
        GM1.deq1(2);
        player = 0;
        onPlayerChange();
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Click a leader.\nEnter number of units:");
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        LayerScript layerObject = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        for (int i = 0; i < 5; i++)
        {
            if (has102 == i)
            {
                chosenCard = "HIS-102";
                onChosenCard();
                onSkipCard(102);
            }
            else
            {
                startButton.status = -1;
                startButton.btn.interactable = false;
                chosenCard = "";
                onChosenCard();
                onDeactivateSkip();
            }
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
            if (i == 0)
            {
                GM1.deq1(1);
            }
            else if (i == 4)
            {
                GM1.enq2("Go to phase 6 - (Any player)");
            }
            else
            {
                GM1.deq1(0);
                GM1.deq1(1);
            }
            inputNumberObject.reset();
            player = i + 1;
            onPlayerChange();
        }
        layerObject.resetLeaderPower();
        currentTextObject.reset();
        GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
        GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
        onPhaseEnd();

    }

    void phase5()
    {

        //check HIS-109
        int has109 = -1;
        int has102 = -1;
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

                if (temp.ElementAt(j).id == 109)
                {
                    has109 = i;
                }
                if (temp.ElementAt(j).id == 102 && i != 5)
                {
                    has102 = i;
                }
            }
        }

        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        if (has109 != -1 && !startButton.btn.interactable)
        {

            GM1.player = has109;
            onPlayerChange();
            chosenCard = "HIS-109";
            onChosenCard();
            onSkipCard(109);
        }
        else if (has109 != -1 && startButton.btn.interactable)
        {
            //spring deployment
            startButton.status = -1;
            startButton.btn.interactable = false;
            chosenCard = "";
            onChosenCard();
            GM1.enq1("Complete deployment - (Ottoman)");
            GM1.enq2("Complete deployment - (Hapsburgs)");
            GM1.toDo.Enqueue("Complete deployment - (England)");
            GM1.toDo.Enqueue("Complete deployment - (France)");
            GM1.toDo.Enqueue("Complete deployment - (Papacy)");

            StartCoroutine(waitDeployment(has102));
        }
        else
        {
            //spring deployment
            GM1.enq1("Complete deployment - (Ottoman)");
            GM1.enq2("Complete deployment - (Hapsburgs)");
            GM1.toDo.Enqueue("Complete deployment - (England)");
            GM1.toDo.Enqueue("Complete deployment - (France)");
            GM1.toDo.Enqueue("Complete deployment - (Papacy)");

            StartCoroutine(waitDeployment(has102));
        }


    }





    void springDeploy()
    {
        onHighlightSelected -= springDeploy;
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

                if (command > spacesGM.ElementAt(capital - 1).regular)
                {
                    command = spacesGM.ElementAt(capital - 1).regular;
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
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Active power: Ottoman");
        GM1.impulse = 0;
        //onPhaseEnd();
    }

    void phase7()
    {
        //Remove the Renegade Leader
        for (int i = 0; i < 134; i++)
        {
            if (spacesGM.ElementAt(i).leader1 == 14 || spacesGM.ElementAt(i).leader2 == 14)
            {
                spacesGM.ElementAt(i).removeLeader(14);
                onChangeLeader(-1, 14);
                break;
            }
        }
        //return naval units

        //return land units

        //remove alliance markers
        GM1.clearAlliance();
        onChangeDip();
        //add 1 regular to each friendly-controlled capital, todo: not under unrest
        checkCapital(0, 98);
        checkCapital(1, 84);
        checkCapital(1, 22);
        checkCapital(2, 28);
        checkCapital(3, 42);
        checkCapital(4, 66);
        //flip debaters to uncommitted
        DebatersScript debaterObject = GameObject.Find("DebaterDisplay").GetComponent("DebatersScript") as DebatersScript;
        debaterObject.toUncommited();
        //resolve mandatory events
        onPhaseEnd();
    }

    void checkCapital(int power, int index)
    {
        DeckScript.spacesGM.ElementAt(index - 1).regular++;
        DeckScript.regulars[index - 1]++;
        onChangeReg(index - 1, power);
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

    void phase8()
    {
        int count = 0;
        int randomIndex = 0;
        switch (segment)
        {
            case 1:
                //exploration
                for (int i = 19; i < 22; i++)
                {
                    if (boolStates[i])
                    {
                        count++;
                    }
                }
                if (count == 0)
                {

                    segment++;
                    phase8();
                }
                resetMap();
                resolveExploration();
                break;
            case 2:
                //conquest
                Array.Clear(boolStates, 19, 3);
                for (int i = 22; i < 25; i++)
                {
                    if (boolStates[i])
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    onPhaseEnd();
                    return;
                }
                resetMap();
                resolveConquest();
                break;
            default:
                Array.Clear(boolStates, 22, 3);
                segment = 1;
                onPhaseEnd();
                break;
        }
    }

    void resolveExploration()
    {
        List<int> order = new List<int>();
        List<int> modifier = new List<int>();
        for (int i = 1; i < 4; i++)
        {
            if (boolStates[i + 18])
            {
                order.Add(i);
                float pos;
                if (GameObject.Find("charted_" + i.ToString()) != null)
                {
                    modifier.Add(0);
                    pos = GameObject.Find("charted_" + i.ToString()).transform.position.x - 960;
                }
                else
                {
                    modifier.Add(-1);
                    pos = GameObject.Find("uncharted_" + i.ToString()).transform.position.x - 960;
                }

                GameObject newObject = new GameObject("explorer_"+GM1.turn.ToString() + i.ToString(), typeof(RectTransform), typeof(Image));
                switch (i)
                {
                    case 1:
                        if (boolStates[45])
                        {
                            intStates[1] = 0;
                            modifier[modifier.Count - 1] = 0;
                        }
                        else
                        {
                            intStates[1] = UnityEngine.Random.Range(1, explorers1.Count);
                        }
                        modifier[modifier.Count - 1] += explorers1.ElementAt(intStates[1]).value;

                        UnityEngine.Debug.Log(explorers1.ElementAt(intStates[1]).name);
                        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NewWorld/" + explorers1.ElementAt(intStates[1]).name);
                        break;
                    case 2:
                        if (boolStates[46])
                        {
                            intStates[2] = 0;
                            modifier[modifier.Count - 1] = 0;
                        }
                        else
                        {
                            intStates[2] = UnityEngine.Random.Range(1, explorers2.Count);
                        }
                        modifier[modifier.Count - 1] += explorers2.ElementAt(intStates[2]).value;

                        UnityEngine.Debug.Log(explorers2.ElementAt(intStates[2]).name);
                        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NewWorld/" + explorers2.ElementAt(intStates[2]).name);
                        break;
                    case 3:
                        if (boolStates[47])
                        {
                            intStates[3] = 0;
                            modifier[modifier.Count - 1] = 0;
                        }
                        else
                        {
                            intStates[3] = UnityEngine.Random.Range(1, explorers3.Count);
                        }
                        modifier[modifier.Count - 1] += explorers3.ElementAt(intStates[3]).value;
                        UnityEngine.Debug.Log(explorers3.ElementAt(intStates[3]).name);
                        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NewWorld/" + explorers3.ElementAt(intStates[3]).name);
                        break;
                }
                if (intStates[13] == i)
                {
                    modifier[modifier.Count - 1] += 2;
                    intStates[13] = -1;
                }
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                newObject.transform.SetParent(GameObject.Find("Atlantic").transform);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos, -197);
            }
        }
        order.Sort((x, y) =>
        {
            if (modifier[order.IndexOf(x)] > modifier[order.IndexOf(y)])
            {
                UnityEngine.Debug.Log(x.ToString() + " before " + y.ToString());
                return -1;
            }
            else if (modifier[order.IndexOf(x)] == modifier[order.IndexOf(y)])
            {
                if (x == 2)
                {
                    return -1;
                }
                else if (x == 3)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                UnityEngine.Debug.Log(y.ToString() + " before " + x.ToString());
                return 1;
            }
        });
        StartCoroutine(highlightEmpty(order, modifier));
    }

    IEnumerator highlightEmpty(List<int> order, List<int> modifier)
    {
        HighlightScript highlightScript = GameObject.Find("HighlightDisplay").GetComponent("HighlightScript") as HighlightScript;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        foreach (int i in order)
        {
            highlightSelected = -1;
            onNoLayer();
            highlightScript.highlightNewWorld(i);

            int randomIndex;
            while (highlightSelected == -1)
            {
                yield return null;
            }
            if (GameObject.Find("charted_" + i.ToString()) != null)
            {

                GameObject.Destroy(GameObject.Find("charted_" + i.ToString()));
            }
            else
            {
                GameObject.Destroy(GameObject.Find("uncharted_" + i.ToString()));
            }
            randomIndex = UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7) + modifier[order.IndexOf(i)];

            UnityEngine.Debug.Log(randomIndex);
            if (randomIndex < 5)
            {
                currentTextObject.post("The explorer is lost at sea. He is removed from the game.");
                //remove cabot
                if (intStates[i] == 0)
                {
                    boolStates[48] = true;
                    foreach (CardObject c in DeckScript.cards)
                    {
                        if (c.id == 99)
                        {
                            cards.Remove(c);
                            break;
                        }
                    }
                }
            }
            else if (randomIndex < 7)
            {
                currentTextObject.post("No discovery is made; the explorer is returned to the pool. ");
            }
            else if (randomIndex < 10)
            {
                currentTextObject.post("A discovery has been made. ");
            }
            else
            {
                currentTextObject.post("The explorer has penetrated deep into South America, and has a choice how to proceed.");
            }
            yield return new WaitForSeconds(3);

            HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
            
            switch (i)
            {
                case 1:
                    if (randomIndex < 5)
                    {
                        GameObject.Destroy(GameObject.Find("explorer_"+GM1.turn.ToString()+"1"));
                        explorers1.RemoveAt(intStates[1]);
                    }
                    else if (randomIndex < 7)
                    {
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "1"));
                    }
                    else if (randomIndex < 10)
                    {
                        if (GameObject.Find("MississippiRiver") != null && GameObject.Find("MississippiRiver").transform.IsChildOf(GameObject.Find("NewWorld").transform))
                        {
                            GameObject.Destroy(GameObject.Find("MississippiRiver"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "1").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-848, -215);
                            handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/MississippiRiver1VP");

                        }
                        else if (GameObject.Find("GreatLakes") != null && GameObject.Find("GreatLakes").transform.IsChildOf(GameObject.Find("NewWorld").transform))
                        {
                            GameObject.Destroy(GameObject.Find("GreatLakes"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "1").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-847, -164);
                            handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/GreatLakes1VP");
                        }
                        else
                        {
                            GameObject.Destroy(GameObject.Find("StLawrenceRiver"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "1").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-848, -215);
                            handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/StLawrenceRiver1VP");

                        }
                        GM1.bonusVPs[1]++;
                        GM1.updateVP();
                        onVP();
                    }
                    else if (randomIndex > 9)
                    {
                        yield return StartCoroutine(discovery10(modifier[order.IndexOf(i)], i));
                    }
                    break;
                case 2:
                    if (randomIndex < 5)
                    {
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "2"));
                        explorers2.RemoveAt(intStates[2]);
                    }
                    else if (randomIndex < 7)
                    {
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "2"));
                    }
                    else if (randomIndex < 10)
                    {
                        if (GameObject.Find("MississippiRiver") != null && GameObject.Find("MississippiRiver").transform.IsChildOf(GameObject.Find("NewWorld").transform))
                        {
                            GameObject.Destroy(GameObject.Find("MississippiRiver"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "2").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-848, -215);
                            handMarkerScript.bonus2.Add("Sprites/jpg/NewWorld/MississippiRiver1VP");

                        }
                        else if (GameObject.Find("GreatLakes") != null && GameObject.Find("GreatLakes").transform.IsChildOf(GameObject.Find("NewWorld").transform))
                        {
                            GameObject.Destroy(GameObject.Find("GreatLakes"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "2").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-847, -164);
                            handMarkerScript.bonus2.Add("Sprites/jpg/NewWorld/GreatLakes1VP");
                        }
                        else
                        {
                            GameObject.Destroy(GameObject.Find("StLawrenceRiver"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "2").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-848, -215);
                            handMarkerScript.bonus2.Add("Sprites/jpg/NewWorld/StLawrenceRiver1VP");

                        }
                        GM1.bonusVPs[2]++;
                        GM1.updateVP();
                        onVP();
                    }
                    else if (randomIndex > 9)
                    {
                        yield return StartCoroutine(discovery10(modifier[order.IndexOf(i)], i));
                    }
                    break;
                case 3:
                    if (randomIndex < 5)
                    {
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "3"));
                        explorers3.RemoveAt(intStates[3]);
                    }
                    else if (randomIndex < 7)
                    {
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "3"));
                    }
                    else if (randomIndex < 10)
                    {
                        if (GameObject.Find("MississippiRiver") != null && GameObject.Find("MississippiRiver").transform.IsChildOf(GameObject.Find("NewWorld").transform))
                        {
                            GameObject.Destroy(GameObject.Find("MississippiRiver"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "3").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "3").GetComponent<RectTransform>().anchoredPosition = new Vector2(-848, -215);
                            handMarkerScript.bonus3.Add("Sprites/jpg/NewWorld/MississippiRiver1VP");

                        }
                        else if (GameObject.Find("GreatLakes") != null && GameObject.Find("GreatLakes").transform.IsChildOf(GameObject.Find("NewWorld").transform))
                        {
                            GameObject.Destroy(GameObject.Find("GreatLakes"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "3").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "3").GetComponent<RectTransform>().anchoredPosition = new Vector2(-847, -164);
                            handMarkerScript.bonus3.Add("Sprites/jpg/NewWorld/GreatLakes1VP");
                        }
                        else
                        {
                            GameObject.Destroy(GameObject.Find("StLawrenceRiver"));
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "3").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("explorer_" + GM1.turn.ToString() + "3").GetComponent<RectTransform>().anchoredPosition = new Vector2(-848, -215);
                            handMarkerScript.bonus3.Add("Sprites/jpg/NewWorld/StLawrenceRiver1VP");

                        }
                        GM1.bonusVPs[3]++;
                        GM1.updateVP();
                        onVP();
                    }
                    else if (randomIndex > 9)
                    {
                        yield return StartCoroutine(discovery10(modifier[order.IndexOf(i)], i));
                    }
                    break;
            }

        }
        currentTextObject.reset();
        segment++;
        phase8();

    }


    IEnumerator discovery10(int modifier, int power)
    {
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        HighlightScript highlightScript = GameObject.Find("HighlightDisplay").GetComponent("HighlightScript") as HighlightScript;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        string[] names = new string[] { "PacificStrait", "AmazonRiver", "GreatLakes", "StLawrenceRiver", "MississippiRiver" };
        for (int i = 0; i < 5; i++)
        {
            if (GameObject.Find(names[i]) != null || (i == 0 && GameObject.Find("Circumnavigation") != null))
            {
                highlightScript.highlightCoordinate(GameObject.Find(names[i]).transform.position.x, GameObject.Find(names[i]).transform.position.y, i);
                UnityEngine.Debug.Log(GameObject.Find(names[i]).transform.position.x);
                UnityEngine.Debug.Log(GameObject.Find(names[i]).transform.position.y);
            }
        }
        highlightSelected = -1;
        onNoLayer();
        while (highlightSelected == -1)
        {
            yield return null;
        }
        List<string> temp = new List<string>();

        switch (power)
        {
            case 1:
                temp = handMarkerScript.bonus1;
                break;
            case 2:
                temp = handMarkerScript.bonus2;
                break;
            case 3:
                temp = handMarkerScript.bonus3;
                break;
        }
        if (highlightSelected != 0 || (highlightSelected == 0 && GameObject.Find("PacificStrait") != null))
        {

            GameObject.Find("explorer_" + GM1.turn.ToString()+power.ToString()).transform.SetParent(GameObject.Find("NewWorld").transform);
            GameObject.Find("explorer_" + GM1.turn.ToString() + power.ToString()).GetComponent<RectTransform>().anchoredPosition = new Vector2(GameObject.Find(names[highlightSelected]).transform.position.x - 990, GameObject.Find(names[highlightSelected]).transform.position.y - 223);
            GameObject.Destroy(GameObject.Find(names[highlightSelected]));
            switch (highlightSelected)
            {
                case 0:
                    temp.Add("Sprites/jpg/NewWorld/PacificStrait1VP");
                    break;
                case 1:
                    temp.Add("Sprites/jpg/NewWorld/AmazonRiver2VP");
                    GM1.bonusVPs[power]++;
                    break;
                case 2:
                    temp.Add("Sprites/jpg/NewWorld/GreatLakes1VP");
                    break;
                case 3:
                    temp.Add("Sprites/jpg/NewWorld/StLawrenceRiver1VP");
                    break;
                case 4:
                    temp.Add("Sprites/jpg/NewWorld/MississippiRiver1VP");
                    break;
            }
            GM1.bonusVPs[power]++;
            GM1.updateVP();
            onVP();
        }
        if (highlightSelected == 0)
        {

            int randomIndex = UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7) + modifier;
            if (randomIndex < 10)
            {
                currentTextObject.post("Circumnavigation failed. The explorer is removed from the game.");
                //remove the explorer
                switch (power)
                {
                    case 1:
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "1"));
                        explorers1.RemoveAt(intStates[1]);
                        break;
                    case 2:
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "2"));
                        explorers2.RemoveAt(intStates[2]);
                        break;
                    case 3:
                        GameObject.Destroy(GameObject.Find("explorer_" + GM1.turn.ToString() + "3"));
                        explorers3.RemoveAt(intStates[3]);
                        break;
                }
                yield return new WaitForSeconds(3);
            }
            else
            {
                currentTextObject.post("Circumnavigation success. ");
                GameObject.Destroy(GameObject.Find("Circumnavigation"));
                GameObject.Find("explorer_" + GM1.turn.ToString()+power.ToString()).transform.SetParent(GameObject.Find("NewWorld").transform);
                GameObject.Find("explorer_" + GM1.turn.ToString()+power.ToString()).GetComponent<RectTransform>().anchoredPosition = new Vector2(-893, -402);
                handMarkerScript.bonus3.Add("Sprites/jpg/NewWorld/Circumnavigation3VP");
                GM1.bonusVPs[power]++;
                GM1.updateVP();
                onVP();
            }

        }
    }

    void resolveConquest()
    {
        List<int> order = new List<int>();
        List<int> modifier = new List<int>();

        if (boolStates[22])
        {
            order.Add(1);

            intStates[4] = UnityEngine.Random.Range(0, conquests.Count);
            modifier.Add(intStates[4]);
            GameObject.Find("conquest_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NewWorld/" + conquests.ElementAt(intStates[4]).name);
        }
        if (boolStates[23])
        {
            order.Add(2);
            modifier.Add(0);
        }
        if (boolStates[24])
        {
            order.Add(3);
            modifier.Add(0);
        }
        StartCoroutine(highlightConquest(order, modifier));
    }

    IEnumerator highlightConquest(List<int> order, List<int> modifier)
    {
        HighlightScript highlightScript = GameObject.Find("HighlightDisplay").GetComponent("HighlightScript") as HighlightScript;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        foreach (int i in order)
        {
            highlightSelected = -1;
            onNoLayer();
            highlightScript.highlightNewWorld(i + 3);

            int randomIndex;
            while (highlightSelected == -1)
            {
                yield return null;
            }
            //if (GameObject.Find("charted_" + i.ToString()) != null)
            //{

            //    GameObject.Destroy(GameObject.Find("charted_" + i.ToString()));
            //}
            //else
            //{
            //    GameObject.Destroy(GameObject.Find("uncharted_" + i.ToString()));
            //}
            randomIndex = UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7) + modifier[order.IndexOf(i)];
            //smallpox effect
            if (intStates[8] == i)
            {
                randomIndex += 2;
                intStates[8] = -1;
            }
            UnityEngine.Debug.Log(randomIndex);
            if (randomIndex < 7)
            {
                currentTextObject.post("The conquistador is killed by natives. He is removed from the game.");
            }
            else if (randomIndex < 9)
            {
                currentTextObject.post("No conquest is completed; the conquistador is returned to the pool. ");
            }
            else
            {
                currentTextObject.post("A conquest has been made.");
            }
            yield return new WaitForSeconds(3);

            HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
            switch (i)
            {
                case 1:
                    if (randomIndex < 7)
                    {
                        GameObject.Destroy(GameObject.Find("conquest_1"));
                        conquests.RemoveAt(intStates[4]);
                    }
                    else if (randomIndex < 9)
                    {
                        GameObject.Destroy(GameObject.Find("conquest_1"));
                    }
                    else
                    {
                        int index = -1;
                        for (int j = 4; j < 7; j++)
                        {
                            if (!boolStates[39 + j])
                            {
                                boolStates[39 + j] = true;
                                index = j;
                                break;
                            }
                        }
                        if (intStates[5] == 0)
                        {
                            intStates[5] = 1;
                            GameObject.Find("Inca").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_1").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-774, -391);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Inca2VP");

                        }
                        else if (intStates[6] == 0)
                        {
                            intStates[6] = 1;
                            GameObject.Find("Aztecs").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_1").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-880, -271);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Aztecs2VP");
                        }
                        else
                        {
                            intStates[7] = 1;
                            GameObject.Find("Maya").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_1").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-840, -282);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Maya2VP");

                        }
                        GM1.bonusVPs[1]++;
                        GM1.updateVP();
                        onVP();
                    }
                    break;
                case 2:
                    if (randomIndex < 7)
                    {
                        GameObject.Destroy(GameObject.Find("conquest_2"));
                        conquests.RemoveAt(intStates[4]);
                    }
                    else if (randomIndex < 9)
                    {
                        GameObject.Destroy(GameObject.Find("conquest_2"));
                    }
                    else
                    {
                        int index = -1;
                        for (int j = 0; j < 2; j++)
                        {
                            if (!boolStates[39 + j])
                            {
                                boolStates[39 + j] = true;
                                index = j;
                                break;
                            }
                        }
                        if (intStates[5] == 0)
                        {
                            intStates[5] = 2;
                            GameObject.Find("Inca").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_2").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-774, -391);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Inca2VP");

                        }
                        else if (intStates[6] == 0)
                        {
                            intStates[6] = 2;
                            GameObject.Find("Aztecs").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_2").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-880, -271);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Aztecs2VP");
                        }
                        else
                        {
                            intStates[7] = 2;
                            GameObject.Find("Maya").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_2").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_2").GetComponent<RectTransform>().anchoredPosition = new Vector2(-840, -282);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Maya2VP");

                        }
                        GM1.bonusVPs[2]++;
                        GM1.updateVP();
                        onVP();
                    }
                    break;
                case 3:
                    if (randomIndex < 7)
                    {
                        GameObject.Destroy(GameObject.Find("conquest_3"));
                        conquests.RemoveAt(intStates[4]);
                    }
                    else if (randomIndex < 9)
                    {
                        GameObject.Destroy(GameObject.Find("conquest_3"));
                    }
                    else
                    {
                        int index = -1;
                        for (int j = 2; j < 4; j++)
                        {
                            if (!boolStates[39 + j])
                            {
                                boolStates[39 + j] = true;
                                index = j;
                                break;
                            }
                        }
                        if (intStates[5] == 0)
                        {
                            intStates[5] = 3;
                            GameObject.Find("Inca").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_3").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_3").GetComponent<RectTransform>().anchoredPosition = new Vector2(-774, -391);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Inca2VP");

                        }
                        else if (intStates[6] == 0)
                        {
                            intStates[6] = 3;
                            GameObject.Find("Aztecs").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_3").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_3").GetComponent<RectTransform>().anchoredPosition = new Vector2(-880, -271);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Aztecs2VP");
                        }
                        else
                        {
                            intStates[7] = 3;
                            GameObject.Find("Maya").GetComponent<RectTransform>().anchoredPosition = new Vector2(-879, 107 - 34 * index);
                            GameObject.Find("conquest_3").transform.SetParent(GameObject.Find("NewWorld").transform);
                            GameObject.Find("conquest_3").GetComponent<RectTransform>().anchoredPosition = new Vector2(-840, -282);
                            //handMarkerScript.bonus1.Add("Sprites/jpg/NewWorld/Maya2VP");

                        }
                        GM1.bonusVPs[3]++;
                        GM1.updateVP();
                        onVP();
                    }
                    break;
            }

        }
        currentTextObject.reset();
        segment++;
        phase8();
    }

    void phase9()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        if (GM1.VPs.Max() >= 25)
        {
            int winner = Array.IndexOf(GM1.VPs, GM1.VPs.Max());
            GM1.player = winner;
            onPlayerChange();
            currentTextObject.post("You won the game via a standard victory!");
            return;
        }
        if (GM1.turn > 3)
        {
            int leader = Array.IndexOf(GM1.VPs, GM1.VPs.Max());
            bool canVictory = true;
            for (int i = 0; i < 6; i++)
            {
                if (i != leader && GM1.VPs[leader] - GM1.VPs[i] < 5)
                {
                    canVictory = false;
                }
            }
            if (canVictory)
            {
                GM1.player = leader;
                onPlayerChange();
                currentTextObject.post("You won the game via a domination victory!");
            }
        }
        if (GM1.turn == 9)
        {
            int winner = Array.IndexOf(GM1.VPs, GM1.VPs.Max());
            GM1.player = winner;
            onPlayerChange();
            currentTextObject.post("You won the game via a time limit victory!");
        }
        onPhaseEnd();
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
