using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static DeckScript;
using static GM1;
using UnityEngine.SceneManagement;
using TMPro;

public class MyCardsScript : MonoBehaviour
{
    public Button btn;
    public int playerIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        playerIndex = GM1.player;
        btn.onClick.AddListener(() => toCanvasCards());
    }

    void OnEnable()
    {
        //GM2.onAddReformer += nextPhase;
    }

    void OnDisable()
    {
        //GM2.onAddReformer -= nextPhase;
    }

    void toCanvasCards()
    {
        playerIndex = GM1.player;
        ContainerScript containerScript = GameObject.Find("CardContainer").GetComponent("ContainerScript") as ContainerScript;
        containerScript.showHands();
        GM2.onPlayerChange();
        GameObject.Find("Confirm").GetComponent<Button>().interactable = false;
        GameObject.Find("CPButton").GetComponent<Button>().interactable = false;
        //SceneManager.LoadScene("ScenePlayer");
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().interactable = true;
        //HIS004 chateaux discard
        if (GM2.chosenCard == "HIS-004" && GM1.player == 3&&GM2.boolStates[0])
        {
            OtherButtonScript otherButtonObject = GameObject.Find("OtherButton").GetComponent("OtherButtonScript") as OtherButtonScript;
            GameObject.Find("OtherButtonText").GetComponent<TextMeshProUGUI>().text = "Discard";
            otherButtonObject.btn.interactable = false;
        }
        else
        {
            OtherButtonScript otherButtonObject = GameObject.Find("OtherButton").GetComponent("OtherButtonScript") as OtherButtonScript;
            GameObject.Find("OtherButtonText").GetComponent<TextMeshProUGUI>().text = "Other";
            otherButtonObject.btn.interactable = false;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
