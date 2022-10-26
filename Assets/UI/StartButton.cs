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
        btn.onClick.AddListener(()=>buttonCallBack(cardIndex));
    }

    void OnEnable()
    {
        GM2.onAddReformer += buttonCallBack;
    }

    void OnDisable()
    {
        GM2.onAddReformer -= buttonCallBack;
    }

    void buttonCallBack(int index)
    {
        //UnityEngine.Debug.Log("You have clicked the button!");
        GM2.onMandatory(index);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
