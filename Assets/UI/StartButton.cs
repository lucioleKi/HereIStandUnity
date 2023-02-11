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
    public int status;//1=intercept, 2 = avoid battle, 3=avoid naval, 4=withdraw into fortification, 5=intervene war, 6 = ransom leader, 7 = agree to be ransomed for a leader, 8 = remove excommunication
    // Start is called before the first frame update
    void Start()
    {

        btn = gameObject.GetComponent<Button>();
        cardIndex = 8;
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack(cardIndex));
        status = -1;

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
        string tempName = "";
        if (status == -1 || status == 5)
        {
            tempName = GameObject.Find("CardDisplay").GetComponent<Image>().sprite.name;
        }
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        switch (status)
        {
            case -1:

                cardIndex = int.Parse(tempName.Substring(4));
                GM2.onMandatory(cardIndex);
                break;
            case 1:
                StartCoroutine(wait132());
                break;
            case 2:
                StartCoroutine(wait133());
                break;
            case 3:
                withdraw();
                break;
            case 4:
                StartCoroutine(wait165());
                break;
            case 5:
                status = -1;

                cardIndex = int.Parse(tempName.Substring(4));
                switch (cardIndex)
                {
                    case 3:
                        MinorPower minorPower = new MinorPower();
                        minorPower.activate(3, 8);
                        GM1.diplomacyState[2, 3] = 1;
                        GM2.onChangeDip();
                        GM2.onDeactivateSkip();
                        DeckScript.hand2.RemoveAt(0);
                        GM2.chosenCard = "";
                        GM2.onChosenCard();
                        GM1.player = 2;
                        GM2.onPlayerChange();
                        GM2.currentCP = 5;
                        GM2.onCPChange(GM2.currentCP);
                        break;
                }
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                break;
            case 6:
                GM1.player = handMarkerScript.ransomedPower[0];
                GM2.onPlayerChange();
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Agree";
                GameObject.Find("Skip (TMP)").GetComponent<TextMeshProUGUI>().text = "Refuse";
                GM2.onSkipCard(2);
                status = 7;
                break;
            case 7:
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                GM2.onDeactivateSkip();
                status = -1;

                StartCoroutine(randomDraw());

                break;
                case 8:
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                StartCoroutine(removeExcommunication());
                GM2.onDeactivateSkip();
                status = -1;
                break;
            default:
                break;
        }

        if (status != 7)
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
                status = 1;

                break;
            case 2:
                btn.interactable = true;
                status = 2;

                break;
            case 3:
                btn.interactable = true;
                status = 3;
                break;
            case 4:
                btn.interactable = true;
                status = 4;

                break;
            case 5:
                btn.interactable = true;
                status = 5;
                break;
            case 6:
                btn.interactable = true;
                status = 6;
                break;
            case 7:
                btn.interactable = true;

                status = 7;
                break;
            case 8:
                btn.interactable = true;
                status = 8;
                break;
        }


    }

    void makeInteractable()
    {
        if (GM2.chosenCard == "" && (status >= 1 && status <= 3))
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

    IEnumerator randomDraw()
    {
        int randomIndex;
        List<CardObject> from = new List<CardObject>();
        List<CardObject> to = new List<CardObject>();
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        switch (GM1.player)
        {
            case 0:
                from = DeckScript.hand0;
                spacesGM.ElementAt(97).addLeader(handMarkerScript.ransomedLeader[0]);
                GM2.onChangeLeader(97, 0);
                break;
            case 1:
                from = DeckScript.hand1;
                spacesGM.ElementAt(83).addLeader(handMarkerScript.ransomedLeader[0]);
                GM2.onChangeLeader(83, 1);
                break;
            case 2:
                from = DeckScript.hand2;
                spacesGM.ElementAt(27).addLeader(handMarkerScript.ransomedLeader[0]);
                GM2.onChangeLeader(27, 2);
                break;
            case 3:
                from = DeckScript.hand3;
                spacesGM.ElementAt(41).addLeader(handMarkerScript.ransomedLeader[0]);
                GM2.onChangeLeader(41, 3);
                break;
            case 4:
                from = DeckScript.hand4;
                spacesGM.ElementAt(65).addLeader(handMarkerScript.ransomedLeader[0]);
                GM2.onChangeLeader(65, 4);
                break;
            case 5:
                from = DeckScript.hand5;
                spacesGM.ElementAt(0).addLeader(handMarkerScript.ransomedLeader[0]);
                GM2.onChangeLeader(0, 5);
                break;
        }


        switch (handMarkerScript.canRansom[0])
        {
            case 0:
                to = DeckScript.hand0;
                foreach (string s in handMarkerScript.bonus0.ToList())
                {
                    if (s == "Sprites/jpg/Leader/" + handMarkerScript.ransomedLeader[0].ToString())
                    {
                        handMarkerScript.bonus0.Remove(s);
                    }
                }
                break;
            case 1:
                to = DeckScript.hand1;
                foreach (string s in handMarkerScript.bonus1.ToList())
                {
                    if (s == "Sprites/jpg/Leader/" + handMarkerScript.ransomedLeader[0].ToString())
                    {
                        handMarkerScript.bonus1.Remove(s);
                    }
                }
                break;
            case 2:
                to = DeckScript.hand2;
                foreach (string s in handMarkerScript.bonus2.ToList())
                {
                    if (s == "Sprites/jpg/Leader/" + handMarkerScript.ransomedLeader[0].ToString())
                    {
                        handMarkerScript.bonus2.Remove(s);
                    }
                }
                break;
            case 3:
                to = DeckScript.hand3;
                foreach (string s in handMarkerScript.bonus3.ToList())
                {
                    if (s == "Sprites/jpg/Leader/" + handMarkerScript.ransomedLeader[0].ToString())
                    {
                        handMarkerScript.bonus3.Remove(s);
                    }
                }
                break;
            case 4:
                to = DeckScript.hand4;
                foreach (string s in handMarkerScript.bonus4.ToList())
                {
                    if (s == "Sprites/jpg/Leader/" + handMarkerScript.ransomedLeader[0].ToString())
                    {
                        handMarkerScript.bonus4.Remove(s);
                    }
                }
                break;
            case 5:
                to = DeckScript.hand5;
                foreach (string s in handMarkerScript.bonus5.ToList())
                {
                    if (s == "Sprites/jpg/Leader/" + handMarkerScript.ransomedLeader[0].ToString())
                    {
                        handMarkerScript.bonus5.Remove(s);
                    }
                }
                break;
        }
        if (from.ElementAt(0).id < 8)
        {
            randomIndex = UnityEngine.Random.Range(1, from.Count);
        }
        else
        {
            randomIndex = UnityEngine.Random.Range(0, from.Count);
        }

        to.Add(from.ElementAt(randomIndex));
        string cardSelected;
        if (from.ElementAt(randomIndex).id < 10)
        {
            cardSelected = "HIS-00" + from.ElementAt(randomIndex).id;

        }
        else if (from.ElementAt(randomIndex).id < 100)
        {
            cardSelected = "HIS-0" + from.ElementAt(randomIndex).id;
        }
        else
        {
            cardSelected = "HIS-" + from.ElementAt(randomIndex).id;
        }
        GM2.chosenCard = cardSelected;
        GM2.onChosenCard();
        from.RemoveAt(randomIndex);
        yield return new WaitForSeconds(3);
        GM2.chosenCard = "";
        GM2.onChosenCard();
        handMarkerScript.canRansom.RemoveAt(0);
        handMarkerScript.ransomedPower.RemoveAt(0);
        handMarkerScript.ransomedLeader.RemoveAt(0);
        GM2.onPhase3();
    }

    IEnumerator removeExcommunication()
    {
        int randomIndex;
        List<CardObject> from = new List<CardObject>();
        HandMarkerScript handMarkerScript = GameObject.Find("HandMarkerDisplay").GetComponent("HandMarkerScript") as HandMarkerScript;
        switch (handMarkerScript.excom[0])
        {
            
            case 1:
                from = DeckScript.hand1;
                foreach (string s in handMarkerScript.bonus1.ToList())
                {
                    if (s == "Sprites/jpg/negative1Card")
                    {
                        handMarkerScript.bonus1.Remove(s);
                    }
                }
                break;
            case 2:
                from = DeckScript.hand2;
                foreach (string s in handMarkerScript.bonus2.ToList())
                {
                    if (s == "Sprites/jpg/negative1Card")
                    {
                        handMarkerScript.bonus2.Remove(s);
                    }
                }
                break;
            case 3:
                from = DeckScript.hand3;
                foreach (string s in handMarkerScript.bonus3.ToList())
                {
                    if (s == "Sprites/jpg/negative1Card")
                    {
                        handMarkerScript.bonus3.Remove(s);
                    }
                }
                break;
        }

        if (from.ElementAt(0).id < 8)
        {
            randomIndex = UnityEngine.Random.Range(1, from.Count);
        }
        else
        {
            randomIndex = UnityEngine.Random.Range(0, from.Count);
        }

        string cardSelected;
        if (from.ElementAt(randomIndex).id < 10)
        {
            cardSelected = "HIS-00" + from.ElementAt(randomIndex).id;

        }
        else if (from.ElementAt(randomIndex).id < 100)
        {
            cardSelected = "HIS-0" + from.ElementAt(randomIndex).id;
        }
        else
        {
            cardSelected = "HIS-" + from.ElementAt(randomIndex).id;
        }
        GM2.chosenCard = cardSelected;
        GM2.onChosenCard();
        
        GM1.player = 4;
        GM2.onPlayerChange();
        GM1.StPeters[0]+=from.ElementAt(randomIndex).CP;
        if (GM1.StPeters[0] >= 5)
        {
            GM1.StPeters[1]++;
            GM1.StPeters[0] = 0;
        }
        GM1.updateVP();
        GM2.onVP();
        from.RemoveAt(randomIndex);
        yield return new WaitForSeconds(3);
        GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
        GM2.chosenCard = "";
        GM2.onChosenCard();
        handMarkerScript.excom.RemoveAt(0);
        GM1.player = 0;
        GM2.onPlayerChange();
        GM2.onPhase3();
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
        status = -1;
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
        status = -1;
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
        status = -1;
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
        status = -1;
        GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
        UnityEngine.Debug.Log("withdraw success");
        GM1.player = landMvmt.mvmtPlayer;
        GM2.onPlayerChange();
        landMvmt.status = 18;
        landMvmt.required2();
    }
}
