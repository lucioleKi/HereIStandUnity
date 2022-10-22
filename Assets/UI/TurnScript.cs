using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScript : MonoBehaviour
{
    int displayTurn;

    // Start is called before the first frame update
    void Start()
    {
        
        displayTurn = GM1.turn;
       
        turnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTurn != GM1.turn)
        {
            displayTurn = GM1.turn;
            turnPosition();
        }
        
    }


    void turnPosition()
    {
        RectTransform pos = gameObject.GetComponent<RectTransform>();
        
        pos.anchoredPosition = new Vector2(195 + (displayTurn-1) * 40.375f, -485);
        
    }
}
