using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static DeckScript;
using static GM1;
using UnityEngine.SceneManagement;


public class DipButtonScript : MonoBehaviour
{
    public Button btn;
    public int playerIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        playerIndex = GM1.player;
        btn.interactable = false;
        btn.onClick.AddListener(() => toCanvasDip());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        GM2.onPhase3 += active3;
        //GM2.onPhase4 += deactivate3;
    }

    void OnDisable()
    {
        GM2.onPhase3 -= active3;
        //GM2.onPhase4 -= deactivate3;
    }

    void active3()
    {
        btn.interactable = true;
    }

    void deactivate3()
    {
        btn.interactable = false;
    }

    void toCanvasDip()
    {
        playerIndex = GM1.player;
        //SceneManager.LoadScene("ScenePlayer");
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().interactable = true;
        //GM2.onPlayerChange();
    }
}
