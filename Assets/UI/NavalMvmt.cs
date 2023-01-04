using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GraphUtils;
using static DeckScript;
using static GM2;

public class NavalMvmt : MonoBehaviour
{
    public Button btn;
    public int mvmtPlayer;
    public int status;
    public int btnStatus;
    public List<int> initial;
    public List<int> destination;
    public List<int> number;
    public List<bool> hasLeader;
    public List<int> canIntercept;
    public int fieldPlayer;
    public List<int> attackerDice;
    public List<int> defenderDice;
    public int attackerHit;
    public int defenderHit;
    public bool attackerElim;
    public bool defenderElim;
    public List<int> tempTrace;
    public int avoidBattle;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(() => buttonCallBack());
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        status = -1;
        btnStatus = -1;
        hasLeader = new List<bool>();

        tempTrace = new List<int>();
        initial = new List<int>();
        destination = new List<int>();
        number = new List<int>();
        canIntercept = new List<int>();
        attackerDice = new List<int>();
        defenderDice = new List<int>();
        attackerHit = 0;
        defenderHit = 0;
        attackerElim = false;
        defenderElim = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void buttonCallBack()
    {
        if (btnStatus != -1)
        {
            btn.interactable = false;
        }
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        //HighlightCPScript highlightCPScript = GameObject.Find("HighlightCPDisplay").GetComponent("HighlightCPScript") as HighlightCPScript;
        switch (btnStatus)
        {
            case 0:
                StopCoroutine("wait1611");
                GM2.onRemoveHighlight();
                //highlightCPScript.removeHighlight();
                LayerScript layerScript = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
                layerScript.changeLayer();
                status = 2;
                tempTrace.Clear();
                UnityEngine.Debug.Log("here2");
                required2();
                break;
            case 1:
                //skip HIS-031
                status = 3;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 2:
                //skip 1 interception
                for (int i = 0; i < canIntercept.Count(); i++)
                {
                    if (canIntercept[i] != -1)
                    {
                        canIntercept[i] = -1;
                        GM1.player = mvmtPlayer;
                        GM2.onPlayerChange();
                        return;
                    }
                }
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                startButton.status = 1;
                status = 4;
                required2();
                break;
            case 3:
                //skip 1 avoid battle
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";
                startButton.status = 3;
                avoidBattle++;
                break;
            case 4:
                //skip HIS-024, either player
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                status = 7;
                required2();
                break;
            case 5:
                //skip HIS-001
                status = 9;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
            case 6:
                //skip HIS-034
                status = 10;
                GM2.chosenCard = "";
                GM2.onChosenCard();
                GM1.player = mvmtPlayer;
                GM2.onPlayerChange();
                required2();
                break;
        }

    }

    public void post()
    {
        GM2.boolStates[31] = true;
        btn.interactable = false;
        mvmtPlayer = GM1.player;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        status = 0;
        initial.Clear();
        destination.Clear();
        number.Clear();
        hasLeader.Clear();
        canIntercept.Clear();
        attackerDice.Clear();
        defenderDice.Clear();
        attackerHit = 0;
        defenderHit = 0;
        attackerElim = false;
        defenderElim = false;
        tempTrace = findAllSquadrons(GM1.player);
        required2();
    }

    void reset()
    {
        GM1.player = mvmtPlayer;
        GM2.onPlayerChange();
        btn.interactable = false;
        btnStatus = -1;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        status = -1;
        initial.Clear();
        destination.Clear();
        number.Clear();
        hasLeader.Clear();
        canIntercept.Clear();
        CPTextScript textScript = GameObject.Find("CPText").GetComponent("CPTextScript") as CPTextScript;
        GM2.onCPChange(textScript.displayCP - 1);
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.reset();
        GM2.boolStates[31] = false;
    }

    public void required2()
    {
        switch (status)
        {
            case 0:
                StartCoroutine("wait1611");
                btn.interactable = true;
                btnStatus = 0;
                break;
            case 1:
                StartCoroutine(wait1612());
                break;
            case 2:
                UnityEngine.Debug.Log("here3");
                check163();
                break;
            case 3:
                StartCoroutine(check164());
                break;
            case 4:
                StartCoroutine(check165());
                break;

            case 5:

                battleDice();
                break;
            case 6:
                //loop start?
                //attacker/defender combat cards
                check1623();
                break;
            case 7:
                StartCoroutine(roll165());
                break;
            case 8:
                //if power is ottoman, offer the option to play janissaries
                GM1.player = 0;
                GM2.onPlayerChange();
                GM2.chosenCard = "HIS-001";
                GM2.onChosenCard();
                btn.interactable = true;
                btnStatus = 7;
                break;
            case 9:
                //find HIS-034
                check34();
                break;
            case 10:
                //declare winner
                StartCoroutine(check1627());
                break;
            case 11:
                moveClear();
                break;
            case 12:
                pop();
                break;
        }
    }

