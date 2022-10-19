using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static EnumSpaceScript;

public class GM1 : MonoBehaviour
{
    public static GM1 instance;
    public int turn;
    public int phase;
    
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
        instance.turn = 1;
        instance.phase = 1;
    }

    void Update()
    {

    }
}
