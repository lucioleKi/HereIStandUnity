using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static EnumSpaceScript;

public class DeckScript : MonoBehaviour
{
    public static DeckScript instanceDeck;
    public static List<SpaceObject> spaces;
    public static List<CardObject> cards;
    public static List<CardObject> activeCards;
    public static List<CardObject> discardCards;
    public static List<DebaterObject> debaters;
    public static List<DebaterObject> activeDebaters;
    public static List<LeaderObject> leaders;
    public static List<LeaderObject> activeLeaders;
    public static List<ReformerObject> reformers;
    public static List<ReformerObject> activeReformers;
    public static List<CardObject> hand0;
    public static List<CardObject> hand1;
    public static List<CardObject> hand2;
    public static List<CardObject> hand3;
    public static List<CardObject> hand4; 
    public static List<CardObject> hand5;
    public static string[] actionName;
    public static int[,] action2d;

    public static DeckScript InstanceDeck
    {
        get
        {
            if (instanceDeck == null)
            {
                UnityEngine.Debug.Log("Deck not initiated.");
            }
            return instanceDeck;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        instanceDeck = this;
        importSpaces();
        importCards();
        actionName = getAction1d();
        action2d = getAction2d();
        importDebaters();
        importLeaders();
        importReformers();
        hand0 = new List<CardObject>();
        hand1 = new List<CardObject>();
        hand2 = new List<CardObject>();
        hand3 = new List<CardObject>();
        hand4 = new List<CardObject>();
        hand5 = new List<CardObject>();

        //luther's 95 theses
        hand5.Add(cards.ElementAt(7));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // read spaces.csv and produce space objects
    void importSpaces()
    {
        spaces = new List<SpaceObject>();
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
                temp.adjacent = new List<int>();
                temp.pass = new List<int>();
                for (int j = 7; j <= 12; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        //UnityEngine.Debug.Log(row);
                        //UnityEngine.Debug.Log(values[j]);
                        temp.adjacent.Add(int.Parse(values[j]));
                    }
                    
                }
                for (int j = 13; j <= 14; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        temp.pass.Add(int.Parse(values[j]));
                    }
                    
                }
                
                spaces.Add(temp);
                row++;
            }
            

        }
    }

    void importCards()
    {
        activeCards = new List<CardObject>();
        discardCards = new List<CardObject>();
        cards = new List<CardObject>();
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

    public void addActive(int turn)
    {
        foreach(var card in cards)
        {
            if(card.turn <= turn&&card.turn!=0&&card.id>7)
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
        foreach(var leader in leaders)
        {
            if(leader.turn <= turn && leader.turn != 0)
            {
                activeLeaders.Add(leader);
            }
        }
    }

    public void Shuffle()
    {
        for(int i=0; i<activeCards.Count; i++)
        {
            CardObject temp = activeCards[i];
            int randomIndex = UnityEngine.Random.Range(i, activeCards.Count);
            activeCards[i] = activeCards[randomIndex];
            activeCards[randomIndex] = temp;
        }
    }

    void importDebaters()
    {
        int id = 0;
        debaters = new List<DebaterObject>();
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
    }

    void importLeaders()
    {
        leaders = new List<LeaderObject>();
        activeLeaders = new List<LeaderObject>();
        using (var reader = new StreamReader("Assets/Input/leaders.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                LeaderObject temp = new LeaderObject();
                temp.id = int.Parse(values[0]);
                temp.name = values[1];
                temp.battle = int.Parse(values[2]);
                temp.command = int.Parse(values[3]);
                temp.type = int.Parse(values[4]);

                temp.matching = values[5];
                temp.turn = int.Parse(values[6]);

                leaders.Add(temp);
            }
        }
    }

    void importReformers()
    {
        reformers = new List<ReformerObject>();
        activeReformers = new List<ReformerObject>();
        reformers.Add(Resources.Load("Objects/Reformer4/Luther1") as ReformerObject);
        reformers.Add(Resources.Load("Objects/Reformer4/Zwingli") as ReformerObject);
        reformers.Add(Resources.Load("Objects/Reformer4/Calvin") as ReformerObject);
        reformers.Add(Resources.Load("Objects/Reformer4/Cranmer") as ReformerObject);
    }
}
