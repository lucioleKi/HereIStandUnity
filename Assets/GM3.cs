
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM2;
using static GraphUtils;

public class GM3
{
    public static GM3 instance;
    public static GM3 Instance
    {
        get
        {
            if (instance == null)
            {
                UnityEngine.Debug.Log("GM3 not initiated.");
            }
            return instance;
        }
    }

    public IEnumerator HIS001B()
    {
        List<int> trace = new List<int>();
        
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).controlPower == 0&i!=111)
            {
                trace.Add(i);
                
            }
        }
        for(int i = 0; i < 4; i++)
        {
            currentTextObject.pauseColor();
            currentTextObject.post("Pick "+(4-i).ToString()+ " highlighted target spaces to add 1 regular.");
            highlightSelected = -1;
            onRegLayer();
            onHighlight(trace);
            while (player != 0 || highlightSelected == -1)//if player is not 1 this wouldn't work
            {
                yield return null;
            }
            regulars[highlightSelected]++;
            spacesGM.ElementAt(highlightSelected).regular++;
            onChangeReg(highlightSelected, 0);
        }
        highlightSelected = -1;
        yield return new WaitForSeconds(3);

        currentTextObject.reset();

    }

    public IEnumerator HIS002()
    {
        List<int> trace = new List<int>();
        int CharlesPos = -1;

        highlightSelected = -1;
        InputToggleObject inputToggleObject = GameObject.Find("InputToggle").GetComponent("InputToggleObject") as InputToggleObject;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;

        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).controlPower == 1)
            {
                trace.Add(i);
                if (spacesGM.ElementAt(i).leader1 == 2 || spacesGM.ElementAt(i).leader1 == 4)
                {
                    CharlesPos = i;
                    if (spacesGM.ElementAt(i).leader2 == 2 || spacesGM.ElementAt(i).leader2 == 4)
                    {
                        currentTextObject.pauseColor();
                        currentTextObject.post("Also move Duke of Alva?");
                        inputToggleObject.post();
                    }
                }
            }
        }
        LayerScript layerObject = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
        layerObject.highlight1Leader("leader_2");
        onHighlight(trace);
        while (player != 1 || highlightSelected == -1)
        {
            yield return null;
        }
        layerObject.reset1Leader();
        if (CharlesPos != -1)
        {
            spacesGM.ElementAt(CharlesPos).removeLeader(2);

            if (inputToggleObject.GetComponent<Toggle>().isOn)
            {
                currentTextObject.restartColor();
                spacesGM.ElementAt(CharlesPos).removeLeader(4);
            }
            
            spacesGM.ElementAt(highlightSelected).addLeader(2);
            UnityEngine.Debug.Log(highlightSelected.ToString());
            onChangeLeader(highlightSelected, 2);
            if (inputToggleObject.GetComponent<Toggle>().isOn)
            {
                spacesGM.ElementAt(highlightSelected).addLeader(4);
                onChangeLeader(highlightSelected, 4);
            }

        }
        yield return new WaitForSeconds(3);

        currentTextObject.reset();

        inputToggleObject.reset();
        hand1.RemoveAt(0);
    }

    public IEnumerator HIS003()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        while (false){
            yield return null;
        }
    }

    public IEnumerator HIS004()
    {
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        int randomIndex = UnityEngine.Random.Range(1, 7);
        int bonus = 0;
        if (spacesGM.ElementAt(76).controlPower == 2)
        {
            bonus = bonus+2;
        }
        if (spacesGM.ElementAt(77).controlPower == 2)
        {
            bonus++;
        }
        int italian = 0;
        bool frenchHome = false;
        bool foreignReg = false;
        for(int i=0; i < spacesGM.Count(); i++)
        {
            if(spacesGM.ElementAt(i).controlPower==2&&spaces.ElementAt(i).language == (Language)3)
            {
                italian++;
            }
            if(spacesGM.ElementAt(i).controlPower != 3 && spaces.ElementAt(i).homePower == (PowerType2)3&&(spaces.ElementAt(i).spaceType == (SpaceType)2 ||spaces.ElementAt(i).spaceType == (SpaceType)3))
            {
                frenchHome = true;
            }
            if(spacesGM.ElementAt(i).controlPower != 3 && spaces.ElementAt(i).homePower == (PowerType2)3 && (spaces.ElementAt(i).spaceType == (SpaceType)2 || spaces.ElementAt(i).spaceType == (SpaceType)3) && spacesGM.ElementAt(i).regular > 0)
            {
                foreignReg = true;
            }
        }
        if (italian >= 3)
        {
            bonus = bonus+2;
        }
        //no touranament scenario for now
        if (frenchHome)
        {
            bonus--;
        }
        if (foreignReg) {
            bonus = bonus - 2;
        }
        int total = randomIndex + bonus;
        if (total > 2)
        {
            //updateVP
            GM1.chateauxC++;
            GM1.updateVP();
            GM2.onVP();

        }
        hand3.RemoveAt(0);
        if (bonus < 0)
        {
            if (total < 3 || total > 7)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + bonus.ToString() + "=" + total.ToString()+"\nDraw 2 cards, then discard 1.");
                
                hand3.AddRange(activeCards.GetRange(0, 2));
                activeCards.RemoveRange(0, 2);
                GM2.boolStates[0] = true;
            }
            else if (total == 3 || total == 4)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card, then discard 1.");
                GM2.boolStates[0] = true;
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            else if(total >5)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card.");
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
            
        }
        else
        {
            if (total < 3 || total > 7)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() +"+"+ bonus.ToString() + "=" + total.ToString() + "\nDraw 2 cards, then discard 1.");
                hand3.AddRange(activeCards.GetRange(0, 2));
                activeCards.RemoveRange(0, 2);
                GM2.boolStates[0] = true;
            }
            else if (total == 3 || total == 4)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + "+" + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card, then discard 1.");
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
                GM2.boolStates[0] = true;
            }
            else if (total > 5)
            {
                currentTextObject.post("Modified roll: " + randomIndex.ToString() + "+" + bonus.ToString() + "=" + total.ToString() + "\nDraw 1 card.");
                hand3.AddRange(activeCards.GetRange(0, 1));
                activeCards.RemoveRange(0, 1);
            }
        }
        while (GM2.boolStates[0])
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        currentTextObject.restartColor();
        currentTextObject.reset();
        chosenCard = "";
        onChosenCard();


    }

    public IEnumerator HIS006()
    {
        InputToggleObject inputToggleObject = GameObject.Find("InputToggle").GetComponent("InputToggleObject") as InputToggleObject;
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        InputNumberObject inputNumberObject = GameObject.Find("InputNumber").GetComponent("InputNumberObject") as InputNumberObject;
        currentTextObject.pauseColor();
        currentTextObject.post("Specify your own debater?\nSpecify language zone.");
        inputToggleObject.post();
        inputNumberObject.post();
        DebateNScript debateNScript = GameObject.Find("DebateNext").GetComponent("DebateNScript") as DebateNScript;
        debateNScript.post();
        while (debateNScript.btn.interactable==true)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        currentTextObject.reset();
        inputNumberObject.reset();
        inputToggleObject.reset();
        currentTextObject.restartColor();
        chosenCard = "";
        onChosenCard();
        hand4.RemoveAt(0);
    }

    public IEnumerator HIS008()
    {
        GM1.enq1("Protestant player to pick 5 target spaces");
        activeReformers.Add(reformers.ElementAt(0));
        GameObject tempObject = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Objects/Reformer4/Luther"), new Vector3(spaces.ElementAt(0).posX + 965, spaces.ElementAt(0).posY + 545, 0), Quaternion.identity);
        tempObject.transform.SetParent(GameObject.Find("Reformers").transform);
        tempObject.name = "Luther";
        tempObject.SetActive(true);
        highlightSelected = 0;
        changeReligion();
        CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
        currentTextObject.pauseColor();
        currentTextObject.post("Pick 5 highlighted target spaces");
        for (int i = 0; i < 5; i++) { 
        


            List<int> pickSpaces = highlightReformation();
            highlightSelected = -1;
            onNoLayer();
            onHighlight(pickSpaces);

            onHighlightSelected += reformAttempt;
            while (highlightSelected == -1)
            {
                //UnityEngine.Debug.Log("here");
                yield return null;
            }

            UnityEngine.Debug.Log("end");
            //onRemoveHighlight(converted);
        }
        GM1.deq1(1);
        GM1.enq2("Any player to go to phase 2");
        yield return new WaitForSeconds(3);
        currentTextObject.reset();
        currentTextObject.restartColor();
        highlightSelected = -1;
        chosenCard = "";
        onChosenCard();
        //remove Luther's 95 theses from backend decks
        //cards.RemoveAt(7);
        hand5.RemoveAt(0);
    }

    public void HIS009()
    {

        regulars[111] = 2;
        regularsPower[111] = 0;
        spacesGM.ElementAt(111).controlMarker = 3;
        spacesGM.ElementAt(111).controlPower = 0;
        spacesGM.ElementAt(111).regular = 2;
        spacesGM.ElementAt(111).corsair = 2;
        spacesGM.ElementAt(111).leader1 = 17;
        onChangeReg(111, 0);
        onChangeCorsair(111);
        onChangeLeader(111, 17);
        //mandatory event by turn 3
        GM2.boolStates[3]=true;
    }

    public void HIS010()
    {
        GM1.updateRuler(4, 10);
        onChangeRuler(4, 10);
        //mandatory event by turn 2
        GM2.boolStates[5]=true;
    }

    public void HIS014()
    {
        GM1.updateRuler(4, 14);
        onChangeRuler(4, 14);
        //mandatory event by turn 4
        GM2.boolStates[7]=true;
    }

    public void HIS016()
    {
        GM1.updateRuler(5, 16);
        onChangeRuler(5, 16);
    }

    public void HIS018()
    {

        int pos = -1;
        for(int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).leader1 == 17 || spacesGM.ElementAt(i).leader2 == 17)
            {
                pos = i;
                spacesGM.ElementAt(i).removeLeader(17);
                spacesGM.ElementAt(i).addLeader(18);
                break;
            }
        }
        if (pos != -1)
        {
            onChangeLeader(-1, 17);
            onChangeLeader(pos, 18);
        }
        //todo: remove this card from deck
    }

    public void HIS019()
    {
        GM1.updateRuler(2, 19);
        onChangeRuler(2, 19);
        int pos = -1;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).leader1 == 8 || spacesGM.ElementAt(i).leader2 == 8)
            {
                pos = i;
                spacesGM.ElementAt(i).removeLeader(8);
                spacesGM.ElementAt(i).addLeader(3);
                break;
            }
        }
        UnityEngine.Debug.Log("position" + pos.ToString());
        if (pos != -1)
        {
            onChangeLeader(-1, 8);
            onChangeLeader(pos, 3);
            //todo: debug overlapping leaders
        }
        else
        {
            onChangeLeader(27, 3);
        }
    }

    public void HIS020()
    {
        GM1.updateRuler(3, 20);
        onChangeRuler(3, 20);
        int pos = -1;
        for (int i = 0; i < spacesGM.Count(); i++)
        {
            if (spacesGM.ElementAt(i).leader1 == 6 || spacesGM.ElementAt(i).leader2 == 6)
            {
                pos = i;
                spacesGM.ElementAt(i).removeLeader(6);
                spacesGM.ElementAt(i).addLeader(7);
                break;
            }
        }
        if (pos != -1)
        {
            onChangeLeader(-1, 6);
            onChangeLeader(pos, 7);
        }
        else
        {
            onChangeLeader(41, 7);
        }
    }

    public void HIS021()
    {
        if (GM1.rulers[2].name == "Henry VIII")
        {
            int pos = -1;
            for (int i = 0; i < spacesGM.Count(); i++)
            {
                if (spacesGM.ElementAt(i).leader1 == 8 || spacesGM.ElementAt(i).leader2 == 8)
                {
                    pos = i;
                    spacesGM.ElementAt(i).removeLeader(8);
                    spacesGM.ElementAt(i).addLeader(3);
                    break;
                }
            }
            UnityEngine.Debug.Log("position" + pos.ToString());
            if (pos != -1)
            {
                onChangeLeader(-1, 8);
                onChangeLeader(pos, 3);
               
            }
            else
            {
                onChangeLeader(27, 3);
            }
        }



        GM1.updateRuler(2, 21);
        onChangeRuler(2, 21);
        //todo: other functionalities
    }

    public void HIS022()
    {
        GM1.updateRuler(4, 22);
        onChangeRuler(4, 22);
    }

    public void HIS023()
    {
        GM1.updateRuler(2, 23);
        onChangeRuler(2, 23);
    }

    public IEnumerator HIS065()
    {
        if (DeckScript.debaters.ElementAt(12).status == (DebaterStatus)1) {
            DeckScript.debaters.ElementAt(12).status = (DebaterStatus)2;
            CurrentTextScript currentTextObject = GameObject.Find("CurrentText").GetComponent("CurrentTextScript") as CurrentTextScript;
            currentTextObject.pauseColor();
            currentTextObject.post("Pick 6 highlighted target spaces");
            for (int i = 0; i < 5; i++)
            {


                List<int> pickSpaces = highlightReformation();
                highlightSelected = -1;
                onSpaceLayer();
                onHighlight(pickSpaces);

                onHighlightSelected += reformAttempt;
                while (highlightSelected == -1)
                {
                    //UnityEngine.Debug.Log("here");
                    yield return null;
                }

                UnityEngine.Debug.Log("end");
                //onRemoveHighlight(converted);
            }
            yield return new WaitForSeconds(3);
            currentTextObject.reset();
            currentTextObject.restartColor();
            highlightSelected = -1;
            chosenCard = "";
            onChosenCard();
        }
    }
}
