using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;
using static GM2;
using static GM1;

public class VPScript : MonoBehaviour
{
    int displayVP;
    int posX;
    int posY;
    // Start is called before the first frame update
    void Start()
    {
        changeVP();
    }

    void OnEnable()
    {
        onVP += changeVP;
    }

    void OnDisable()
    {
        onVP -= changeVP;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void changeVP()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        GM2.resetMap();
        int index = gameObject.name[2] - '0';
        displayVP = VPs[index];
        position();
        int[] win = new int[5] { 10, 13, 8, 10, 6 };
        if (GM1.phase == 6)
        {
            for(int i=0; i<5; i++)
            {
                if (GM1.cardTracks[i] >= win[i])
                {
                    GM1.player = i;
                    onPlayerChange();
                    currentTextObject.post("You won the game via a military victory!");
                    break;
                }
            }
        }
        
    }

    //offset VP markers according to current VP count
    void position()
    {
        
        RectTransform pos = gameObject.GetComponent<RectTransform>();
        if (displayVP < 10)
        {
            pos.anchoredPosition = new Vector2(-322 + displayVP * 34.56f, -467);
        }
        else
        {
            pos.anchoredPosition = new Vector2(-665 + (displayVP - 10) * 34.56f, -502);
        }
    }

    
}
