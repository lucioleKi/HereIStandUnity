using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine;
using System;
using static EnumSpaceScript;
using static DeckScript;


public class GM1 : MonoBehaviour
{
    public static GM1 instance;
    public static int player;
    public static int CurrentCard;
    public static int turn;
    public static int phase;
    public static int segment;
    public static int impulse;
    public static bool[] skipped= new bool[6];
    public static int englishSpaces;
    public static int protestantSpaces;
    public static RulerClass[] rulers;
    public static int[] cardTracks;
    public static int[] VPs;
    public static bool[] maritalStatus;
    public static int[] translations = new int[6];
    public static bool[] excommunicated;
    public static int[] StPeters;
    public static Religion[] religiousInfluence;
    public static PowerObject[] powerObjects;
    public static int[,] diplomacyState;
    public static Status0 status0;
    public static Status1 status1;
    public static Status2 status2;
    public static Status3 status3;
    public static Status4 status4;
    public static Status5 status5;
    public static int[] bonusVPs;
    public static int piracyC;
    public static int chateauxC;
    public static Queue<string> toDo;

    public static GM1 Instance { 
        get { 
            if(instance == null)
            {
                UnityEngine.Debug.Log("GM not initiated.");
            }
            
            return instance; 
        }
    }
    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        instance = this;
        status0 = Resources.Load("Objects/Status6/s0") as Status0;
        status1 = Resources.Load("Objects/Status6/s1") as Status1;
        status2 = Resources.Load("Objects/Status6/s2") as Status2;
        status3 = Resources.Load("Objects/Status6/s3") as Status3;
        status4 = Resources.Load("Objects/Status6/s4") as Status4;
        status5 = Resources.Load("Objects/Status6/s5") as Status5;
        cardTracks = new int[6];
        cardTracks[0] = status0.cardTrack;
        cardTracks[1] = status1.cardTrack;
        cardTracks[2] = status2.cardTrack;
        cardTracks[3] = status3.cardTrack;
        cardTracks[4] = status4.cardTrack;
        cardTracks[5] = status5.protestantSpaces;
        piracyC = status0.piracyTrack;
        maritalStatus = new bool[status2.maritalStatus.Length];
        Array.Copy(status2.maritalStatus, maritalStatus, status2.maritalStatus.Length);
        excommunicated = new bool[status4.excommunicated.Length];
        Array.Copy(status4.excommunicated, excommunicated, status4.excommunicated.Length);
        translations = new int[6];
        Array.Copy(status5.translationInit, translations, 6);
        
        StPeters = new int[2];
        ScenarioObject scenario = Resources.Load("Objects/Scenario3/1517") as ScenarioObject;
        //turn = scenario.turnStart;
        turn = 3;
        phase = scenario.phaseStart;
        segment = 1;
        powerObjects = new PowerObject[10];
        powerObjects[0] = Resources.Load("Objects/Power10/PowerOttoman") as PowerObject;
        powerObjects[1] = Resources.Load("Objects/Power10/PowerHapsburgs") as PowerObject;
        powerObjects[2] = Resources.Load("Objects/Power10/PowerEngland") as PowerObject;
        powerObjects[3] = Resources.Load("Objects/Power10/PowerFrance") as PowerObject;
        powerObjects[4] = Resources.Load("Objects/Power10/PowerPapacy") as PowerObject;
        powerObjects[5] = Resources.Load("Objects/Power10/PowerProtestant") as PowerObject;
        powerObjects[6] = Resources.Load("Objects/Power10/PowerGenoa") as PowerObject;
        powerObjects[7] = Resources.Load("Objects/Power10/PowerHB") as PowerObject;
        powerObjects[8] = Resources.Load("Objects/Power10/PowerScotland") as PowerObject;
        powerObjects[9] = Resources.Load("Objects/Power10/PowerVenice") as PowerObject;

        rulers = new RulerClass[6];

        VPs = new int[6];
        bonusVPs = new int[6];
        for(int i = 0; i < 6; i++)
        {
            VPs[i] = scenario.VPs[i];
            RulerObject tempRuler = Resources.Load("Objects/Ruler6/Ruler"+i.ToString()) as RulerObject;
            rulers[i] = new RulerClass(tempRuler.index, tempRuler.name, tempRuler.adminRating, tempRuler.cardBonus);
            bonusVPs[i] = 0;
        }

        diplomacyState = new int[6, 11];
        Array.Clear(diplomacyState, 0, diplomacyState.Length);
        diplomacyState[1, 3] = 1;
        diplomacyState[3, 4] = 1;
        diplomacyState[0, 7] = 1;
        englishSpaces = status5.englishSpaces;
        protestantSpaces = status5.protestantSpaces;

