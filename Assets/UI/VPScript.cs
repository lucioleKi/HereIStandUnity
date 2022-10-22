using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;

public class VPScript : MonoBehaviour
{
    int displayVP;
    int posX;
    int posY;
    // Start is called before the first frame update
    void Start()
    {
        int index = gameObject.name[2]-'0';
        displayVP = GM1.VPs[index];
        position();
    }

    // Update is called once per frame
    void Update()
    {
        int index = gameObject.name[2] - '0';
        if (displayVP != GM1.VPs[index])
        {
            displayVP = GM1.VPs[index];

        }
        position();
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
            pos.anchoredPosition = new Vector2(-665 + (displayVP-10) * 34.56f, -502);
        }
    }

   
}
