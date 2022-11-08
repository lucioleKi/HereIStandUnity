using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static DeckScript;
using static GM1;
using UnityEngine.SceneManagement;

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
        //
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
