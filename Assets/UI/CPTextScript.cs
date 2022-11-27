using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CPTextScript : MonoBehaviour
{
    public int displayCP;
    // Start is called before the first frame update
    void Start()
    {
        reset();
    }

    void OnEnable()
    {

        GM2.onCPChange += showCP;

        //chooseCard();
    }

    void OnDisable()
    {

        GM2.onCPChange -= showCP;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showCP(int value)
    {
        if (value>0)//GM1.phase == 6)
        {
            GM2.onRemoveHighlight();
            string temp = "CP : " + value.ToString();
            TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
            mtext.text = temp;
            UnityEngine.Debug.Log(value.ToString());
            GM2.onHighlightRectangles(value);
            displayCP = value;
            GM2.boolStates[3] = false;
        }
        else
        {
            reset();
            GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
            GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
        }

    }

    void reset()
    {
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        mtext.text = "";
    }
}
