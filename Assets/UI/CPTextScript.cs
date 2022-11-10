using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class CPTextScript : MonoBehaviour
{
    public int displayCP;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        mtext.text = "";
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
        if (GM1.phase == 6)
        {
            
            string temp = "CP : " + value.ToString();
            TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
            mtext.text = temp;
        }

    }
}
