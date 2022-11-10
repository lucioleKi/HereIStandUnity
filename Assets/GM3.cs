using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GM3 : MonoBehaviour
{
    public static GM3 instance;
    public static GM3 Instance
    {
        get
        {
            if (instance == null)
            {
                UnityEngine.Debug.Log("GM3 not initiated.");
            }
            return instance;
        }
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