        religiousInfluence = new Religion[134];
        
        Array.Clear(religiousInfluence, 0, 134);

        toDo = new Queue<string>();
        enq1("Protestant to play Luther's 95 Theses");
        player = 5;
        GM2.boolStates[22] = true;
    }

    

    void Update()
    {
        
    }

    public static void updateVP()
    {
        VPs[0] = status0.setVP(cardTracks[0]) + status0.piracy[piracyC] + bonusVPs[0];
        VPs[1] = status1.setVP(cardTracks[1]) + bonusVPs[1];
        UnityEngine.Debug.Log(VPs[1]);
        VPs[2] = status2.setVP(cardTracks[2]) + bonusVPs[2];
        VPs[3] = status3.setVP(cardTracks[3]) + status3.chateaux[chateauxC]+bonusVPs[3];
        UnityEngine.Debug.Log(VPs[3]);
        VPs[4] = status4.setVP(cardTracks[4])+15- status5.setVP(protestantSpaces) + bonusVPs[4] + StPeters[1];
        VPs[5] = status5.setVP(protestantSpaces) + bonusVPs[5];
    }

    public static void updateRuler(int power, int index)
    {
        RulerObject tempRuler = Resources.Load("Objects/Ruler6/Ruler" + index.ToString()) as RulerObject;
        rulers[power] = new RulerClass(tempRuler.index, tempRuler.name, tempRuler.adminRating, tempRuler.cardBonus);
        rulers[power].toString();
    }

    public static void clearAlliance()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (diplomacyState[i, j] == 2)
                {
                    diplomacyState[i, j] = 0;
                }
            }
        }
    }

    public static void enq1(string message)
    {
        toDo.Enqueue(message);
        TodoScript todoObject = GameObject.Find("TodoBox").GetComponent("TodoScript") as TodoScript;
        todoObject.put1();

    }

    public static void enq2(string message)
    {
        toDo.Enqueue(message);
        TodoScript todoObject = GameObject.Find("TodoBox").GetComponent("TodoScript") as TodoScript;
        todoObject.put2();
    }

    public static void deq1(int index)
    {
        
        TodoScript todoObject = GameObject.Find("TodoBox").GetComponent("TodoScript") as TodoScript;
        switch (index)
        {
            case 0:
                todoObject.moveUp();
                break;
            case 1:
                todoObject.check1();
                break;
            case 2:
                todoObject.check2();
                break;
            case 3:
                todoObject.remove2();
                break;
            default:
                break;

        }
    }


    public static void nextImpulse()
    {
        checkPass(player);
        GM2.onDeactivateSkip();
        bool allSkipped = skipped.Any(x => x == false);
        if (!allSkipped)
        {
            UnityEngine.Debug.Log("all skipped");
            GM2.onPhaseEnd();
            impulse = -1;
            return;
        }
        for(int i=0; i < 6; i++)
        {
            if (impulse == 5)
            {
                impulse = 0;
            }
            else
            {
                impulse++;
            }
            if (!skipped[impulse])
            {
                break;
            }
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        UnityEngine.Debug.Log("Active power: " + impulse.ToString());
        switch (impulse)
        {
            case 0:
                currentTextObject.post("Active power: Ottoman");
                break;
            case 1:
                currentTextObject.post("Active power: Hapsburg");
                break;
            case 2:
                currentTextObject.post("Active power: England");
                break;
            case 3:
                currentTextObject.post("Active power: France");
                break;
            case 4:
                currentTextObject.post("Active power: Papacy");
                break;
            case 5:
                currentTextObject.post("Active power: Protestant");
                break;
        }
        if (checkPass(impulse))
        {
            GM2.onSkipCard(6);
        }

    }

    public static bool checkPass(int playerIndex)
    {
        List<CardObject> temp = hand0;
        switch (playerIndex)
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
        //force pass
        if (temp.Count == 0)
        {
            UnityEngine.Debug.Log("no cards left");
            skipped[playerIndex] = true;
            return true;
        }
        //unplayed home card
        if (temp.ElementAt(0).id > 8)
        {
            return false;
        }
        //unplayed mandatory event
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp.ElementAt(i).cardType == (CardType)1)
            {
                return false;
            }
        }
        //more cards in hand than leader's admin rating
        if (temp.Count > rulers[playerIndex].adminRating)
        {
            return false;
        }
        return true;
    }
}
