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
    public static List<SpaceGM> spacesGM;
    public static int[] regulars;
    public static int[] regularsPower;
    
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
        Screen.SetResolution(1920, 1080, true);
        instance = this;
        
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
        spacesGM = new List<SpaceGM>();
        regulars = new int[instanceDeck.spaces.Count() + 6];
        religiousInfluence = new Religion[instanceDeck.spaces.Count()];
        regularsPower = new int[instanceDeck.spaces.Count];
        Array.Clear(regulars, 0, instanceDeck.spaces.Count() + 6);
        Array.Clear(religiousInfluence, 0, instanceDeck.spaces.Count());

        for (int i = 1; i <= instanceDeck.spaces.Count(); i++) {
            CitySetup temp = Resources.Load("Objects/1517/" + i.ToString()) as CitySetup;
            if (temp != null&&i>6) {
                SpaceGM temp1 = new SpaceGM(temp);
                spacesGM.Add(temp1);
                regulars[i - 1] = temp.regular;
                regularsPower[i - 1] = temp.controlPower;
            }
            else
            {
                SpaceGM temp1 = new SpaceGM();
                temp1.name = instanceDeck.spaces.ElementAt(i - 1).name;
                temp1.id = i;
                temp1.controlPower = (int)instanceDeck.spaces.ElementAt(i - 1).homePower;
                regularsPower[i - 1] = (int)instanceDeck.spaces.ElementAt(i - 1).homePower;
                spacesGM.Add(temp1);
            }
        }
        regulars[134] = 2;
        regulars[135] = 1;
        regulars[136] = 1;
        regulars[137] = 1;
        regulars[138] = 1;
        regulars[139] = 2;

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
