using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class KeyLRScript : MonoBehaviour
{
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        //btn.interactable = true;
        btn.onClick.AddListener(() => buttonCallBack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonCallBack()
    {
        if (gameObject.name == "KeyLeft")
        {
            GM1.player = (GM1.player - 1) % 6;

            if (GM1.player == -1)
            {
                GM1.player = 5;
            }
            GM2.onPlayerChange();
        }
        else if(gameObject.name =="KeyRight")
        {
            GM1.player = (GM1.player + 1) % 6;
            GM2.onPlayerChange();
        }

    }
}
