using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GM2;

public class Home25Script : MonoBehaviour
{
    int place2;
    int place5;
    // Start is called before the first frame update
    void Start()
    {
        HomePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        GM2.onMoveHome25 += HomePosition;
    }

    void OnDisable()
    {
        GM2.onMoveHome25 -= HomePosition;
    }
    

    void HomePosition()
    {
        GM2.resetReligious();
        place2 = GM1.englishSpaces;
        place5 = GM1.protestantSpaces;
        RectTransform pos = gameObject.GetComponent<RectTransform>();
        if (gameObject.name == "EnglishHome")
        {
            if (place2 < 13)
            {
                pos.anchoredPosition = new Vector2(-165 + place2 * 27.5f, -33);
            }
            else if (place2 < 26)
            {
                pos.anchoredPosition = new Vector2(-165 + (place2 - 13) * 27.5f, -62);
            }
            else if (place2 < 39)
            {
                pos.anchoredPosition = new Vector2(-165 + (place2 - 26) * 27.5f, -91);
            }
            else
            {
                pos.anchoredPosition = new Vector2(-165 + (place2 - 39) * 27.5f, -121);
            }
        }
        else
        {
            if (place5 < 13)
            {
                pos.anchoredPosition = new Vector2(-165 + place5 * 27.5f, -33);
            }
            else if (place5 < 26)
            {
                pos.anchoredPosition = new Vector2(-165 + (place5 - 13) * 27.5f, -62);
            }
            else if (place5 < 39)
            {
                pos.anchoredPosition = new Vector2(-165 + (place5 - 26) * 27.5f, -91);
            }
            else
            {
                pos.anchoredPosition = new Vector2(-165 + (place5 - 39) * 27.5f, -121);
            }
        }
        if (GM1.protestantSpaces >= 50)
        {
            CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
            currentTextObject.post("You won the game via a religious victory!");
            GM1.player = 5;
            onPlayerChange();
        }
        
    }


}