    IEnumerator wait1611()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Declare Formation");
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        inputNumberObject.post();
        GM2.highlightSelected = -1;
        leaderSelected = -1;
        GM2.onNavalLayer();
        GM2.onHighlight(tempTrace);
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        tempTrace.Remove(GM2.highlightSelected);
        initial.Add(GM2.highlightSelected);
        if (!string.IsNullOrEmpty(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text))
        {
            number.Add(int.Parse(GameObject.Find("InputNumber").GetComponent<TMP_InputField>().text));
        }
        else
        {
            number.Add(0);
        }
        if (GM2.leaderSelected == spacesGM.ElementAt(GM2.highlightSelected).leader1 || leaderSelected == spacesGM.ElementAt(GM2.highlightSelected).leader2)
        {
            hasLeader.Add(true);

        }
        else
        {
            hasLeader.Add(false);
        }
        GM2.highlightSelected = -1;
        inputNumberObject.reset();
        status = 1;
        required2();
    }

    IEnumerator wait1612()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.post("Declare Destination");

        GM2.highlightSelected = -1;

        GM2.onNavalLayer();
        UnityEngine.Debug.Log(initial.Last());
        GM2.onHighlight(findNavalDestination(mvmtPlayer, initial.Last()));
        while (GM2.highlightSelected == -1)
        {
            yield return null;
        }
        destination.Add(GM2.highlightSelected);
        List<int> interceptSpace = new List<int>();
        for (int i = 0; i < 6; i++)
        {

            if (GM1.diplomacyState[GM1.player, i] == 1 || GM1.diplomacyState[i, GM1.player] == 1)
            {

                for (int j = 0; j < spaces.ElementAt(destination.Last()).adjacent.Count(); j++)
                {
                    if (spacesGM.ElementAt(spaces.ElementAt(destination.Last()).adjacent[j] - 1).regularPower == i && spacesGM.ElementAt(spaces.ElementAt(destination.Last()).adjacent[j] - 1).squadron>0)
                    {
                        interceptSpace.Add(spaces.ElementAt(destination.Last()).adjacent[j] - 1);
                        UnityEngine.Debug.Log(spaces.ElementAt(destination.Last()).adjacent[j] - 1);
                    }

                }
                if (interceptSpace.Count() > 0)
                {
                    canIntercept.Add(i);
                    continue;
                }
            }



        }
        if (canIntercept.Count() < initial.Count())
        {
            canIntercept.Add(-1);
        }
        GM2.highlightSelected = -1;
        if (tempTrace.Count() > 0)
        {
            status = 0;
        }
        else
        {
            UnityEngine.Debug.Log("here1");
            status = 2;
        }
        currentTextObject.reset();
        required2();
    }

    void check163()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.reset();
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        inputNumberObject.reset();
        int has31 = -1;
        for (int i = 0; i < 6; i++)
        {
            List<CardObject> temp = null;
            switch (i)
            {
                case 0:
                    temp = hand0;
                    break;
                case 1:
                    temp = hand1;
                    break;
                case 2:
                    temp = hand2;
                    break;
                case 3:
                    temp = hand3;
                    break;
                case 4:
                    temp = hand4;
                    break;
                case 5:
                    temp = hand5;
                    break;
            }
            for (int j = 0; j < temp.Count(); j++)
            {
                if (temp.ElementAt(j).id == 31)
                {
                    has31 = i;
                }
            }
        }
        if (has31 != -1 && has31 != GM1.player)
        {
            UnityEngine.Debug.Log(has31);
            GM1.player = has31;
            GM2.onPlayerChange();
            GM2.chosenCard = "HIS-031";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 1;

        }
        else
        {
            UnityEngine.Debug.Log("here4");
            status = 3;
            required2();
        }

    }

    IEnumerator check164()
    {
        btn.interactable = true;
        btnStatus = 2;
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        for (int i = 0; i < canIntercept.Count(); i++)
        {
            if (canIntercept[i] != -1)
            {
                GM1.player = canIntercept[i];
                GM2.onPlayerChange();
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Intercept";
                startButton.startOther(1);
            }

            while (canIntercept[i] != -1)
            {
                yield return null;
            }
        }


        GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";

        startButton.btn.interactable = false;
        startButton.status=-1;
        status = 4;
        required2();

    }

    IEnumerator check165()
    {
        btn.interactable = true;
        btnStatus = 3;
        StartButton startButton = GameObject.Find("Start").GetComponent("StartButton") as StartButton;
        avoidBattle = 0;
        for (int i = 0; i < destination.Count(); i++)
        {

            if (spacesGM.ElementAt(destination[i]).squadron > 0 && spacesGM.ElementAt(destination[i]).controlPower != mvmtPlayer)
            {
                UnityEngine.Debug.Log(spacesGM.ElementAt(destination[i]).controlPower + " " + destination[i]);
                tempTrace = findNavalAvoidD(spacesGM.ElementAt(destination[i]).controlPower, destination[i]);
                GM1.player = spacesGM.ElementAt(destination[i]).controlPower;
                GM2.onPlayerChange();
                GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Avoid Battle";
                startButton.startOther(4);
                btnStatus = 3;
            }
            else
            {
                avoidBattle++;
            }

            while (avoidBattle == i)
            {
                yield return null;
            }
            UnityEngine.Debug.Log(avoidBattle);
        }


        GameObject.Find("StartText (TMP)").GetComponent<TextMeshProUGUI>().text = "Start";

        startButton.btn.interactable = false;
        startButton.status=-1;
        status = 5;
        required2();
    }

    void battleDice()
    {
        for (int i = 0; i < initial.Count(); i++)
        {
            UnityEngine.Debug.Log(initial[i]+" " + destination[i]);
            int permitted = 0;
            attackerDice.Add(0);
            defenderDice.Add(0);
            //todo: include 2 leaders
            if (hasLeader[i])
            {
                permitted = leaders.ElementAt(leaderSelected - 1).command;

            }


            if (number[i] > spacesGM.ElementAt(initial[i]).squadron)
            {
                number[i] = spacesGM.ElementAt(initial[i]).squadron;
            }

            attackerDice[i] += 2 * number[i];
            if (hasLeader[i])
            {
                attackerDice[i] += leaders.ElementAt(leaderSelected - 1).battle;
            }
            //if (spacesGM.ElementAt(destination[i]).leader1 != 0)
            //{
            //    defenderDice[i] += leaders.ElementAt(spacesGM.ElementAt(destination[i]).leader1 - 1).battle;
            //}
            //if (spacesGM.ElementAt(destination[i]).leader2 != 0)
            //{
            //    defenderDice[i] += leaders.ElementAt(spacesGM.ElementAt(destination[i]).leader2 - 1).battle;
            //}
            if (spacesGM.ElementAt(destination[i]).controlPower == mvmtPlayer|| spacesGM.ElementAt(destination[i]).controlPower ==-1) {
                defenderDice[i] = 1;
            }
            else
            {
                defenderDice[i] += spacesGM.ElementAt(destination[i]).squadron * 2 + 1;
            }
            
            UnityEngine.Debug.Log(attackerDice[i]);
            UnityEngine.Debug.Log(defenderDice[i]);
        }

        status = 6;
        
        required2();
    }

    void check1623()
    {
        if (defenderDice[0] == 1)
        {
            status = 11;
            required2();
        }
        else
        {
            fieldPlayer = spacesGM.ElementAt(destination[0]).controlPower;
            int has24 = -1;
            for (int i = 0; i < 6; i++)
            {
                List<CardObject> temp = null;
                switch (i)
                {
                    case 0:
                        temp = hand0;
                        break;
                    case 1:
                        temp = hand1;
                        break;
                    case 2:
                        temp = hand2;
                        break;
                    case 3:
                        temp = hand3;
                        break;
                    case 4:
                        temp = hand4;
                        break;
                    case 5:
                        temp = hand5;
                        break;
                }
                for (int j = 0; j < temp.Count(); j++)
                {

                    if (temp.ElementAt(j).id == 24)
                    {
                        has24 = i;
                    }
                }
            }
            if (has24 == fieldPlayer || has24 == GM1.player)
            {
                UnityEngine.Debug.Log(has24);
                GM1.player = has24;
                GM2.onPlayerChange();
                GM2.chosenCard = "HIS-024";
                GM2.onChosenCard();
                btn.interactable = true;
                btnStatus = 4;

            }
            else
            {
                UnityEngine.Debug.Log("here7");
                status = 7;
                required2();
            }
        }
        


    }

    IEnumerator roll165()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;

        attackerHit = 0;
        defenderHit = 0;

        for (int i = 0; i < defenderDice.ElementAt(0); i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);
            if (randomIndex >= 5)
            {
                defenderHit++;
            }
        }
        for (int i = 0; i < attackerDice.ElementAt(0); i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 7);
            if (randomIndex >= 5)
            {
                attackerHit++;
            }
        }

        currentTextObject.post("Attacker hit: " + attackerHit.ToString() + " out of " + attackerDice[0].ToString() + ".\nDefender hit: " + defenderHit.ToString() + " out of " + defenderDice[0].ToString() + ".");
        yield return new WaitForSeconds(3);
        if ((mvmtPlayer == 0 || fieldPlayer == 0) && hand0.ElementAt(0).cardType != 0)
        {
            status = 8;

        }
        else
        {
            status = 9;
        }
        required2();
    }

    void check34()
    {
        int has34 = -1;
        for (int i = 0; i < 6; i++)
        {
            List<CardObject> temp = null;
            switch (i)
            {
                case 0:
                    temp = hand0;
                    break;
                case 1:
                    temp = hand1;
                    break;
                case 2:
                    temp = hand2;
                    break;
                case 3:
                    temp = hand3;
                    break;
                case 4:
                    temp = hand4;
                    break;
                case 5:
                    temp = hand5;
                    break;
            }
            for (int j = 0; j < temp.Count(); j++)
            {

                if (temp.ElementAt(j).id == 34)
                {
                    has34 = i;
                }
            }
        }
        if (has34 != -1)
        {
            UnityEngine.Debug.Log(has34);
            GM1.player = has34;
            GM2.onPlayerChange();
            GM2.chosenCard = "HIS-034";
            GM2.onChosenCard();
            btn.interactable = true;
            btnStatus = 6;

        }
        else
        {
            status = 10;
            required2();
        }
    }

    IEnumerator check1627()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        if (attackerHit <= defenderHit)
        {
            casualties(mvmtPlayer, initial[0], attackerHit / 2);
            casualties(fieldPlayer, destination[0], (defenderHit + 1) / 2);

        }
        else
        {
            casualties(mvmtPlayer, initial[0], (attackerHit + 1) / 2);
            casualties(fieldPlayer, destination[0], defenderHit / 2);
        }

        if (attackerElim && defenderElim)
        {
            if (attackerHit <= defenderHit)
            {
                spacesGM.ElementAt(destination[0]).squadron++;
                spacesGM.ElementAt(destination[0]).regularPower = fieldPlayer;
                GM2.onChangeSquadron(destination[0], fieldPlayer);
                defenderElim = false;
            }
            else
            {
                spacesGM.ElementAt(initial[0]).squadron++;
                spacesGM.ElementAt(initial[0]).regularPower = mvmtPlayer;
                GM2.onChangeSquadron(initial[0], mvmtPlayer);
                attackerElim = false;
            }
        }

        if (attackerHit > defenderHit)
        {
            currentTextObject.post("Winner: Attacker");
        }
        else
        {
            currentTextObject.post("Winner: Defender");
        }
        yield return new WaitForSeconds(3);
        if (defenderElim)
        {
            status = 11;
        }
        else
        {
            status = 12;

        }
        required2();
    }



    void moveClear()
    {
        spacesGM.ElementAt(destination[0]).squadron = spacesGM.ElementAt(destination[0]).squadron + number[0];

        spacesGM.ElementAt(initial[0]).squadron = spacesGM.ElementAt(initial[0]).squadron - number[0];
        onChangeSquadron(destination[0], GM1.player);
        onChangeSquadron(initial[0], GM1.player);

        if (spacesGM.ElementAt(initial[0]).squadron == 0 && spacesGM.ElementAt(initial[0]).merc == 0 && spacesGM.ElementAt(initial[0]).regular == 0 && spaces.ElementAt(initial[0]).spaceType == 0)
        {
            spacesGM.ElementAt(initial[0]).regularPower = -1;
        }
        status = 12;
        required2();
    }

    void pop()
    {
        initial.RemoveAt(0);
        destination.RemoveAt(0);
        number.RemoveAt(0);
        hasLeader.RemoveAt(0);
        canIntercept.RemoveAt(0);
        attackerDice.RemoveAt(0);
        defenderDice.RemoveAt(0);
        if (initial.Count() > 0)
        {
            status = 6;
            required2();
        }
        else
        {
            GM2.boolStates[31] = false;
            reset();
        }
    }

    void casualties(int player, int place, int hit)
    {

        if (spacesGM.ElementAt(place).squadron >= hit)
        {
            spacesGM.ElementAt(place).squadron -= hit;
            hit = 0;
            GM2.onChangeSquadron(place, player);
            return;
        }
        else if (spacesGM.ElementAt(place).squadron > 0)
        {
            if (player == mvmtPlayer)
            {
                attackerElim = true;
            }
            else
            {
                defenderElim = true;
            }
            hit -= spacesGM.ElementAt(place).squadron;
            spacesGM.ElementAt(place).squadron = 0;
            if (regulars[place] == 0)
            {
                spacesGM.ElementAt(place).regularPower = -1;
            }
            GM2.onChangeSquadron(place, player);
        }
        if (player == 0 && spacesGM.ElementAt(place).corsair > 0)
        {
            spacesGM.ElementAt(place).corsair -= Math.Min(hit, spacesGM.ElementAt(place).corsair);
            GM2.onChangeCorsair(place);
        }

    }
}
