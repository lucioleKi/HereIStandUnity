using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM2;

public class HandsScript : MonoBehaviour
{
    List<int> hands;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void onEnable()
    {

    } 

    void onDisable()
    {

    }

    void showHands()
    {
        hands.Clear();
        List<CardObject> temp;
        switch (player)
        {
            case 0:
                temp = instanceDeck.hand0;
                break;
                case 1:
                temp = instanceDeck.hand1;
                break;
            case 2:
                temp = instanceDeck.hand2;
                break;
            case 3:
                temp = instanceDeck.hand3;
                break;
            case 4:
                temp = instanceDeck.hand4;
                break;
            case 5:
                temp = instanceDeck.hand5;
                break;
        }
        for(int i = 0; i < temp.Count; i++)
        {
            hands.Add(temp.ElementAt(i).id);
            GameObject newObject = new GameObject("HIS_" + temp.ElementAt(i).id.ToString());
            newObject.AddComponent<Image>();

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
