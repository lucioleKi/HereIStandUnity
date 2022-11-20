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
    public int playerIndex;
    public string cardSelected;
    public string cardTag;
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
        GM2.waitCard = false;
        hand3.RemoveAt(int.Parse(cardTag.Substring(1)));

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
