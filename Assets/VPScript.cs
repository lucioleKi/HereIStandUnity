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
        displayVP = GM1.powerObjects[index].initialVP;
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

    void position()
    {

    }
}
