using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipCard : MonoBehaviour
{
    public Button btn;
    public int btnStatus;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack());
        btnStatus = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void buttonCallBack()
    {
        if (btnStatus != -1)
        {
            btn.interactable = false;
        }
        switch(btnStatus)
        {
            case 109:
                GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
                GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
                GM2.onPhase5();
                break;
        }
    }

    void OnEnable()
    {
        GM2.onSkipCard += activateSkip;
    }

    void OnDisable()
    {
        GM2.onSkipCard -= activateSkip;
    }

    void activateSkip(int index)
    {
        btnStatus = index;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        btn.interactable = true;
    }
}
