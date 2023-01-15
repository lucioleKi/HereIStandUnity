using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
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
    public List<int> turnData = new List<int>();
    public int[,] war = new int[6, 8];
    // Start is called before the first frame update
    void Start()
    {
        Array.Clear(completed, 0, 6);
        Array.Clear(dipStatus, 0, 36);
        Array.Clear(loanSquadron, 0, 36);
        Array.Clear(randomDraw, 0, 36);
        Array.Clear(giveMerc, 0, 36);
        for(int i=0; i<6; i++)
        {
            for(int j=0; j<8; j++)
            {
                war[i, j] = 10;
            }
        }
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

    public void reset()
    {
        Array.Clear(completed, 0, 6);
        Array.Clear(dipStatus, 0, 36);
        Array.Clear(loanSquadron, 0, 36);
        Array.Clear(randomDraw, 0, 36);
        Array.Clear(giveMerc, 0, 36);
        Array.Fill(war.OfType<int>().ToArray(), 10);
    }

    public void confirmDip(int playerIndex)
    {
        if (!completed[playerIndex])
        {
            completed[playerIndex] = true;
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                dipStatus[playerIndex, i] = 0;
                loanSquadron[playerIndex, i] = 0;
                randomDraw[playerIndex, i] = 0;
                giveMerc[playerIndex, i] = 0;
            }
        }


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
        UnityEngine.Debug.Log(dipStatus[1, 3]);
        UnityEngine.Debug.Log(dipStatus[3, 1]);

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (i != j && dipStatus[i, j] != dipStatus[j, i]||i==j)
                {
                    dipStatus[i, j] = 0;
                    dipStatus[j, i] = 0;
                    UnityEngine.Debug.Log(i.ToString() + " and " + j.ToString() + " did not reach an agreement");

                }
                if (i < j && diplomacyState[i, j] == 1 && dipStatus[i, j] != 1)
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
                if (i != j && dipStatus[i, j] == 2 && loanSquadron[i, j] != 0 && loanSquadron[j, i] != 0)
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
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if ((i >3||i<1)||(j>3||j<1))
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
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        //TODO 9.3.4 remove units
        completed[playerIndex] = true;
        if (GameObject.Find("PeaceRequest_0").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 0] = 1;
            GameObject.Find("PeaceRequest_0").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 0] == 1|| GM1.diplomacyState[0, playerIndex] == 1)
            {
                turnData.Add(playerIndex);
                turnData.Add(0);
                GM1.diplomacyState[playerIndex, 0] = 0;
                GM1.diplomacyState[0, playerIndex] = 0;
                GM2.onChangeDip();
                handMarkerScript.bonus0.Add("Sprites/jpg/WarWinner2VP");
                GM1.bonusVPs[0] += 2;
            }
        }
        if (GameObject.Find("PeaceRequest_1").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 1] = 1;
            GameObject.Find("PeaceRequest_1").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 1] == 1|| GM1.diplomacyState[1, playerIndex] == 1)
            {
                turnData.Add(playerIndex);
                turnData.Add(1);
                GM1.diplomacyState[playerIndex, 1] = 0;
                GM1.diplomacyState[1, playerIndex] = 0;
                GM2.onChangeDip();
                if (playerIndex == 0)
                {
                    handMarkerScript.bonus1.Add("Sprites/jpg/WarWinner2VP");
                    GM1.bonusVPs[1] += 2;
                }
                else
                {
                    handMarkerScript.bonus1.Add("Sprites/jpg/WarWinner1VP");
                    GM1.bonusVPs[1] += 1;
                }

            }
        }
        if (GameObject.Find("PeaceRequest_2").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 2] = 1;
            GameObject.Find("PeaceRequest_2").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 2] == 1 || GM1.diplomacyState[2, playerIndex] == 1)
            {
                turnData.Add(playerIndex);
                turnData.Add(2);
                GM1.diplomacyState[playerIndex, 2] = 0;
                GM1.diplomacyState[2, playerIndex] = 0;
                GM2.onChangeDip();
                if (playerIndex == 0)
                {
                    handMarkerScript.bonus2.Add("Sprites/jpg/WarWinner2VP");
                    GM1.bonusVPs[2] += 2;
                }
                else
                {
                    handMarkerScript.bonus2.Add("Sprites/jpg/WarWinner1VP");
                    GM1.bonusVPs[2] += 1;
                }
            }
        }
        if (GameObject.Find("PeaceRequest_3").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 3] = 1;
            GameObject.Find("PeaceRequest_3").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 3] == 1 || GM1.diplomacyState[3, playerIndex] == 1)
            {
                turnData.Add(playerIndex);
                turnData.Add(3);
                GM1.diplomacyState[playerIndex, 3] = 0;
                GM1.diplomacyState[3, playerIndex] = 0;
                GM2.onChangeDip();
                if (playerIndex == 0)
                {
                    handMarkerScript.bonus3.Add("Sprites/jpg/WarWinner2VP");
                    GM1.bonusVPs[3] += 2;
                }
                else
                {
                    handMarkerScript.bonus3.Add("Sprites/jpg/WarWinner1VP");
                    GM1.bonusVPs[3] += 1;
                }
            }
        }
        if (GameObject.Find("PeaceRequest_4").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 4] = 1;
            GameObject.Find("PeaceRequest_4").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 4] == 1 || GM1.diplomacyState[4, playerIndex] == 1)
            {
                turnData.Add(playerIndex);
                turnData.Add(4);
                GM1.diplomacyState[playerIndex, 4] = 0;
                GM1.diplomacyState[4, playerIndex] = 0;
                GM2.onChangeDip();
                if (playerIndex == 0)
                {
                    handMarkerScript.bonus4.Add("Sprites/jpg/WarWinner2VP");
                    GM1.bonusVPs[4] += 2;
                }
                else
                {
                    handMarkerScript.bonus4.Add("Sprites/jpg/WarWinner1VP");
                    GM1.bonusVPs[4] += 1;
                }
            }
        }
        if (GameObject.Find("PeaceRequest_5").GetComponent<Toggle>().isOn)
        {
            dipStatus[playerIndex, 5] = 1;
            GameObject.Find("PeaceRequest_5").GetComponent<Toggle>().isOn = false;
            if (GM1.diplomacyState[playerIndex, 5] == 1 || GM1.diplomacyState[5, playerIndex] == 1)
            {
                turnData.Add(playerIndex);
                turnData.Add(5);
                GM1.diplomacyState[playerIndex, 5] = 0;
                GM1.diplomacyState[5, playerIndex] = 0;
                GM2.onChangeDip();
                if (playerIndex == 0)
                {
                    handMarkerScript.bonus5.Add("Sprites/jpg/WarWinner2VP");
                    GM1.bonusVPs[5] += 2;
                }
                else
                {
                    handMarkerScript.bonus5.Add("Sprites/jpg/WarWinner1VP");
                    GM1.bonusVPs[5] += 1;
                }
            }
        }
        GM1.updateVP();
        GM2.onVP();
    }

    public List<int> DOW(int playerindex)
    {
        List<int> trace = new List<int>();
        for(int i=0; i<10; i++)
        {
            if(i!=playerindex&&canDOW(playerindex, i))
            {
                trace.Add(i);
            }
        }
        return trace;
    }

    public bool canDOW(int playerindex, int index)
    {
        //cannot DOW on at war powers
        if (diplomacyState[playerindex, index]==1|| (index < 5 && diplomacyState[index, playerindex] == 1))
        {
            return false;
        }
        //cannot DOW on Hungary-Bohemia
        if(index==7)
        {
            return false;
        }
        //cannot DOW on scotland
        if (index == 8)
        {
            if(playerindex== 0||playerindex==4||playerindex==5) { return false; }
            if (diplomacyState[playerindex,3] == 2|| diplomacyState[3,playerindex] == 2) { return false; }
        }
        //cannot DOW on venice if 2
        if (index == 9 && playerindex == 2) { return false; }
        //cannot DOW on allied minor power
        if (index > 5)
        {
            for(int i=0; i<5; i++)
            {
                if (diplomacyState[i, index] == 2) { return false; }
            }
        }
        //cannot DOW on allies
        if (diplomacyState[playerindex, index]==2||(index<5&& diplomacyState[index, playerindex] == 2)) { return false; }
        //cannot DOW on 5 if HIS013 is not played
        if ((playerindex==5||index == 5) && !boolStates[13]) { return false; }
        //cannot DOW on a power that just got peace
        for(int i=0; i < turnData.Count; i += 2)
        {
            if (turnData[i] == playerindex && turnData[i+1]==index|| turnData[i] == index && turnData[i + 1] == playerindex)
            {
                return false;
            }
        }
        return true;
    }

    public List<int> getDOWCP()
    {
        UnityEngine.Debug.Log(String.Join(" ", war.Cast<int>()));
        int[,] warCP = new int[6, 10]; 
        using (var reader = new StreamReader("Assets/Input/war.csv"))
        {
            int counter = 0;
            while (!reader.EndOfStream)
            {
                
                var line = reader.ReadLine();
                var values = line.Split(',');
                for (int i = 0; i < 10; i++)
                {
                    warCP[counter, i] = int.Parse(values[i]);

                }

                counter++;
            }


        }
        List<int> trace = new List<int>();
        for(int i=0;i<6;i++)
        {
            int temp = 0;
            for(int j=0; j<8; j++)
            {
                if (war[i,j]!=10) { temp += warCP[i, war[i, j]]; }
                UnityEngine.Debug.Log(i + ", " + j + ", " + warCP[i, j]);
            }
            trace.Add(temp);
        }
        return trace;
    }
}
