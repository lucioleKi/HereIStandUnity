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
    public SimpleHandler on8;
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

    void OnEnable()
    {
        onMandatory += mandatory;
    }

    void OnDisable()
    {
        onMandatory -= mandatory;
    }

    /*if (onMoveHome25 != null)
        {
            UnityEngine.Debug.Log("Here");
            onMoveHome25?.Invoke(1, 2);
}
*/
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
                reformAttempt();
                break;
        }
    }

    void reformAttempt()
    {
        //1. pick target space
        int[] refo = new int[5] { 1, 2, 3, 4, 5 };
        int target = 1;
        SpaceObject targetSpace = instanceDeck.spaces.ElementAt(target);
        //2. add up reformer dice
        int reformerDice = 1;
        //+2 if reformer in target space, +1 if reformer adjacent
        for(int i = 0; i < instanceDeck.activeReformers.Count(); i++)
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
                    if (tempSpace.adjacent.ElementAt(j) == targetSpace.matching)
                    {
                        reformerDice++;
                    }
                }
            }
        }
        //UnityEngine.Debug.Log(reformerDice);
        //+1 for every adjacent under protestant religious influence

        //+2 if protestant land units, +1 if land units adjacent




        //3. add bonus dice

        //4. roll dice
        int randomIndex = UnityEngine.Random.Range(1, reformerDice);

        //5. add up papal dice
        int papalDice = 1;

        //6. roll papal dice
        int random1 = UnityEngine.Random.Range(1, reformerDice); 

        //7. determine result
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
