using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TTextScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
        changeText();
    }

    void OnEnable()
    {
        GM2.onChangePhase += changeText;
    }

    void OnDisable()
    {
        GM2.onChangePhase -= changeText;
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
}
