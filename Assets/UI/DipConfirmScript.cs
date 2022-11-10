using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static GM1;
using static GM2;
using TMPro;

public class DipConfirmScript : MonoBehaviour
{
    public static Button btn;
    public int playerIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => buttonCallBack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonCallBack()
    {
        playerIndex = GM1.player;
        if (gameObject.name == "Confirm_dip")
        {
            GM2.onConfirmDipForm(playerIndex);
        }else if (gameObject.name == "Confirm_peace")
        {

        }
        
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().interactable = false;
        
    }
}
