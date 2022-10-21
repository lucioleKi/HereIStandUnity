using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.Versioning;
using UnityEngine;
using static EnumSpaceScript;

public class GM1 : MonoBehaviour
{
    public static GM1 instance;
    public static int turn;
    public static int phase;
    public static PowerObject[] powerObjects;
    public static int[] VPs;
    
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
        turn = 1;
        phase = 1;
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
            VPs[i] = powerObjects[i].initialVP;
        }
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
