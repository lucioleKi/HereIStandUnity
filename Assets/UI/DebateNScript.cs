using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static EnumSpaceScript;
using static DeckScript;
using static GM2;

public class DebateNScript : MonoBehaviour
{
    public Button btn;
    public int step;
    public bool specify4;
    public List<int> count41;
    public List<int> count42;
    public List<int> count51;
    public List<int> count52;
    public int attackerIndex;
    public int defenderIndex;
    public bool uncommitted;
    public int hit4;
    public int hit5;
    // Start is called before the first frame update
    void Start()
    {
        specify4 = false;
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack());
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        count41 = new List<int>();
        count42 = new List<int>();
        count51 = new List<int>();
        count52 = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void buttonCallBack()
    {
        DebatersScript debatersScript = GameObject.Find("DebaterDisplay").GetComponent("DebatersScript") as DebatersScript;
        InputToggleObject inputToggleObject = GameObject.Find("InputToggle").GetComponent("InputToggleObject") as InputToggleObject;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        int area = 0;
        switch (step)
        {
            case 0:
                if (!string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
                {
                    area = int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
                    if (area < 0 || area > 2)
                    {
                        area = 0;
                    }
                }
                else
                {
                    area = 0;
                }

                countDebaters(area);

                UnityEngine.Debug.Log("count41 " + count41.Count().ToString());
                if (GM1.player == 4)
                {
                    int randomIndex = UnityEngine.Random.Range(0, count41.Count());
                    attackerIndex = count41.ElementAt(randomIndex) - 1;
                    debaters.ElementAt(attackerIndex).status = (DebaterStatus)7;
                    debatersScript.putPapal(attackerIndex);
                    debatersScript.updateDebater();

                }
                else
                {
                    int randomIndex = UnityEngine.Random.Range(0, count51.Count());
                    attackerIndex = count51.ElementAt(randomIndex) - 1;
                    debaters.ElementAt(attackerIndex).status = (DebaterStatus)7;
                    debatersScript.putProtestant(attackerIndex);
                    debatersScript.updateDebater();
                }
                currentTextObject.post("Choose committed enemy debaters?\nDefault: Choose uncommitted enemy debaters");
                inputNumberObject.reset();
                inputToggleObject.reset();
                inputToggleObject.post();
                step++;
                break;
            case 1:

                if (GameObject.Find("InputToggle").GetComponent<Toggle>().isOn)
                {
                    uncommitted = false;
                }
                else
                {
                    uncommitted = true;
                }
                countDebaters(area);
                UnityEngine.Debug.Log("count51 " + count51.Count().ToString());
                if (GM1.player == 4)
                {
                    if (uncommitted || count52.Count() == 0)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, count51.Count());
                        defenderIndex = count51.ElementAt(randomIndex) - 1;
                        debatersScript.putProtestant(defenderIndex);
                        debaters.ElementAt(defenderIndex).status = (DebaterStatus)7;
                        debatersScript.putProtestant(defenderIndex);
                        debatersScript.updateDebater();

                    }
                    else
                    {
                        int randomIndex = UnityEngine.Random.Range(0, count52.Count());
                        defenderIndex = count52.ElementAt(randomIndex) - 1;
                        debatersScript.putProtestant(defenderIndex);
                        debaters.ElementAt(defenderIndex).status = (DebaterStatus)7;
                        debatersScript.putProtestant(defenderIndex);
                        debatersScript.updateDebater();
                    }
                }
                else
                {
                    if (uncommitted || count42.Count() == 0)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, count41.Count());
                        defenderIndex = count41.ElementAt(randomIndex) - 1;
                        debatersScript.putProtestant(defenderIndex);
                        debaters.ElementAt(defenderIndex).status = (DebaterStatus)7;
                        debatersScript.putPapal(defenderIndex);
                        debatersScript.updateDebater();
                    }
                    else
                    {
                        int randomIndex = UnityEngine.Random.Range(0, count42.Count());
                        defenderIndex = count42.ElementAt(randomIndex) - 1;
                        debatersScript.putProtestant(defenderIndex);
                        debaters.ElementAt(defenderIndex).status = (DebaterStatus)7;
                        debatersScript.putPapal(defenderIndex);
                        debatersScript.updateDebater();
                    }
                }
                step++;
                break;
            case 2:
                inputToggleObject.reset();
                int result = step46();
                switch (result)
                {
                    case 1:
                        step = step + 2;
                        break;
                    case 0:
                        step++;
                        break;
                    case -1:
                        step = step + 3;
                        break;
                }
                break;
            case 3:
                //reroll
                break;
            case 4:

                //StartCoroutine(step79);
                step++;
                break;
            case 5:
                step++;
                break;
            default:
                break;
        }

    }

    public void post()
    {
        btn.interactable = true;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().interactable = true;

    }

    void countDebaters(int area)
    {
        count41.Clear();
        count42.Clear();
        count51.Clear();
        count52.Clear();
        for (int i = 0; i < debaters.Count(); i++)
        {
            //UnityEngine.Debug.Log(i.ToString()+" status +" + debaters.ElementAt(i).status+" type "+ debaters.ElementAt(i).type.ToString()+" language "+ debaters.ElementAt(i).language);
            if (debaters.ElementAt(i).status == (DebaterStatus)1 && debaters.ElementAt(i).type == 0)
            {
                UnityEngine.Debug.Log("count41 +" + (i + 1).ToString());
                count41.Add(i + 1);
            }
            else if (debaters.ElementAt(i).status == (DebaterStatus)2 && debaters.ElementAt(i).type == 0)
            {
                count42.Add(i + 1);
            }
            if (debaters.ElementAt(i).status == (DebaterStatus)1 && debaters.ElementAt(i).type == 1)
            {
                UnityEngine.Debug.Log("count51 +" + (i + 1).ToString());
                count51.Add(i + 1);
            }
            else if (debaters.ElementAt(i).status == (DebaterStatus)2 && debaters.ElementAt(i).type == 1)
            {
                count52.Add(i + 1);
            }
        }
    }

    int step46()
    {
        //add up debater dice
        int reformerDice = 0;
        int papalDice = 0;
        int hit4 = 0;
        int hit5 = 0;

        if (GM1.player == 4)
        {
            if (uncommitted)
            {
                reformerDice = debaters.ElementAt(attackerIndex).value + 2;
            }
            else
            {
                reformerDice = debaters.ElementAt(attackerIndex).value + 1;
            }
            papalDice = debaters.ElementAt(defenderIndex).value + 3;
        }
        else
        {
            if (uncommitted)
            {
                papalDice += debaters.ElementAt(attackerIndex).value + 2;
            }
            else
            {
                papalDice += debaters.ElementAt(attackerIndex).value + 1;
            }
            reformerDice += debaters.ElementAt(defenderIndex).value + 3;

        }

        if (attackerIndex == 0 || attackerIndex == 11)
        {
            papalDice++;
        }
        //roll the dices
        for (int i = 0; i < papalDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);

            if (randomIndex == 6 || randomIndex == 5)
            {
                hit4++;

            }
        }
        for (int i = 0; i < reformerDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);

            if (randomIndex == 6 || randomIndex == 5)
            {
                hit5++;

            }
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Papal hit: " + hit4.ToString() +" out of "+papalDice.ToString()+ ".\nProtestant hit: " + hit5.ToString() + " out of " + reformerDice.ToString() + ".");
        if (hit4 > hit5)
        {
            //aleander
            if (attackerIndex == 2)
            {
                hit4++;
            }
            return 1;
        }else if (hit5 > hit4)
        {
            //campeggio
            if (attackerIndex == 1)
            {
                int randomIndex = UnityEngine.Random.Range(1, 7);
                if (randomIndex == 6 || randomIndex == 5)
                {
                    //ignored
                    return -1;
                }
            }
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public IEnumerator step79()
    {
        //resolve
        if (hit4 > hit5)
        {
            for (int i = 0; i < hit4 - hit5; i++)
            {

                List<int> pickSpaces = GM2.highlightReformation();
                GM2.highlightSelected = -1;
                onHighlight(pickSpaces);

                onHighlightSelected += changeReligion;
                while (GM2.highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }

                UnityEngine.Debug.Log("end");
                //onRemoveHighlight(converted);
            }
        }
        else if (hit4 < hit5)
        {
            
            for (int i = 0; i < hit5 - hit4; i++)
            {

                List<int> pickSpaces = highlightReformation();
                highlightSelected = -1;
                onHighlight(pickSpaces);

                onHighlightSelected += changeReligion;
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }

                UnityEngine.Debug.Log("end");
                //onRemoveHighlight(converted);
            }
        }
        
    }
}
