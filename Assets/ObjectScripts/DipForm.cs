using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using static GM2;
using static GM1;
using TMPro;

public class DipForm : MonoBehaviour
{
    public bool[] completed = new bool[6];
    public int[,] dipStatus = new int[6, 6];
    public int[,] loanSquadron = new int[6, 6];
    public int[,] randomDraw = new int[6, 6];
    public int[,] giveMerc = new int[6, 6];
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
        onChangeSegment += clearCompleted;
        onConfirmPeace += peace29;
    }

    void OnDisable()
    {
        onConfirmDipForm -= confirmDip;
        onChangeSegment -= clearCompleted;
        onConfirmPeace -= peace29;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void confirmDip(int playerIndex)
    {
        //UnityEngine.Debug.Log(playerIndex.ToString() + "finished");
        if (!completed[playerIndex])
        {
            completed[playerIndex] = true;
        }
        else
        {
            for(int i = 0; i < 6; i++)
            {
                dipStatus[playerIndex, i] = 0;
                loanSquadron[playerIndex, i] = 0;
                randomDraw[playerIndex, i] = 0;
                giveMerc[playerIndex, i] = 0;
            }
        }
        
        if (GameObject.Find("EndWar").GetComponent<Toggle>().isOn)
        {
            
            if (GameObject.Find("EndWar_0").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 0] = 1;
                GameObject.Find("EndWar_0").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("EndWar_1").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 1] = 1;
                GameObject.Find("EndWar_1").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("EndWar_2").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 2] = 1;
                GameObject.Find("EndWar_2").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("EndWar_3").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 3] = 1;
                GameObject.Find("EndWar_3").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("EndWar_4").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 4] = 1;
                GameObject.Find("EndWar_4").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("EndWar_5").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 5] = 1;
                GameObject.Find("EndWar_5").GetComponent<Toggle>().isOn = false;
            }
            GameObject.Find("EndWar").GetComponent<Toggle>().isOn = false;
        }

        if (GameObject.Find("Alliance").GetComponent<Toggle>().isOn)
        {
            
            if (GameObject.Find("Alliance_0").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 0] = 2;
                GameObject.Find("Alliance_0").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("Alliance_1").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 1] = 2;
                GameObject.Find("Alliance_1").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("Alliance_2").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 2] = 2;
                GameObject.Find("Alliance_2").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("Alliance_3").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 3] = 2;
                GameObject.Find("Alliance_3").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("Alliance_4").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 4] = 2;
                GameObject.Find("Alliance_4").GetComponent<Toggle>().isOn = false;
            }
            if (GameObject.Find("Alliance_5").GetComponent<Toggle>().isOn)
            {
                dipStatus[playerIndex, 5] = 2;
                GameObject.Find("Alliance_5").GetComponent<Toggle>().isOn = false;
            }
            GameObject.Find("Alliance").GetComponent<Toggle>().isOn = false;
        }

        for (int i = 0; i < 6; i++)
        {

            UnityEngine.Debug.Log(GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text);

            if (!string.IsNullOrEmpty(GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text))
            {
                loanSquadron[playerIndex, i] = int.Parse(GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text);
                GameObject.Find("NumberSquadron_" + i.ToString()).GetComponent<TMP_InputField>().text = "";
            }
            if (!string.IsNullOrEmpty(GameObject.Find("RandomDraw_" + i.ToString()).GetComponent<TMP_InputField>().text))
            {
                randomDraw[playerIndex, i] = int.Parse(GameObject.Find("RandomDraw_" + i.ToString()).GetComponent<TMP_InputField>().text);
                GameObject.Find("RandomDraw_" + i.ToString()).GetComponent<TMP_InputField>().text = "";
            }

            if (randomDraw[playerIndex, i] < 0 || randomDraw[playerIndex, i] > 2)
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

    public void verifyDip()
    {
        if (turn == 1)
        {
            verifyTurn1();
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (i != j && dipStatus[i, j] != dipStatus[j, i])
                {
                    dipStatus[i, j] = 0;
                    dipStatus[j, i] = 0;
                    UnityEngine.Debug.Log(i.ToString() + " and " + j.ToString() + " did not reach an agreement");

                }
                if (i < j && diplomacyState[i, j] == 1 && dipStatus[i, j] == 2)
                {
                    dipStatus[i, j] = 0;
                    dipStatus[j, i] = 0;
                    UnityEngine.Debug.Log(i.ToString() + " and " + j.ToString() + " are still at war");
                }
                
                if (i != j && randomDraw[i, j] != 0 && randomDraw[j, i] != 0)
                {
                    randomDraw[i, j] = 0;
                    randomDraw[j, i] = 0;
                    UnityEngine.Debug.Log(i.ToString() + " and " + j.ToString() + " cannot trade draws to each other.");
                }
                if (i != j && giveMerc[i, j] != 0 && giveMerc[j, i] != 0)
                {
                    giveMerc[i, j] = 0;
                    giveMerc[j, i] = 0;
                    UnityEngine.Debug.Log(i.ToString() + " and " + j.ToString() + " cannot give merc to each other.");
                }
            }
        }
        if (dipStatus[0, 4] == 2 || dipStatus[4, 0] == 2)
        {
            dipStatus[0, 4] = 0;
            dipStatus[4, 0] = 0;
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (i != j && dipStatus[i, j]==2 && loanSquadron[i, j] != 0 && loanSquadron[j, i] != 0)
                {
                    loanSquadron[i, j] = 0;
                    loanSquadron[j, i] = 0;
                    UnityEngine.Debug.Log(i.ToString() + " and " + j.ToString() + " cannot loan squadrons to each other.");
                }
            }
        }
    }

    public void verifyTurn1()
    {
        for(int i=0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if (i != 2 && j != 2)
                {
                    dipStatus[i, j] = 0;
                    dipStatus[j, i] = 0;
                    loanSquadron[i, j] = 0;
                    loanSquadron[j, i] = 0;
                    randomDraw[i, j] = 0;
                    randomDraw[j, i] = 0;
                    giveMerc[i, j] = 0;
                    giveMerc[j, i] = 0;
                }
            }
        }
    }

    public void clearCompleted()
    {
        Array.Clear(completed, 0, 6);
        Array.Clear(dipStatus, 0, 6);
    }

    public void peace29(int playerIndex)
    {
        if (GameObject.Find("PeaceRequest_0").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 0] = 1;
            GameObject.Find("PeaceRequest_0").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 0] == 1)
            {

            }
        }
        if (GameObject.Find("PeaceRequest_1").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 1] = 1;
            GameObject.Find("PeaceRequest_1").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 1] == 1)
            {

            }
        }
        if (GameObject.Find("PeaceRequest_2").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 2] = 1;
            GameObject.Find("PeaceRequest_2").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 2] == 1)
            {

            }
        }
        if (GameObject.Find("PeaceRequest_3").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 3] = 1;
            GameObject.Find("PeaceRequest_3").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 3] == 1)
            {

            }
        }
        if (GameObject.Find("PeaceRequest_4").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 4] = 1;
            GameObject.Find("PeaceRequest_4").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 4] == 1)
            {

            }
        }
        if (GameObject.Find("PeaceRequest_5").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 5] = 1;
            GameObject.Find("PeaceRequest_5").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 5] == 1)
            {

            }
        }
    }
}
