using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandMvmt : MonoBehaviour
{
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack());
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonCallBack()
    {

    }

    public void post()
    {
        GM2.boolStates[28] = true;
        btn.interactable = true;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().interactable = true;

    }

    void reset()
    {
        GM2.boolStates[28] = false;
        btn.interactable = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
    }
}
