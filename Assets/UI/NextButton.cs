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
        }else if (phase == 2)
        {
            phase++;
            GM2.onChangePhase();
            GM2.onPhase3();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
