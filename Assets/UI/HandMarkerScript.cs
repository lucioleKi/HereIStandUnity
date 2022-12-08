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
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(810 + i * 36+990, 30);
            newObject.transform.SetParent(gameObject.transform);
        }
    }
}
