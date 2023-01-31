using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
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
            case 3:
                //HIS-087
                List<CardObject> hand = new List<CardObject>();
                switch (GM1.player)
                {
                    case 0:
                        hand = hand0;
                        break;

                    case 1:
                        hand = hand1;
                        break;
                    case 2:
                        hand = hand2;
                        break;
                    case 3:
                        hand = hand3;
                        break;
                    case 4:
                        hand = hand4;
                        break;
                    case 5:
                        hand = hand5;
                        break;
                }
                int CP = hand.ElementAt(int.Parse(cardTag.Substring(1))).CP;
                int[] keep = new int[6] { 2, 4, 6, 10, 50, 50 };
                int count = 0;
                for (int i = 0; i<134; i++)
                {
                    if (spacesGM.ElementAt(i).controlPower == GM1.player && spacesGM.ElementAt(i).merc > 0)
                    {
                        count += spacesGM.ElementAt(i).merc;
                        if (keep[CP] < count)
                        {
                            spacesGM.ElementAt(i).merc = 0;
                            if (DeckScript.spacesGM.ElementAt(i).regular == 0 && DeckScript.spacesGM.ElementAt(i).cavalry == 0)
                            {
                                DeckScript.spacesGM.ElementAt(i).regularPower = -1;
                            }
                            GM2.onChangeMerc(i, GM1.player);
                        }
                    }
                }
                discardCards.Add(hand.ElementAt(int.Parse(cardTag.Substring(1))));
                hand.RemoveAt(int.Parse(cardTag.Substring(1)));
                GM2.boolStates[50] = false;
                GM2.onDeactivateSkip();
                break;
            case 4:
                //HIS-074
                CardObject temp = new CardObject();
                switch (GM1.player)
                {
                    case 0:
                        temp = hand0.ElementAt(int.Parse(cardTag.Substring(1)));
                        hand0.RemoveAt(int.Parse(cardTag.Substring(1)));
                        break;
                    case 1:
                        temp = hand1.ElementAt(int.Parse(cardTag.Substring(1)));
                        hand1.RemoveAt(int.Parse(cardTag.Substring(1)));
                        break;
                    case 2:
                        temp = hand2.ElementAt(int.Parse(cardTag.Substring(1)));
                        hand2.RemoveAt(int.Parse(cardTag.Substring(1)));
                        break;
                    case 3:
                        temp = hand3.ElementAt(int.Parse(cardTag.Substring(1)));
                        hand3.RemoveAt(int.Parse(cardTag.Substring(1)));
                        break;
                    case 4:
                        temp = hand4.ElementAt(int.Parse(cardTag.Substring(1)));
                        hand4.RemoveAt(int.Parse(cardTag.Substring(1)));
                        break;
                    case 5:
                        temp = hand5.ElementAt(int.Parse(cardTag.Substring(1)));
                        hand5.RemoveAt(int.Parse(cardTag.Substring(1)));
                        break;
                }
                UnityEngine.Debug.Log(GM2.highlightSelected);
                switch (GM2.highlightSelected)
                {
                    case 0:
                        hand0.Add(temp);
                        break;
                    case 1:
                        hand1.Add(temp);
                        break;
                    case 2:
                        hand2.Add(temp);
                        break;
                    case 3:
                        hand3.Add(temp);
                        break;
                    case 4:
                        hand4.Add(temp);
                        break;
                    case 5:
                        hand5.Add(temp);
                        break;
                }
                GM2.boolStates[51] = false;
                break;
        }
        if (btnStatus != -1)
        {
            btnStatus = -1;
            btn.interactable = false;
            GameObject.Find("OtherButtonText").GetComponent<TextMeshProUGUI>().text = "Other";
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

    void deactivateOther()
    {
        UnityEngine.Debug.Log("other deactivated");
        //gameObject.GetComponent<CanvasGroup>().alpha = 0;
        //gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //gameObject.GetComponent<CanvasGroup>().interactable = false;
        btnStatus = -1;
        btn.interactable = false;
    }
}
