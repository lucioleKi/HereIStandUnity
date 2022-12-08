using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static GM2;
using static GraphUtils;

public class HighlightCPScript : MonoBehaviour, IPointerClickHandler
{
    int actionIndex;
    int cost;
    // Start is called before the first frame update
    void Start()
    {
        actionIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    public void OnPointerClick(PointerEventData eventData)
    {
        GM2.resetPower();
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        if (eventData.pointerCurrentRaycast.gameObject.name[9] == 'R')
        {
            actionIndex = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(14));
            UnityEngine.Debug.Log("actionIndex " + actionIndex.ToString());
        }
        else
        {
            return;
        }
        removeHighlight();
        switch (actionIndex)
        {
            case 0:
                //move formation in clear
                cost = 1;
                StartCoroutine(moveInClear1());
                break;
            case 1:
                //move formation over pass
                cost = 2;
                StartCoroutine(moveOverPass());
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
                    StartCoroutine(buySquadron());
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
                    StartCoroutine(translate());
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
                else if (GM1.player == 5)
                {
                    //publish treatise
                    cost = 2;
                    StartCoroutine(treatise5());
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
                    StartCoroutine(buyCorsair());
                }
                else if (GM1.player == 4)
                {
                    //build st. peters
                    cost = 1;
                    GM1.StPeters[0]++;
                    if (GM1.StPeters[0] == 5)
                    {
                        GM1.StPeters[1]++;
                        GM1.StPeters[0] = 0;
                    }
                    GM1.updateVP();
                    GM2.onVP();
                    GM2.onCPChange(textScript.displayCP - cost);
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
                    cost = 2;
                    StartCoroutine(buySquadron());
                }
                else if (GM1.player == 4)
                {
                    //burn books
                    cost = 2;
                    StartCoroutine(burnBook());
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
                    //TODO: after turn 5
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
                    //TODO: after Henry VIII marries Anne Boleyn
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

    public IEnumerator moveInClear1()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findClearFormation(GM1.player);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();

        //declare formation and starting space for moving

        //declare destination

       


        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        GM2.highlightSelected = -1;
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator moveInClear2()
    {
        yield break;
    }

    public IEnumerator moveOverPass()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findPassFormation(GM1.player);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();

        //declare formation and starting space for moving

        //declare destination




        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        GM2.highlightSelected = -1;
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator theologicalDebate()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        while (GM2.boolStates[2])
        {
            yield return null;
        }
        GM2.onCPChange(textScript.displayCP - cost);
        


    }

    public IEnumerator buyMerc()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findTrace(GM1.player);
        improveTrace(trace);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).merc++;
        GM2.onChangeMerc(GM2.highlightSelected, GM1.player);
        GM2.highlightSelected = -1;
        

        GM2.onCPChange(textScript.displayCP - cost);
        //UnityEngine.Debug.Log(new System.Diagnostics.StackTrace().ToString());
    }

    public IEnumerator buyRegular()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findTrace(GM1.player);
        improveTrace(trace);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).regular++;
        DeckScript.regulars[GM2.highlightSelected]++;
        GM2.onChangeReg(GM2.highlightSelected, GM1.player);
        GM2.highlightSelected = -1;
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator buyCav()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findTrace(GM1.player);
        improveTrace(trace);
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).cavalry++;
        GM2.onChangeCav(GM2.highlightSelected, GM1.player);
        GM2.highlightSelected = -1;
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator buySquadron()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findPorts(GM1.player);
       
        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).squadron++;
        GM2.onChangeSquadron(GM2.highlightSelected, GM1.player);
        GM2.highlightSelected = -1;
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator buyCorsair()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findPorts(0);

        GM2.highlightSelected = -1;
        GM2.onNoLayer();



        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).corsair++;
        GM2.onChangeCorsair(GM2.highlightSelected);
        GM2.highlightSelected = -1;
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator controlUnfortified()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> trace = findUnfortified(GM1.player);
        if(trace.Count()== 0)
        {
            GM2.onCPChange(textScript.displayCP);
            yield break;
        }
        GM2.highlightSelected = -1;
        GM2.onNoLayer();


        GM2.onHighlight(trace);
        UnityEngine.Debug.Log(trace.Count());
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }

        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).controlPower = GM1.player;
        DeckScript.spacesGM.ElementAt(GM2.highlightSelected).controlMarker = 1;
        GM2.onAddSpace(GM2.highlightSelected, GM1.player, 1);
        GM2.highlightSelected = -1;
        
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator burnBook()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        if (GM1.protestantSpaces == 0)
        {
            GM2.onCPChange(textScript.displayCP);
            yield break;
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        HandMarkerScript handMarkerObject = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        for(int i= 0; i < 2; i++)
        {
            List<int> pickSpaces = highlightCReformation();
            GM2.highlightSelected = -1;
            currentTextObject.post("Flip a space to Catholic influence.");
            onNoLayer();
            onHighlight(pickSpaces);

            onHighlightSelected += changeReligion;
            while (GM2.highlightSelected == -1)
            {
                yield return null;
            }

            UnityEngine.Debug.Log("end");
        }
        GM2.highlightSelected = -1;
        
        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator treatise5()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        if (GM1.protestantSpaces == 0)
        {
            GM2.onCPChange(textScript.displayCP);
            yield break;
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        HandMarkerScript handMarkerObject = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        for (int i = 0; i < 2; i++)
        {
            List<int> pickSpaces = highlightReformation();
            GM2.highlightSelected = -1;
            currentTextObject.post("Flip a space to Protestant influence.");
            onNoLayer();
            onHighlight(pickSpaces);

            onHighlightSelected += changeReligion;
            while (GM2.highlightSelected == -1)
            {
                yield return null;
            }

            UnityEngine.Debug.Log("end");
        }
        GM2.highlightSelected = -1;
        

        GM2.onCPChange(textScript.displayCP - cost);
    }

    public IEnumerator translate()
    {
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        List<int> pickSpaces = new List<int>();
        if (GM1.translations[3] <10)
        {
            pickSpaces.Add(28);
        }
        if (GM1.translations[4] < 10)
        {
            pickSpaces.Add(42);
        }
        if (GM1.translations[5] < 10)
        {
            pickSpaces.Add(1);
        }
        if(pickSpaces.Count() == 0)
        {
            GM2.onCPChange(textScript.displayCP);
            yield break;
        }
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        HandMarkerScript handMarkerObject = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        
            
            GM2.highlightSelected = -1;
            currentTextObject.post("Select a language zone.");
            onNoLayer();
            onHighlight(pickSpaces);
            while (GM2.highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }
        switch (GM2.highlightSelected)
        {
            case 28:
                
                if (GM1.translations[0] == 6)
                {
                    GM1.translations[3]++;
                }
                else
                {
                    GM1.translations[0]++;
                }
                break;
                case 42:
                if (GM1.translations[1] == 6)
                {
                    GM1.translations[4]++;
                }
                else
                {
                    GM1.translations[1]++;
                }
                break;
            case 1:
                if (GM1.translations[2] == 6)
                {
                    GM1.translations[5]++;
                }
                else
                {
                    GM1.translations[2]++;
                }
                break;
                
        }
        GM2.highlightSelected = -1;
        currentTextObject.reset();
        GM1.updateVP();
        GM2.onVP();
        GM2.onCPChange(textScript.displayCP - cost);
    }

    void removeHighlight() { 
  
        
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }
}
