using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static DeckScript;
using static GM2;

public class CPButtonScript : MonoBehaviour
{
    public static Button btn;
    public int playerIndex;
    public static string cardSelected;
    public static string cardTag;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => toCanvasBoard());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void toCanvasBoard()
    {
        
        
        
        switch (int.Parse(cardTag.Substring(0, 1)))
        {
            case 0:
                GM2.currentCP = hand0.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand0.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
            case 1:
                GM2.currentCP = hand1.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand1.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
            case 2:
                GM2.currentCP = hand2.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand2.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
            case 3:
                GM2.currentCP = hand3.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand3.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
            case 4:
                GM2.currentCP = hand4.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand4.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
            case 5:
                GM2.currentCP = hand5.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand5.RemoveAt(int.Parse(cardTag.Substring(1)));
                break;
        }
        UnityEngine.Debug.Log(GM2.currentCP);
        onCPChange(GM2.currentCP);

        ConfirmScript.cardSelected = "";

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
}