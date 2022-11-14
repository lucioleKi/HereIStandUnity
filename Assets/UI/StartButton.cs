using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;

public class StartButton : MonoBehaviour
{
    public Button btn;
    public int cardIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        cardIndex = 8;
        btn.interactable = false;
        btn.onClick.AddListener(()=> buttonCallBack(cardIndex));
    }

    void OnEnable()
    {
        GM2.onAddReformer += buttonCallBack;
        GM2.onChosenCard += makeInteractable;
    }

    void OnDisable()
    {
        GM2.onAddReformer -= buttonCallBack;
        GM2.onChosenCard -= makeInteractable;
    }

    void buttonCallBack(int index)
    {
        //UnityEngine.Debug.Log("You have clicked the button!");
        string tempName = GameObject.Find("CardDisplay").GetComponent<Image>().sprite.name;
        cardIndex = int.Parse(tempName.Substring(4));
            GM2.onMandatory(28);
        btn.interactable = false;
        

    }

    void makeInteractable()
    {
    if (GM2.chosenCard == "")
    {
        btn.interactable = false;
    }
    else
    {
        btn.interactable = true;
    }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
