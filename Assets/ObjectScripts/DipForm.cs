using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using static GM2;
using TMPro;

public class DipForm : ScriptableObject
{
    public bool[] completed = new bool[6];
    public int[,] dipStatus = new int[6,6];
    public int[,] loanSquadron = new int[6,6];
    public int[,] randomDraw = new int[6,6];
    public int[,] giveMerc = new int[6,6];
    // Start is called before the first frame update
    void Start()
    {
        Array.Clear(completed, 0, 6);
        Array.Clear(dipStatus, 0, 36);
        Array.Clear(loanSquadron, 0, 36);
        Array.Clear(randomDraw, 0, 36);
        Array.Clear(giveMerc, 0, 36);
        
    }

    void OnEnable()
    {
        Array.Clear(completed, 0, 6);
        Array.Clear(dipStatus, 0, 36);
        Array.Clear(loanSquadron, 0, 36);
        Array.Clear(randomDraw, 0, 36);
        Array.Clear(giveMerc, 0, 36);
        onConfirmDipForm += confirmDip;
    }

    void OnDisable()
    {
        onConfirmDipForm -= confirmDip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void confirmDip(int playerIndex)
    {
        //UnityEngine.Debug.Log(playerIndex.ToString() + "finished");
        if (completed[playerIndex])
        {
            return;
        }
        else
        {
            completed[playerIndex] = true;
        }
        if (GameObject.Find("EndWar").GetComponent<Toggle>().isOn)
        {
            GameObject.Find("EndWar").GetComponent<Toggle>().isOn = false;
            if (GameObject.Find("EndWar_0").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 0] = 1;
                GameObject.Find("EndWar_0").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("EndWar_1").GetComponent<Toggle>().isOn) {
                dipStatus[playerIndex, 1] = 1;
                GameObject.Find("EndWar_1").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("EndWar_2").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 2] = 1;
                GameObject.Find("EndWar_2").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("EndWar_3").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 3] = 1;
                GameObject.Find("EndWar_3").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("EndWar_4").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 4] = 1;
                GameObject.Find("EndWar_4").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("EndWar_5").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 5] = 1;
                GameObject.Find("EndWar_5").GetComponent<Toggle>().isOn = false;
            }

        }

        if (GameObject.Find("Alliance").GetComponent<Toggle>().isOn)
        {
            GameObject.Find("Alliance").GetComponent<Toggle>().isOn = false;
            if (GameObject.Find("Alliance_0").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 0] = 2;
                GameObject.Find("Alliance_0").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("Alliance_1").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 1] = 2;
                GameObject.Find("Alliance_1").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("Alliance_2").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 2] = 2;
                GameObject.Find("Alliance_2").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("Alliance_3").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 3] = 2;
                GameObject.Find("Alliance_3").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("Alliance_4").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 4] = 2;
                GameObject.Find("Alliance_4").GetComponent<Toggle>().isOn = false;
            }
            else if (GameObject.Find("Alliance_5").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 5] = 2;
                GameObject.Find("Alliance_5").GetComponent<Toggle>().isOn = false;
            }

        }

        for(int i = 0; i < 6; i++)
        {
            
            UnityEngine.Debug.Log(GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text);
            
            if(!string.IsNullOrEmpty(GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text))
            {
                loanSquadron[playerIndex, i] = int.Parse(GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text);
                GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text = "";
            }
            if (!string.IsNullOrEmpty(GameObject.Find("RandomDraw_" + i.ToString()).GetComponent<TMP_InputField>().text))
            {
                randomDraw[playerIndex, i] = int.Parse(GameObject.Find("RandomDraw_" + i.ToString()).GetComponent<TMP_InputField>().text);
                GameObject.Find("RandomDraw_" + i.ToString()).GetComponent<TMP_InputField>().text = "";
            }
                
            if(randomDraw[playerIndex, i] < 0 || randomDraw[playerIndex, i] > 2)
            {
                randomDraw[playerIndex, i] = 0;
            }
            if (i != 0)
            {
                if (!string.IsNullOrEmpty(GameObject.Find("GiveMerc_" + i.ToString()).GetComponent<TMP_InputField>().text))
                {
                    giveMerc[playerIndex, i] = int.Parse(GameObject.Find("GiveMerc_" + i.ToString()).GetComponent<TMP_InputField>().text);
                    GameObject.Find("GiveMerc_" + i.ToString()).GetComponent<TMP_InputField>().text = "";
                }
                    
                if (giveMerc[playerIndex, i] < 0 || giveMerc[playerIndex, i] > 4)
                {
                    giveMerc[playerIndex, i] = 0;
                }
            }
            
        }
    }

}
