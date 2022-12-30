using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static EnumSpaceScript;

[System.Serializable]
public class SaveData
{
    //DeckScript
    public List<CardObject> cards;
    public List<CardObject> activeCards;
    public List<CardObject> discardCards;
    public List<CardObject> hand0;
    public List<CardObject> hand1;
    public List<CardObject> hand2;
    public List<CardObject> hand3;
    public List<CardObject> hand4;
    public List<CardObject> hand5;
    public int[] regulars;
    public List<SpaceGM> spacesGM;

    //GM1
    public int player;
    public int CurrentCard;
    public int turn;
    public int phase;
    public int segment;
    public int englishSpaces;
    public int protestantSpaces;
    public RulerClass[] rulers;
    public int[] cardTracks;
    public int[] VPs;
    public int[] translations;
    public bool[] excommunicated;
    public int[] StPeters;
    public Religion[] religiousInfluence;
    //public PowerObject[] powerObjects;
    public int[,] diplomacyState;
    
    public int[] bonusVPs;
    public int piracyC;
    public int chateauxC;
    public Queue<string> toDo;

    //GM2
    public bool[] boolStates;
    public int[] intStates;

    public SaveData()
    {
        //DeckScript
        cards = DeckScript.cards;
        activeCards = DeckScript.activeCards;
        discardCards = DeckScript.discardCards;
        hand0 = DeckScript.hand0;
        hand1 = DeckScript.hand1;
        hand2 = DeckScript.hand2;
        hand3 = DeckScript.hand3;
        hand4 = DeckScript.hand4;
        hand5 = DeckScript.hand5;
        regulars = DeckScript.regulars;
        spacesGM = DeckScript.spacesGM;

        //GM1
        player = GM1.player;
        CurrentCard = GM1.CurrentCard;
        turn = GM1.player;
        phase = GM1.phase;
        segment = GM1.segment;
        englishSpaces = GM1.englishSpaces;
        protestantSpaces = GM1.protestantSpaces;
        rulers = GM1.rulers;
        cardTracks = GM1.cardTracks;
        VPs = GM1.VPs;
        translations = GM1.translations;
        excommunicated = GM1.excommunicated;
        StPeters = GM1.StPeters;
        religiousInfluence = GM1.religiousInfluence;
        //powerObjects = GM1.powerObjects;
        diplomacyState = GM1.diplomacyState;
        
        bonusVPs = GM1.bonusVPs;
        piracyC = GM1.piracyC;
        chateauxC = GM1.chateauxC;
        toDo = GM1.toDo;

        //GM2
        boolStates = GM2.boolStates;
        intStates = GM2.intStates;
    }

    public void loadData(SaveData data)
    {
        //DeckScript
        DeckScript.cards = data.cards;
        DeckScript.activeCards = data.activeCards;
        DeckScript.discardCards = data.discardCards;
        DeckScript.hand0 = data.hand0;
        DeckScript.hand1 = data.hand1;
        DeckScript.hand2 = data.hand2;
        DeckScript.hand3 = data.hand3;
        DeckScript.hand4 = data.hand4;
        DeckScript.hand5 = data.hand5;
        DeckScript.regulars = data.regulars;
        DeckScript.spacesGM = data.spacesGM;

        //GM1
        GM1.player = data.player;
        GM1.CurrentCard = data.CurrentCard;
        GM1.turn = data.player;
        GM1.phase = data.phase;
        GM1.segment = data.segment;
        GM1.englishSpaces = data.englishSpaces;
        GM1.protestantSpaces = data.protestantSpaces;
        GM1.rulers = data.rulers;
        GM1.cardTracks = data.cardTracks;
        GM1.VPs = data.VPs;
        GM1.translations = data.translations;
        GM1.excommunicated = data.excommunicated;
        GM1.StPeters = data.StPeters;
        GM1.religiousInfluence = data.religiousInfluence;
        GM1.diplomacyState = data.diplomacyState;

        GM1.bonusVPs = data.bonusVPs;
        GM1.piracyC = data.piracyC;
        GM1.chateauxC = data.chateauxC;
        GM1.toDo = data.toDo;

        //GM2
        GM2.boolStates = data.boolStates;
        GM2.intStates = data.intStates;

        GM2.onMoveHome25();
        GM2.onPlayerChange();
        GM2.onVP();

        NextButton nextButton = GameObject.Find("Next").GetComponent("NextButton") as NextButton;
        nextButton.buttonCallBack();
        if (nextButton.btn.interactable == false)
        {
            nextButton.btn.interactable = true;
        }

        ControlMarkerDisplay controlMarkerDisplay = GameObject.Find("SpacesDisplay").GetComponent("ControlMarkerDisplay") as ControlMarkerDisplay;
        controlMarkerDisplay.initSpaces();
        for (int i=0; i<134; i++)
        {
            GM2.onChangeReg(i, DeckScript.spacesGM.ElementAt(i).regularPower);
            GM2.onChangeMerc(i, DeckScript.spacesGM.ElementAt(i).regularPower);
            GM2.onChangeCav(i, DeckScript.spacesGM.ElementAt(i).regularPower);
            GM2.onChangeSquadron(i, DeckScript.spacesGM.ElementAt(i).controlPower);
            
        }

        LeaderScript leaderScript = GameObject.Find("LeaderDisplay").GetComponent("LeaderScript") as LeaderScript;
        leaderScript.initLeaders();

    }
}
