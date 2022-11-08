using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TTextScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
        changeText();
        changeColor();
    }

    void OnEnable()
    {
        GM2.onChangePhase += changeText;
        GM2.onPlayerChange += changeColor;
    }

    void OnDisable()
    {
        GM2.onChangePhase -= changeText;
        GM2.onPlayerChange -= changeColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeText()
    {
        string temp = "Turn " + GM1.turn.ToString() + ": ";
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        
        switch (GM1.phase)
        {
            case 1:
                temp = temp + "Luther\'s 95 Theses ";
                break;
            case 2:
                temp = temp + "Card Draw ";
                break;
            case 3:
                temp = temp + "Diplomacy ";
                break;
            case 4:
                temp = temp + "Diet of Worms ";
                break;
            case 5:
                temp = temp + "Spring Deployment ";
                break;
            case 6:
                temp = temp + "Action ";
                break;
            case 7:
                temp = temp + "Winter ";
                break;
            case 8:
                temp = temp + "New World ";
                break;
            case 9:
                temp = temp + "Victory Determination ";
                break;
        }
        temp = temp + "Phase";
        mtext.text = temp;
    }

    void changeColor()
    {
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        
        switch (GM1.player)
        {
            case 0:
                mtext.color = new Color(43f/255f, 165f/255f, 66f/255f, 1f);
                break;
            case 1:
                mtext.color = new Color(1f, 223f/255f, 63f/255f, 1f);
                break;
            case 2:
                mtext.color = new Color(240f/255f, 55f/255f, 63f/255f, 1f);
                break;
            case 3:
                mtext.color = new Color(10f/255f, 142f/255f, 216f/255f, 1f);
                break;
            case 4:
                mtext.color = new Color(109f/255f, 73f/255f, 169f/255f, 1f);
                break;
            case 5:
                mtext.color = new Color(162f/255f, 88f/255f, 61f/255f, 1f);
                break;
            
        }
    }
}
