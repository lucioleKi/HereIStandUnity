using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;

public class GM2 : MonoBehaviour
{
    public static GM2 instance;
    public static GM2 Instance
    {
        get
        {
            if (instance == null)
            {
                UnityEngine.Debug.Log("GM2 not initiated.");
            }
            return instance;
        }
    }

    public delegate void SimpleHandler();
    public static SimpleHandler on8;
    public static SimpleHandler onVP;
    public static SimpleHandler onPhase2;
    public static SimpleHandler onHighlightSelected;
    public delegate void Int2Handler(int index1, int index2);
    public static Int2Handler onMoveHome25;
    //(card index = id - 1, power)
    public static Int2Handler onChangeReg;
    public delegate void Int3Handler(int index1, int index2, int index3);
    public static Int3Handler onAddSpace;
    
    public delegate void Int1Handler(int index);
    public static Int1Handler onRemoveSpace;
    public static Int1Handler onAddReformer;
    public delegate void CardHandler(int index);
    public static CardHandler onMandatory;

    public delegate void List1Handler(List<int> index);
    public static List1Handler onHighlight;
    public static List1Handler onRemoveHighlight;

    public static int highlightSelected = -1;
    public static bool phaseEnd = false;

    //
    void OnEnable()
    {
        onMandatory += mandatory;
        onPhase2 += phase2;
        
    }

    void OnDisable()
    {
        onMandatory -= mandatory;
        onPhase2 -= phase2;
    }

    /*if (onMoveHome25 != null)
        {
            UnityEngine.Debug.Log("Here");
            onMoveHome25?.Invoke(1, 2);
}
    */

