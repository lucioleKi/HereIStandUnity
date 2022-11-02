using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentScript : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventdata)
    {
        UnityEngine.Debug.Log("click");
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GM1.player = (GM1.player - 1) % 6;
            
            if (GM1.player == -1)
            {
                GM1.player = 5;
            }
            GM2.onPlayerChange();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GM1.player = (GM1.player + 1) % 6;
            GM2.onPlayerChange();
        }
    }
}
