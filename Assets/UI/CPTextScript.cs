using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CPTextScript : MonoBehaviour
{
    public int displayCP;
    // Start is called before the first frame update
    void Start()
    {
        
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
        string temp = "Current CP : " + value.ToString();
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        mtext.text = temp;
    }
}
