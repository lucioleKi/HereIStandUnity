using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine;
using static EnumSpaceScript;
using static System.Net.Mime.MediaTypeNames;

public class DeckScript : MonoBehaviour
{
    public static DeckScript instanceDeck;
    public static List<SpaceObject> spaces;
    public static List<SeaObject> seas;
    public static List<CardObject> cardsLib;
    public static List<CardObject> cards;
    public static List<int> choiceCards = new List<int> { 1, 3, 5, 7, 30, 34, 38, 68, 72, 73, 86, 112, 115 };
    public static List<CardObject> activeCards;
    public static List<CardObject> discardCards;
    public static List<DebaterObject> debaters;
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


    public static int[] regulars;
    public static int[] regularsPower;
    public static List<SpaceGM> spacesGM;
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
        importSeas();
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

        hand5.Add(cards.ElementAt(7));
        cards.RemoveAt(7);

        addActive(1);

        regulars = new int[134 + 6];
        regularsPower = new int[134];
        Array.Clear(regulars, 0, 134 + 6);

        spacesGM = new List<SpaceGM>();
        for (int i = 1; i <= 134; i++)
        {
            CitySetup temp = Resources.Load("Objects/1517/" + i.ToString()) as CitySetup;
            if (temp != null)
            {
                SpaceGM temp1 = new SpaceGM(temp);
                spacesGM.Add(temp1);
                regulars[i - 1] = temp.regular;
                regularsPower[i - 1] = temp.controlPower;
                
            }
            else
            {
                SpaceGM temp1 = new SpaceGM();
                temp1.name = spaces.ElementAt(i - 1).name;
                temp1.id = i;
                temp1.controlPower = (int)spaces.ElementAt(i - 1).homePower;
                temp1.regularPower = (int)spaces.ElementAt(i - 1).homePower;
                temp1.mercPower = (int)spaces.ElementAt(i - 1).homePower;
                temp1.squadronPower = (int)spaces.ElementAt(i - 1).homePower;
                regularsPower[i - 1] = (int)spaces.ElementAt(i - 1).homePower;
                spacesGM.Add(temp1);
            }
        }
        
        regulars[134] = 2;
        regulars[135] = 1;
        regulars[136] = 1;
        regulars[137] = 1;
        regulars[138] = 1;
        regulars[139] = 2;
        
        //luther's 95 theses
        
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

    void importSeas()
    {
        seas = new List<SeaObject>();
        int row = 0;
        using (var reader = new StreamReader("Assets/Input/seas.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                SeaObject temp = new SeaObject();
                temp.name = values[1];
                temp.id = int.Parse(values[0]);
                temp.adjacent = new List<int>();
                temp.ports = new List<int>();
                for (int j = 2; j <= 5; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        temp.adjacent.Add(int.Parse(values[j]));
                    }

                }
                for (int j = 6; j <= 14; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        temp.ports.Add(int.Parse(values[j]));
                    }

                }

                seas.Add(temp);
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
        cardsLib = cards;
    }

    public static void discardById(int player, int id)
    {
        List<CardObject> temp = new List<CardObject>();
        switch (player)
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
        for(int i=0; i<temp.Count(); i++)
        {
            if (temp.ElementAt(i).id==id) { 
                temp.RemoveAt(i);
                discardCards.Add(temp.ElementAt(i));
                break;
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
            if (debater.turn <= turn && debater.turn != 0&&debater.status==(DebaterStatus)0)
            {
                debater.status = (DebaterStatus)1;

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
        int id = 1;
        debaters = new List<DebaterObject>();
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
                temp.status = (DebaterStatus)int.Parse(values[5]);

                debaters.Add(temp);
                id++;
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
