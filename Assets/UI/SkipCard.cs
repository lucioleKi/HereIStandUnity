using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        LandMvmt landMvmt = gameObject.GetComponent("LandMvmt") as LandMvmt;
        SiegeScript siegeScript = gameObject.GetComponent("SiegeScript") as SiegeScript;
        if (landMvmt.status != -1 || siegeScript.status != -1)
        {
            return;
        }
        switch (btnStatus)
        {
            case 1:
                //skip ransom leader
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                startButton.btn.interactable = false;
                
                handMarkerScript.canRansom.RemoveAt(0);
                handMarkerScript.ransomedPower.RemoveAt(0);
                handMarkerScript.ransomedLeader.RemoveAt(0);
                GM2.onPhase3();
                break;
            case 2:
                //refuse to be ransomed
                GM1.player = 0;
                GM2.onPlayerChange();
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                startButton.btn.interactable = false;
                GameObject.Find("Skip (TMP)").GetComponent<TextMeshProUGUI>().text = "Skip";
                handMarkerScript.canRansom.RemoveAt(0);
                handMarkerScript.ransomedPower.RemoveAt(0);
                handMarkerScript.ransomedLeader.RemoveAt(0);
                GM2.onPhase3();
                break;
            case 3:
                //GameObject.Find("KeyLeft").GetComponent<Button>().interactable = true;
                //GameObject.Find("KeyRight").GetComponent<Button>().interactable = true;
                GM1.diplomacyState[2, 8] = 1;
                GM2.onChangeDip();
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                startButton.status = -1;
                startButton.btn.interactable = false;
                DeckScript.hand2.RemoveAt(0);
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = 2;
                GM2.onPlayerChange();
                GM2.currentCP = 5;
                GM2.onCPChange(GM2.currentCP);
                break;
            case 4:
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                startButton.status = -1;
                startButton.btn.interactable = false;
                handMarkerScript.excom.RemoveAt(0);
                GM1.player = 0;
                GM2.onPlayerChange();
                GM2.onPhase3();
                break;
            case 5:
                GM2.boolStates[33] = false;
                break;
            case 6:
                //action phase pass
                GM1.skipped[GM1.player] = true;
                GM1.nextImpulse();
                break;
            case 87:
                //lose all merc
                for(int i=0; i < 134; i++)
                {
                    if (DeckScript.spacesGM.ElementAt(i).controlPower == GM1.player && DeckScript.spacesGM.ElementAt(i).merc > 0)
                    {
                        DeckScript.spacesGM.ElementAt(i).merc = 0;
                        if(DeckScript.spacesGM.ElementAt(i).regular==0&& DeckScript.spacesGM.ElementAt(i).cavalry == 0)
                        {
                            DeckScript.spacesGM.ElementAt(i).regularPower = -1;
                        }
                        GM2.onChangeMerc(i, GM1.player);
                    }
                }
                GM2.onDeactivateOther();
                GM2.boolStates[50]= false;
                break;
            case 102:
                startButton.status = -1;
                startButton.btn.interactable = false;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                deactivateSkip();
                break;
            case 109:
                GameObject.Find("KeyLeft").GetComponent<Button>().interactable = false;
                GameObject.Find("KeyRight").GetComponent<Button>().interactable = false;
                //startButton.status = -1;
                //startButton.btn.interactable = false;
                //GM2.chosenCard = "";
                //GM2.onChosenCard();
                deactivateSkip();
                GM2.onPhase5();
                break;
        }
        if (btnStatus != -1&&btnStatus!=5)
        {
            btnStatus = -1;
            btn.interactable = false;
        }
    }

    void OnEnable()
    {
        GM2.onSkipCard += activateSkip;
        GM2.onDeactivateSkip += deactivateSkip;
    }

    void OnDisable()
    {
        GM2.onSkipCard -= activateSkip;
        GM2.onDeactivateSkip -= deactivateSkip;
    }

    void activateSkip(int index)
    {
        UnityEngine.Debug.Log("activated");
        btnStatus = index;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        btn.interactable = true;
    }

    void deactivateSkip()
    {
        UnityEngine.Debug.Log("deactivated");
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        btnStatus = -1;
        btn.interactable = false;
    }
}
