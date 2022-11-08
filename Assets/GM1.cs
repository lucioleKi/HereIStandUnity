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
    public static int protestantSpaces;
    public static int englishSpaces;
    public static Religion[] religiousInfluence;
    public static PowerObject[] powerObjects;
    public static int[] VPs;
    public static int[,] diplomacyState;
    public static Status0 status0;
    public static Status1 status1;
    public static Status2 status2;
    public static Status3 status3;
    public static Status4 status4;
    public static Status5 status5;

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
        
        
        instance = this;
        status0 = Resources.Load("Objects/Status6/s0") as Status0;
        status1 = Resources.Load("Objects/Status6/s1") as Status1;
        status2 = Resources.Load("Objects/Status6/s2") as Status2;
        status3 = Resources.Load("Objects/Status6/s3") as Status3;
        status4 = Resources.Load("Objects/Status6/s4") as Status4;
        status5 = Resources.Load("Objects/Status6/s5") as Status5;

        Screen.SetResolution(1920, 1080, true);
        
        player = 5;
        ScenarioObject scenario = Resources.Load("Objects/Scenario3/1517") as ScenarioObject;
        turn = scenario.turnStart;
        phase = scenario.phaseStart;
        protestantSpaces = scenario.protestantSpaces;
        englishSpaces = scenario.englishSpaces;
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
        VPs = new int[6];
        for(int i = 0; i < 6; i++)
        {
            VPs[i] = scenario.VPs[i];
        }
        diplomacyState = new int[6, 10];
        Array.Clear(diplomacyState, 0, diplomacyState.Length);
        diplomacyState[1, 3] = 1;
        diplomacyState[3, 4] = 1;
        diplomacyState[0, 7] = 1;
        
        
        religiousInfluence = new Religion[134];
        
        Array.Clear(religiousInfluence, 0, 134);

        
        
        

    }

    

    void Update()
    {
        
    }

    void checkWin()
    {
        if (VPs.Contains(25))
        {
            //win!
        }
    }
}
