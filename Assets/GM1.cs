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
