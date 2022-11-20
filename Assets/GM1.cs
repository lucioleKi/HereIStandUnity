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
    public static int englishSpaces;
    public static int protestantSpaces;
    public static RulerClass[] rulers;
    public static int[] cardTracks;
    public static int[] VPs;
    public static int[] translations;
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
        excommunicated = status4.excommunicated;
        translations = status5.translations;

        ScenarioObject scenario = Resources.Load("Objects/Scenario3/1517") as ScenarioObject;
        turn = scenario.turnStart;
        phase = scenario.phaseStart;
        
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
            rulers[i] = new RulerClass(tempRuler.name, tempRuler.adminRating, tempRuler.cardBonus);
            bonusVPs[i] = 0;
        }

        diplomacyState = new int[6, 10];
        Array.Clear(diplomacyState, 0, diplomacyState.Length);
        diplomacyState[1, 3] = 1;
        diplomacyState[3, 4] = 1;
        diplomacyState[0, 7] = 1;
        englishSpaces = status5.englishSpaces;
        protestantSpaces = status5.protestantSpaces;

        religiousInfluence = new Religion[134];
        
        Array.Clear(religiousInfluence, 0, 134);



        player = 5;

    }

    

    void Update()
    {
        
    }

    public static void updateVP()
    {
        VPs[0] = status0.setVP(cardTracks[0]) + status0.piracy[piracyC] + bonusVPs[0];
        VPs[1] = status1.setVP(cardTracks[1]) + bonusVPs[1];
        VPs[2] = status2.setVP(cardTracks[2]) + bonusVPs[2];
        VPs[3] = status3.setVP(cardTracks[3]) + status3.chateaux[chateauxC]+bonusVPs[3];
        VPs[4] = status4.setVP(cardTracks[4])+15- status5.setVP(protestantSpaces) + bonusVPs[4];
        VPs[5] = status5.setVP(protestantSpaces) + bonusVPs[5];
    }

    public static void updateRuler(int power, int index)
    {
        RulerObject tempRuler = Resources.Load("Objects/Ruler6/Ruler" + index.ToString()) as RulerObject;
        rulers[power] = new RulerClass(tempRuler.name, tempRuler.adminRating, tempRuler.cardBonus);
        rulers[power].toString();
    }

    void checkWin()
    {
        if (false)
        {
            //win!
        }
    }
}
