using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;

public class StartButton : MonoBehaviour
{
    public Button btn;
    public int cardIndex;
    public bool forIntercept;
    public bool forAvoid;
    public bool forAvoidNaval;
    public bool forWithdraw;
    public bool forIntervene;
    // Start is called before the first frame update
    void Start()
    {

        btn = gameObject.GetComponent<Button>();
        cardIndex = 8;
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack(cardIndex));
        forIntercept = false;

    }

    void OnEnable()
    {
        GM2.onAddReformer += buttonCallBack;
        GM2.onChosenCard += makeInteractable;
    }

    void OnDisable()
    {
        GM2.onAddReformer -= buttonCallBack;
        GM2.onChosenCard -= makeInteractable;
    }

    void buttonCallBack(int index)
    {
        //UnityEngine.Debug.Log("You have clicked the button!");
        if (forIntercept)
        {

            StartCoroutine(wait132());
        }
        else if (forAvoid)
        {
            StartCoroutine(wait133());
        }else if (forWithdraw)
        {
            withdraw();
        }else if (forAvoidNaval)
        {
            StartCoroutine(wait165());
        }
        else if (forIntervene) {
            //section 22
        } 
        else
        {
            string tempName = GameObject.Find("CardDisplay").GetComponent<Image>().sprite.name;
            cardIndex = int.Parse(tempName.Substring(4));
            GM2.onMandatory(cardIndex);
            
        }
        btn.interactable = false;

    }

    public void startOther(int actionIndex)
    {
        switch (actionIndex)
        {
            case 0:
                //reset
                if (btn.interactable)
                {
                    btn.interactable = false;
                }
                else
                {
                    btn.interactable = true;
                }
                break;
            case 1:
                btn.interactable = true;
                forIntercept = true;
                
                break;
            case 2:
                btn.interactable = true;
                forAvoid = true;
                
                break;
            case 3:
                btn.interactable = true;
                forWithdraw = true;
                break;
            case 4:
                btn.interactable = true;
                forAvoidNaval = true;

                break;
        }


    }

    void makeInteractable()
    {
        if (GM2.chosenCard == "" && (forIntercept||forAvoid||forWithdraw))
        {
            btn.interactable = true;
        }
        else if (GM2.chosenCard == "")
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator wait132()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Declare Formation");
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        inputNumberObject.post();
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        List<int> trace = landMvmt.tempTrace;
        GM2.highlightSelected = -1;
        GM2.leaderSelected = -1;
        GM2.onRegLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        bool hasLeader = false;
        if (GM2.leaderSelected == spacesGM.ElementAt(GM2.highlightSelected).leader1 || GM2.leaderSelected == spacesGM.ElementAt(GM2.highlightSelected).leader2)
        {
            hasLeader = true;

        }
        int command = 0;
        if (!string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
        {
            int permitted = 4;
            command = int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
            if (hasLeader)
            {
                permitted = leaders.ElementAt(GM2.leaderSelected - 1).command;
            }
            else
            {
                permitted = 4;
            }
            if (command > permitted)
            {
                command = permitted;
            }

            if (command > regulars[GM2.highlightSelected])
            {
                command = regulars[GM2.highlightSelected];
            }
        }
        int randomIndex = UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7);
        if (hasLeader)
        {
            randomIndex = randomIndex + leaders.ElementAt(GM2.leaderSelected).battle;
        }
        UnityEngine.Debug.Log("interception dice " + randomIndex.ToString());
        if (randomIndex >= 9 && command > 0)
        {
            spacesGM.ElementAt(landMvmt.destination).regular = spacesGM.ElementAt(landMvmt.destination).regular + command;
            spacesGM.ElementAt(GM2.highlightSelected).regular = spacesGM.ElementAt(GM2.highlightSelected).regular - command;
            regulars[landMvmt.destination] = regulars[landMvmt.destination] + command;
            regulars[GM2.highlightSelected] = regulars[GM2.highlightSelected] - command;
            GM2.onChangeReg(landMvmt.destination, GM1.player);
            GM2.onChangeReg(GM2.highlightSelected, GM1.player);
            //successful interception, go to 14.field battle
            landMvmt.defenderDice = command;
            if (hasLeader)
            {
                landMvmt.defenderDice += leaders.ElementAt(GM2.leaderSelected).battle;
            }
            landMvmt.fieldPlayer = GM1.player;
            landMvmt.status = 7;
        }
        forIntercept = false;
        GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
        startOther(0);
        GM2.highlightSelected = -1;
        inputNumberObject.reset();
        GM1.player = landMvmt.mvmtPlayer;
        GM2.onPlayerChange();
        //continue to look for powers that can have an interception attempt
        landMvmt.required2();
    }

    IEnumerator wait133()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Choose Units");
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        inputNumberObject.post();
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        List<int> trace = landMvmt.tempTrace;
        GM2.highlightSelected = -1;
        GM2.leaderSelected = -1;
        GM2.onRegLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        UnityEngine.Debug.Log("highlight " + GM2.highlightSelected.ToString());
        bool hasLeader = false;
        if (GM2.leaderSelected == spacesGM.ElementAt(landMvmt.destination).leader1 || GM2.leaderSelected == spacesGM.ElementAt(landMvmt.destination).leader2)
        {
            hasLeader = true;

        }
        int command = 0;
        if (!string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
        {
            
            command = int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text);
            
        }
        int randomIndex = UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7);
        if (hasLeader)
        {
            randomIndex = randomIndex + leaders.ElementAt(GM2.leaderSelected).battle;
        }
        UnityEngine.Debug.Log("avoid dice " + randomIndex.ToString());
        if (randomIndex >= 9 && command > 0)
        {
            spacesGM.ElementAt(landMvmt.destination).regular = spacesGM.ElementAt(landMvmt.destination).regular - command;
            spacesGM.ElementAt(GM2.highlightSelected).regular = spacesGM.ElementAt(GM2.highlightSelected).regular + command;
            regulars[landMvmt.destination] = regulars[landMvmt.destination] - command;
            regulars[GM2.highlightSelected] = regulars[GM2.highlightSelected] + command;
            GM2.onChangeReg(landMvmt.destination, GM1.player);
            GM2.onChangeReg(GM2.highlightSelected, GM1.player);
            //successful avoid
            landMvmt.status = 17;
        }
        forAvoid = false;
        GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
        startOther(0);
        GM2.highlightSelected = -1;
        inputNumberObject.reset();
       
        GM1.player = landMvmt.mvmtPlayer;
        GM2.onPlayerChange();
        //try to withdraw into fortifications
        landMvmt.status = 17;
        landMvmt.required2();
    }

    IEnumerator wait165()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Choose Destination");
        
        NavalMvmt navalMvmt = GameObject.Find("ProcedureButton").GetComponent("NavalMvmt") as NavalMvmt;
        navalMvmt.btn.interactable = false;
        List<int> trace = navalMvmt.tempTrace;
        GM2.highlightSelected = -1;
        GM2.leaderSelected = -1;
        GM2.onNavalLayer();
        GM2.onHighlight(trace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        UnityEngine.Debug.Log("highlight " + GM2.highlightSelected.ToString());
        bool hasLeader = false;
        if (GM2.leaderSelected == spacesGM.ElementAt(navalMvmt.destination[navalMvmt.avoidBattle]).leader1 || GM2.leaderSelected == spacesGM.ElementAt(navalMvmt.destination[navalMvmt.avoidBattle]).leader2)
        {
            hasLeader = true;

        }
        int command = 0;
        
        int randomIndex = UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7);
        if (hasLeader)
        {
            randomIndex = randomIndex + leaders.ElementAt(GM2.leaderSelected).battle;
        }
        UnityEngine.Debug.Log("avoid dice " + randomIndex.ToString());
        if (randomIndex >= 9 && command > 0)
        {
            spacesGM.ElementAt(navalMvmt.destination[navalMvmt.avoidBattle]).squadron = spacesGM.ElementAt(navalMvmt.destination[navalMvmt.avoidBattle]).squadron - command;
            spacesGM.ElementAt(GM2.highlightSelected).squadron = spacesGM.ElementAt(GM2.highlightSelected).squadron + command;
            
            GM2.onChangeSquadron(navalMvmt.destination[navalMvmt.avoidBattle], GM1.player);
            GM2.onChangeSquadron(GM2.highlightSelected, GM1.player);
            //successful avoid
            navalMvmt.status = 17;
        }
        forAvoidNaval = false;
        GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
        startOther(0);
        GM2.highlightSelected = -1;

        GM1.player = navalMvmt.mvmtPlayer;
        GM2.onPlayerChange();
        //go to next avoid battle
        navalMvmt.avoidBattle++;
    }

    public void withdraw()
    {
        LandMvmt landMvmt = GameObject.Find("ProcedureButton").GetComponent("LandMvmt") as LandMvmt;
        spacesGM.ElementAt(landMvmt.destination).sieged = true;
        forWithdraw = false;
        UnityEngine.Debug.Log("withdraw success");
        GM1.player = landMvmt.mvmtPlayer;
        GM2.onPlayerChange();
        landMvmt.status = 18;
        landMvmt.required2();
    }
}