    //todo: make this generic
    IEnumerator waitHighlight()
    {
        
        for (int i = 0; i < 5; i++)
        {
            UnityEngine.Debug.Log("start");
            List<int> pickSpaces = highlightReformation();
            highlightSelected = -1;
            onHighlight(pickSpaces);
            onHighlightSelected += reformAttempt;
            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }

            UnityEngine.Debug.Log("end");
            //onRemoveHighlight(converted);
        }
    }
    

    void mandatory(int index)
    {
        switch (index)
        {
            case 8:
                
                instanceDeck.activeReformers.Add(instanceDeck.reformers.ElementAt(0));
                GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/Reformer4/Luther"), new Vector3(instanceDeck.spaces.ElementAt(0).posX + 965, instanceDeck.spaces.ElementAt(0).posY + 545, 0), Quaternion.identity);
                tempObject.transform.SetParent(GameObject.Find("Reformers").transform);
                tempObject.name = "Luther";
                tempObject.SetActive(true);
                religiousInfluence[0] = (Religion)1;
                onMoveHome25(0, 1);
                regulars[134] = 0;
                onChangeReg(134, 5);
                regulars[0] = 2;
                onChangeReg(0, 5);
                StartCoroutine(waitHighlight());
                

                
                break;
        }
    }

    void reformAttempt()
    {
        onHighlightSelected -= reformAttempt;
        //1. pick target space


        UnityEngine.Debug.Log(highlightSelected);
        int target = highlightSelected;

        SpaceObject targetSpace = instanceDeck.spaces.ElementAt(target);
        //2. add up reformer dice
        int reformerDice = 1;
        int papalDice = 1;
        //+2 if reformer in target space, +1 if reformer adjacent
        for (int i = 0; i < instanceDeck.activeReformers.Count(); i++)
        {
            if (instanceDeck.activeReformers.ElementAt(i).space == target)
            {
                reformerDice = reformerDice + 2;
            }
            else
            {
                
                SpaceObject tempSpace = instanceDeck.spaces.ElementAt(instanceDeck.activeReformers.ElementAt(i).space);
                for(int j= 0; j < tempSpace.adjacent.Count(); j++)
                {
                    if (tempSpace.adjacent.ElementAt(j) == targetSpace.id)
                    {
                        reformerDice++;
                    }
                }
            }
        }
        
        //+1 for every adjacent under protestant/catholic religious influence 
        for(int i = 0; i < instanceDeck.spaces.ElementAt(target).adjacent.Count; i++)
        {
            if (religiousInfluence[instanceDeck.spaces.ElementAt(target).adjacent.ElementAt(i)] == (Religion)1) {
                reformerDice++;
            }else if(religiousInfluence[instanceDeck.spaces.ElementAt(target).adjacent.ElementAt(i)] == (Religion)0)
            {
                papalDice++;
            }
        }

        //+2 if protestant land units, +1 if land units adjacent
        if (regulars[target] > 0&&spacesGM.ElementAt(target).controlPower==5)
        {
            reformerDice = reformerDice + 2; 
        }else if(regulars[target] > 0 && spacesGM.ElementAt(target).controlPower == 4)
        {
            papalDice = papalDice + 2;
        }
        for (int i = 0; i < targetSpace.adjacent.Count(); i++)
        {
            if (regulars[targetSpace.adjacent.ElementAt(i)] > 0 && regularsPower[targetSpace.adjacent.ElementAt(i)]==5)
            {
                reformerDice++;
            }else if (regulars[targetSpace.adjacent.ElementAt(i)] > 0 && regularsPower[targetSpace.adjacent.ElementAt(i)] == 4)
            {
                papalDice++;
            }
        }


        //3. add bonus dice
        //todo: printing press event and debater bonus
        if (turn == 1 && phase == 1)
        {
            reformerDice++;
        }
        UnityEngine.Debug.Log(reformerDice);
        UnityEngine.Debug.Log(papalDice);
        //4. roll dice
        int dice1 = 0;
        for(int i= 0; i < reformerDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 6);
            
            if (dice1 < randomIndex)
            {
                dice1 = randomIndex;
            }
            if (dice1 == 6)
            {
                UnityEngine.Debug.Log("6!");
                religiousInfluence[target] = (Religion)1;
                //send signal to various parties
                return;
            }
        }


        //5. add up papal dice



        //6. roll papal dice
        int dice2 = 0;
        for (int i = 0; i < papalDice; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 6);

            if (dice2 < randomIndex)
            {
                dice2 = randomIndex;
            }
        }

        //7. determine result
        if (dice1 > dice2)
        {
            UnityEngine.Debug.Log("win");
            religiousInfluence[target] = (Religion)1;
            onMoveHome25(0, 1);
            if((int)targetSpace.spaceType == 4)
            {
                regulars[134+target] = 0;
                onChangeReg(134+target, 5);
                regulars[target] = 1;
                onChangeReg(target, 5);
                
                
            }
            //send signal to various parties
            return;
        }
        else
        {
            UnityEngine.Debug.Log("lose");
            return;
        }
    }

    List<int> highlightReformation()
    {
        //todo: make port
        List<int> highlightReformations = new List<int>();
        for(int i = 0; i < instanceDeck.spaces.Count(); i++)
        {
            if ((int)religiousInfluence[i] == 1)
            {
                continue;
            }
            for(int j = 0; j<instanceDeck.spaces.ElementAt(i).adjacent.Count(); j++)
            {
                
                if (religiousInfluence[instanceDeck.spaces.ElementAt(i).adjacent.ElementAt(j)-1] == (Religion)1)
                {
                    
                    highlightReformations.Add(i);
                    break;
                }
            }
            if (!highlightReformations.Contains(i))
            {
                for (int j = 0; i < instanceDeck.spaces.ElementAt(i).pass.Count(); j++)
                {
                    if (religiousInfluence[instanceDeck.spaces.ElementAt(i).pass.ElementAt(j)] == (Religion)1)
                    {
                        highlightReformations.Add(i);
                        break;
                    }
                }
            }
            if (!highlightReformations.Contains(i))
            {
                for (int j = 0; j < instanceDeck.activeReformers.Count(); j++)
                {
                    if (instanceDeck.activeReformers.ElementAt(j).space == i)
                    {
                        highlightReformations.Add(i);
                        break;
                    }
                }
            }
                
            
        }
        return highlightReformations;
    }

    void phase2()
    {

    }

    void Awake()
    {
        instance = this;


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