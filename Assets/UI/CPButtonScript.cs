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
        if (GM1.phase==4)
        {
            
            setSecretCP();
            
        }
        else
        {
            setActionCP();
            UnityEngine.Debug.Log(GM2.currentCP);
            onCPChange(GM2.currentCP);

            ConfirmScript.cardSelected = "";
        }
        
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = true;
        if (GM1.phase != 4)
        {
            GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
            GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
        }
        

        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().interactable = false;
        foreach (Transform child in GameObject.Find("CardContainer").transform)
        {
            GameObject.Destroy(child.gameObject);
        }


    }

    void setActionCP()
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
    }

    void setSecretCP()
    {
        switch (int.Parse(cardTag.Substring(0, 1)))
        {
            
            case 1:
                GM2.secretCP[1] = hand1.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand1.RemoveAt(int.Parse(cardTag.Substring(1)));
                if(GM1.toDo.Count > 0)
                {
                    GM1.deq1(0);
                }
                else
                {
                    GM1.deq1(1);
                }
                break;
            case 4:
                GM2.secretCP[4] = hand4.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand4.RemoveAt(int.Parse(cardTag.Substring(1)));
                if (GM1.toDo.Count > 0)
                {
                    GM1.deq1(0);
                }
                else
                {
                    GM1.deq1(1);

                }
                break;
            case 5:
                GM2.secretCP[5] = hand5.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                hand5.RemoveAt(int.Parse(cardTag.Substring(1)));
                GM1.deq1(2);
                break;
        }
    }
}
