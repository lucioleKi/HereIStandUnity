using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static EnumSpaceScript;

public class DeckScript : MonoBehaviour
{
    public static DeckScript instance;
    public List<SpaceObject> spaces;
    public List<CardObject> cards;
    public List<CardObject> activeCards;
    public List<CardObject> discardCards;
    public List<DebaterObject> debaters;
    public List<DebaterObject> activeDebaters;
    public List<CardObject> hand0;
    public List<CardObject> hand1;
    public List<CardObject> hand2;
    public List<CardObject> hand3;
    public List<CardObject> hand4;
    public List<CardObject> hand5;
    public string[] actionName;
    public int[,] action2d;

    public static DeckScript Instance
    {
        get
        {
            if (instance == null)
            {
                UnityEngine.Debug.Log("Deck not initiated.");
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        instance.spaces = importSpaces();
        instance.cards = importCards();
        instance.actionName = getAction1d();
        instance.action2d = getAction2d();
        instance.debaters = importDebaters();
        addActive(1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // read spaces.csv and produce space objects
    List<SpaceObject> importSpaces()
    {
        List<SpaceObject> spaces = new List<SpaceObject>();
        string[,] adjacentArray = new string[134, 6];
        string[,] passArray = new string[134, 2];
        int row = 0;
        using (var reader = new StreamReader("Assets/Input/spaces.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                SpaceObject temp = new SpaceObject();
                temp.name = values[1];
                temp.id = int.Parse(values[0]);
                temp.posX = int.Parse(values[2]);
                temp.posY = int.Parse(values[3]);
                temp.spaceType = (SpaceType)int.Parse(values[4]);
                temp.homePower = (PowerType2)int.Parse(values[5]);
                temp.language = (Language)int.Parse(values[6]);
                temp.matching = values[16];
                for (int j = 7; j <= 12; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        //UnityEngine.Debug.Log(row);
                        //UnityEngine.Debug.Log(values[j]);
                        adjacentArray[row, j - 7] = values[j];
                    }
                    else
                    {
                        adjacentArray[row, j - 7] = "";
                    }
                }
                for (int j = 13; j <= 14; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        passArray[row, j - 13] = values[j];
                    }
                    else
                    {
                        passArray[row, j - 13] = "";
                    }
                }
                spaces.Add(temp);
                row++;
            }
            /*foreach(string item in adjacentArray)
            {
                UnityEngine.Debug.Log(item);
            }*/
            for (int i = 0; i < spaces.Count; i++)
            {
                List<string> tempAdjacent = new List<string>();
                List<string> tempPass = new List<string>();
                for (int j = 0; j < 6; j++)
                {
                    if (adjacentArray[i, j] != "")
                    {
                        //UnityEngine.Debug.Log(adjacentArray[i, j]);
                        tempAdjacent.Add(adjacentArray[i, j]);
                    }
                    else
                    {
                        break;
                    }
                }
                spaces.ElementAt(i).adjacent = tempAdjacent;
                for (int j = 0; j < 2; j++)
                {
                    if (passArray[i, j] != "")
                    {
                        tempPass.Add(passArray[i, j]);
                    }
                    else
                    {
                        break;
                    }
                }
                spaces.ElementAt(i).pass = tempPass;
            }

        }
        return spaces;
    }

    List<CardObject> importCards()
    {
        activeCards = new List<CardObject>();
        discardCards = new List<CardObject>();
        List<CardObject> cards = new List<CardObject>();
        using (var reader = new StreamReader("Assets/Input/cards.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                CardObject temp = new CardObject();
                temp.name = values[3];
                temp.id = int.Parse(values[0]);
                //sprite?
                temp.CP = int.Parse(values[5]);
                temp.cardType = (CardType)int.Parse(values[4]);
                if (temp.cardType == 0)
                {
                    temp.canRandomDraw = false;
                }
                else
                {
                    temp.canRandomDraw = true;
                }
                switch (values[6])
                {
                    case "No":
                        temp.remove = 0;
                        break;
                    case "Yes":
                        temp.remove = 1;
                        break;
                    case "Leader":
                        temp.remove = 2;
                        break;
                    case "Special":
                        temp.remove = 3;
                        break;
                }
                temp.turn = int.Parse(values[7]);
                if (values[10] != null && values[10] != "")
                {
                    temp.options = 2;
                }
                else
                {
                    temp.options = 1;
                }
                temp.matching = values[2];

                cards.Add(temp);
            }
        }
        return cards;
    }

    string[] getAction1d()
    {
        string[] action1 = new string[21];
        using (var reader = new StreamReader("Assets/Input/actions.csv"))
        {
            for (int j = 0; j < 21; j++)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                action1[j] = values[1];
            }

        }
        return action1;
    }

    int[,] getAction2d()
    {
        int[,] action = new int[21, 7];
        using (var reader = new StreamReader("Assets/Input/actions.csv"))
        {
            for (int j = 0; j < 21; j++)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                action[j, 0] = int.Parse(values[0]);
                action[j, 1] = int.Parse(values[2]);
                action[j, 2] = int.Parse(values[3]);
                action[j, 3] = int.Parse(values[4]);
                action[j, 4] = int.Parse(values[5]);
                action[j, 5] = int.Parse(values[6]);
                action[j, 6] = int.Parse(values[7]);
            }

        }
        return action;
    }

    void addActive(int turn)
    {
        foreach(var card in cards)
        {
            if(card.turn <= turn&&card.turn!=0)
            {
                activeCards.Add(card);
            }
        }
        foreach(var debater in debaters)
        {
            if (debater.turn <= turn && debater.turn != 0)
            {
                activeDebaters.Add(debater);
            }
        }
    }

    void Shuffle()
    {
        for(int i=0; i<activeCards.Count; i++)
        {
            CardObject temp = activeCards[i];
            int randomIndex = UnityEngine.Random.Range(i, activeCards.Count);
            activeCards[i] = activeCards[randomIndex];
            activeCards[randomIndex] = temp;
        }
    }

    List<DebaterObject> importDebaters()
    {
        int id = 0;
        List<DebaterObject> debaters = new List<DebaterObject>();
        activeDebaters = new List<DebaterObject>();
        using (var reader = new StreamReader("Assets/Input/debaters.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                DebaterObject temp = new DebaterObject();
                temp.name = values[0];
                temp.id = id;
                temp.value = int.Parse(values[1]);
                temp.type = int.Parse(values[2]);
                temp.turn = int.Parse(values[3]);
                
                temp.language = (Language)int.Parse(values[4]);
                

                debaters.Add(temp);
            }
        }
        return debaters;
    }
}
