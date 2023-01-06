using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static DeckScript;
using static GM2;

public class OtherButtonScript : MonoBehaviour
{
    public Button btn;
    public int btnStatus;
    public int playerIndex;
    public string cardSelected;
    public string cardTag;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallback());
        btnStatus = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        GM2.onOtherBtn += activateOther;
        GM2.onDeactivateOther += deactivateOther;
    }

    void OnDisable()
    {
        GM2.onOtherBtn -= activateOther;
        GM2.onDeactivateOther -= deactivateOther;
    }

    void buttonCallback()
    {
        switch (btnStatus)
        {
            case 1:
                //HIS-003
                GM2.boolStates[0] = false;
                discardCards.Add(hand3.ElementAt(int.Parse(cardTag.Substring(1))));
                hand3.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
            case 2:
                discardCards.Add(hand2.ElementAt(int.Parse(cardTag.Substring(1))));
                hand2.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
        }
        if (btnStatus != -1)
        {
            btnStatus = -1;
            btn.interactable = false;
        }

        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = true;

        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().interactable = false;
        foreach (Transform child in GameObject.Find("CardContainer").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void activateOther(int index)
    {
        UnityEngine.Debug.Log("other activated");
        btnStatus = index;
        //gameObject.GetComponent<CanvasGroup>().alpha = 1;
        //gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //gameObject.GetComponent<CanvasGroup>().interactable = true;
        //btn.interactable = true;
    }

    void deactivateOther() {
        UnityEngine.Debug.Log("other deactivated");
        //gameObject.GetComponent<CanvasGroup>().alpha = 0;
        //gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //gameObject.GetComponent<CanvasGroup>().interactable = false;
        btnStatus = -1;
        btn.interactable = false;
    }
}
