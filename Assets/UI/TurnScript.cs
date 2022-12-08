using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScript : MonoBehaviour
{
    int displayTurn;

    // Start is called before the first frame update
    void Start()
    {
        
        displayTurn = GM1.phase;
       
        turnPosition();
    }

    void OnEnable()
    {

        //GM2.onChangePhase += turnPosition;

    }

    void OnDisable()
    {

        //GM2.onChangePhase -= turnPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    void turnPosition()
    {
        GM2.resetMap();
        displayTurn = GM1.phase;
        RectTransform pos = gameObject.GetComponent<RectTransform>();
        
        pos.localPosition = new Vector2(195 + (displayTurn-1) * 40.375f, -485);
        
    }
}
