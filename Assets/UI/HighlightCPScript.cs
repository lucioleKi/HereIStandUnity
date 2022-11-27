using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HighlightCPScript : MonoBehaviour, IPointerClickHandler
{
    int actionIndex;
    int cost;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        actionIndex = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(10));
        GM2.boolStates[3] = true;
        GM2.onRemoveHighlight();
        switch (actionIndex)
        {
            case 0:
                //move formation in clear
                cost = 1;
                break;
            case 1:
                //move formation over pass
                cost = 2;
                break;
            case 2:
                if (GM1.player == 5)
                {
                    //buy merc
                    cost = 1;
                    StartCoroutine(buyMerc());
                }
                else
                {
                    //naval move
                    cost = 1;
                }
                break;
            case 3:
                if (GM1.player == 0)
                {
                    //control unfortified space
                    cost = 1;
                    StartCoroutine(controlUnfortified());
                }
                else if (GM1.player == 5)
                {
                    //raise regular troop
                    cost = 2;
                    StartCoroutine(buyRegular());
                }
                else
                {
                    //buy merc
                    cost = 1;
                    StartCoroutine(buyMerc());
                }
                break;
            case 4:
                if (GM1.player == 0)
                {
                    //initiate piracy at sea
                    cost = 2;
                }
                else if (GM1.player == 5)
                {
                    //assault
                    cost = 1;
                }
                else
                {
                    //raise regular troop
                    cost = 2;
                    StartCoroutine(buyRegular());
                }
                break;
            case 5:
                if (GM1.player == 0)
                {
                    //assault/fight foreign war
                    cost = 1;
                }
                else if (GM1.player == 5)
                {
                    //control unfortified space
                    cost = 1;
                    StartCoroutine(controlUnfortified());
                }
                else
                {
                    //build naval squadron
                    cost = 2;
                }
                break;
            case 6:
                if (GM1.player == 0)
                {
                    //raise cavalry
                    cost = 1;
                    StartCoroutine(buyCav());
                }
                else if (GM1.player == 5)
                {
                    //translate scripture
                    cost = 1;
                }
                else
                {
                    //assault/fight foreign war
                    cost = 1;
                }
                break;
            case 7:
                if (GM1.player == 0)
                {
                    //raise regular troop
                    cost = 2;
                    StartCoroutine(buyRegular());
                }
                else if (GM1.player == 4)
                {
                    //control unfortified space
                    cost = 1;
                    StartCoroutine(controlUnfortified());
                }
                else if (GM1.player == 5)
                {
                    //publish treatise
                    cost = 2;
                }
                else
                {
                    //control unfortified space
                    cost = 1;
                    StartCoroutine(controlUnfortified());
                }
                break;
            case 8:
                if (GM1.player == 0)
                {
                    //build corsair
                    cost = 1;
                }
                else if (GM1.player == 4)
                {
                    //build st. peters
                    cost = 1;
                }
                else if (GM1.player == 5)
                {
                    //call theological debate
                    cost = 3;
                    actionIndex = -1;
                    GM2.theologicalDebate();
                    StartCoroutine(theologicalDebate());
                    
                }
                else
                {
                    //explore
                    cost = 2;
                }
                break;
            case 9:
                if (GM1.player == 0)
                {
                    //build naval squadron
                }
                else if (GM1.player == 4)
                {
                    //burn books
                    cost = 2;
                }
                else if (GM1.player == 1)
                {
                    //colonize
                    cost = 2;
                }
                else
                {
                    //colonize
                    cost = 3;
                }
                break;
            case 10:
                if (GM1.player == 4)
                {
                    //found jesuit university
                    cost = 3;
                }
                else
                {
                    //conquer
                    cost = 4;
                }

                break;
            case 11:
                if (GM1.player == 2)
                {
                    //publish treatise(english zone)
                    cost = 3;
                }
                else if (GM1.player == 4)
                {
                    //call theological debate
                    cost = 3;
                    actionIndex = -1;
                    GM2.theologicalDebate();
                    StartCoroutine(theologicalDebate());
                }
                break;

        }
        actionIndex = -1;
        
    }

    public IEnumerator theologicalDebate()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        while (GM2.boolStates[2])
        {
            yield return null;
        }
        //UnityEngine.Debug.Log(textScript.displayCP - cost);
        if (!GM2.boolStates[3])
        {
            yield break;
        }
        
        GM2.onCPChange(textScript.displayCP - cost);
        


    }

    public IEnumerator buyMerc()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = GM2.findTrace(GM1.player);
        improveTrace(trace);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        //onHighlightSelected += springDeploy;
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).merc++;
        GM2.onChangeMerc(GM2.highlightSelected, GM1.player);
        GM2.highlightSelected = -1;
        if (!GM2.boolStates[3])
        {
            yield break;
        }

        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator buyRegular()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = GM2.findTrace(GM1.player);
        improveTrace(trace);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        //onHighlightSelected += springDeploy;
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).regular++;
        DeckScript.regulars[GM2.highlightSelected]++;
        GM2.onChangeReg(GM2.highlightSelected, GM1.player);
        GM2.highlightSelected = -1;
        if (!GM2.boolStates[3])
        {
            yield break;
        }

        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator buyCav()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = GM2.findTrace(GM1.player);
        improveTrace(trace);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        //onHighlightSelected += springDeploy;
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).cavalry++;
        GM2.onChangeCav(GM2.highlightSelected, GM1.player);
        GM2.highlightSelected = -1;
        if (!GM2.boolStates[3])
        {
            yield break;
        }

        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator controlUnfortified()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = GM2.findUnfortified(GM1.player);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        //onHighlightSelected += springDeploy;
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).controlPower = GM1.player;
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).controlMarker = 1;
        GM2.onAddSpace(GM2.highlightSelected, GM1.player, 1);
        GM2.highlightSelected = -1;
        if (!GM2.boolStates[3])
        {
            yield break;
        }

        GM2.onCPChange(textScript.displayCP - cost);
    }

    public List<int> improveTrace(List<int> trace)
    {
        switch (GM1.player)
        {
            case 0:
                trace.Add(97);
                break;
            case 1:
                trace.Add(83);
                trace.Add(21);
                break;
            case 2:
                trace.Add(27);
                break;
            case 3:
                trace.Add(41);
                break;
            case 4:
                trace.Add(65);
                break;
            
        }
        return trace;
    }
}
