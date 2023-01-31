using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static GM1;
using static GM2;

public class HandMarkerScript : MonoBehaviour
{
    int player;
    public List<string> bonus0;
    public List<string> bonus1;
    public List<string> bonus2;
    public List<string> bonus3;
    public List<string> bonus4;
    public List<string> bonus5;
    public List<int> canRansom;
    public List<int> ransomedPower;
    public List<int> ransomedLeader;
    public List<int> excom;
    // Start is called before the first frame update
    void Start()
    {
        showHandMarker();
        bonus0 = new List<string>();
        bonus1 = new List<string>();
        bonus2 = new List<string>();
        bonus3 = new List<string>();
        bonus4 = new List<string>();
        bonus5 = new List<string>();
        //bonus1.Add("Sprites/jpg/negative1Card");
        canRansom = new List<int>();
        ransomedPower = new List<int>();
        ransomedLeader = new List<int>();
        excom = new List<int>();
    }

    void OnEnable()
    {
        onVP += showHandMarker;
        onPlayerChange += showHandMarker;
    }

    void OnDisable()
    {
        onVP -= showHandMarker;
        onPlayerChange -= showHandMarker;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void showHandMarker()
    {
        GM2.resetPower();
        player = GM1.player;

        foreach (Transform child in GameObject.Find("HandMarkerDisplay").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        //GameObject newObject = null;
        switch (player)
        {
            case 0:
                status0.setUp();
                break;
            case 1:
                status1.setUp();
                break;
            case 2:
                status2.setUp();
                break;
            case 3:
                status3.setUp();
                break;
            case 4:
                status4.setUp();
                break;
            case 5:
                status5.setUp();
                break;
        }
        putBonus();
    }

    void putBonus()
    {
        GM2.resetPower();
        List<string> names = new List<string>();
        switch (player)
        {
            case 0:
                names = bonus0;
                break;
            case 1:
                names = bonus1;
                break;
            case 2:
                names = bonus2;
                break;
            case 3:
                names = bonus3;
                break;
            case 4:
                names = bonus4;
                break;
            case 5:
                names = bonus5;
                break;
        }
        for (int i = 0; i < names.Count(); i++)
        {
            GameObject newObject = new GameObject("bonus_"+i.ToString(), typeof(RectTransform), typeof(Image));
            newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(names.ElementAt(i));
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(27, 27);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(810 + i * 36+990, 45);
            newObject.transform.SetParent(gameObject.transform);
        }
    }

    public void leadersCaptured()
    {
        canRansom.Clear();
        ransomedLeader.Clear();
        ransomedPower.Clear();
        foreach(string s in bonus0)
        {
            if (s.Contains("Sprites/jpg/Leader/"))
            {
                canRansom.Add(0);
                ransomedLeader.Add(int.Parse(s.Substring(19)));
                ransomedPower.Add(DeckScript.leaders.ElementAt(ransomedLeader.Last()).power);
            }
        }
        foreach (string s in bonus1)
        {
            if (s.Contains("Sprites/jpg/Leader/"))
            {
                canRansom.Add(1);
                ransomedLeader.Add(int.Parse(s.Substring(19)));
                ransomedPower.Add(DeckScript.leaders.ElementAt(ransomedLeader.Last()).power);
            }
        }
        foreach (string s in bonus2)
        {
            if (s.Contains("Sprites/jpg/Leader/"))
            {
                canRansom.Add(2);
                ransomedLeader.Add(int.Parse(s.Substring(19)));
                ransomedPower.Add(DeckScript.leaders.ElementAt(ransomedLeader.Last()).power);
            }
        }
        foreach (string s in bonus3)
        {
            if (s.Contains("Sprites/jpg/Leader/"))
            {
                canRansom.Add(3);
                ransomedLeader.Add(int.Parse(s.Substring(19)));
                ransomedPower.Add(DeckScript.leaders.ElementAt(ransomedLeader.Last()).power);
            }
        }
        foreach (string s in bonus4)
        {
            if (s.Contains("Sprites/jpg/Leader/"))
            {
                canRansom.Add(4);
                ransomedLeader.Add(int.Parse(s.Substring(19)));
                ransomedPower.Add(DeckScript.leaders.ElementAt(ransomedLeader.Last()).power);
            }
        }
        foreach (string s in bonus5)
        {
            if (s.Contains("Sprites/jpg/Leader/"))
            {
                canRansom.Add(5);
                ransomedLeader.Add(int.Parse(s.Substring(19)));
                ransomedPower.Add(DeckScript.leaders.ElementAt(ransomedLeader.Last()).power);
            }
        }
        
    }

    public void canRemoveExcom()
    {
        excom.Clear();
        
        foreach (string s in bonus1)
        {
            if (s.Contains("Sprites/jpg/negative1Card") && GM2.intStates[12]!=1)
            {
                excom.Add(1);
            }
        }
        foreach (string s in bonus2)
        {
            if (s.Contains("Sprites/jpg/negative1Card") && GM2.intStates[12] != 2)
            {
                excom.Add(2);
            }
        }
        foreach (string s in bonus3)
        {
            if (s.Contains("Sprites/jpg/negative1Card") && GM2.intStates[12] != 3)
            {
                excom.Add(3);
            }
        }
    }
}
