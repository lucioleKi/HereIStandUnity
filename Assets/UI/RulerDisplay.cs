using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        
        GM2.onPlayerChange += changeDisplay;
    }

    void OnDisable()
    {
        
        GM2.onPlayerChange -= changeDisplay;
    }


    void changeDisplay()
    {
        GM2.resetPower();
        if (GM1.player == int.Parse(gameObject.name.Substring(6))){
            
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
        }
    }
}
