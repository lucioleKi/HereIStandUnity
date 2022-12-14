using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GM1;
using static GM2;
using System;

public class ConfirmScript : MonoBehaviour
{
    public static Button btn;
    public static string cardSelected;
    public int playerIndex;
    // Start is called before the first frame update
    void Start()
    {
        cardSelected = "";
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void buttonCallBack()
    {
        CPButtonScript.cardSelected = "";
        btn.interactable = false;
        //SceneManager.LoadScene("SceneMap");
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = true;
        if (transform.parent.name == "CanvasCards")
        {
            GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().interactable = false;
            foreach (Transform child in GameObject.Find("CardContainer").transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            GM2.chosenCard = cardSelected;
            GM2.onChosenCard();
        }
        else if(transform.parent.name == "CanvasDiplomacy")
        {
            
            GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().interactable = false;
        }
        
        
        
    }
}
