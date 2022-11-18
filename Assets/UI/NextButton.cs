using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;

public class NextButton : MonoBehaviour
{
    public Button btn;
    public int cardIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        cardIndex = 8;
        btn.interactable = true;
        btn.onClick.AddListener(()=>buttonCallBack());
        
    }

    void OnEnable()
    {
        //GM2.onAddReformer += buttonCallBack;
    }

    void OnDisable()
    {
        //GM2.onAddReformer -= buttonCallBack;
    }

    void buttonCallBack()
    {
        //UnityEngine.Debug.Log("You have clicked the button!");
        if (phase == 1)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase2();
        }
        else if (phase == 2)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase3();
        }
        else if (phase == 3 && turn == 1)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase4();
        }
        else if (phase == 3) {
            phase = phase + 2;
            GM2.onChangePhase();
            GM2.onPhase5();
        }
        else if (phase == 4)
        {
            CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
            currentTextObject.reset();
            phase++;
            GM2.onChangePhase();
            //GM2.onPhase6();
        }
        else if (phase == 5)
        {
            phase++;
            GM2.onChangePhase();
            //GM2.onPhase6();
        }
        /*else if (phase == 6)
        {
            phase++;
            GM2.onChangePhase();
        }else if (phase == 7)
        {
            phase++;
            GM2.onChangePhase();
        }
        else if (phase == 8)
        {
            phase++;
            GM2.onChangePhase();
        }
        else if (phase == 9)
        {
            turn++;
            phase=2;
            GM2.onChangePhase();
            GM2.onChangePhase();
            GM2.onPhase2();
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
