using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnScript : MonoBehaviour
{
    int displayTurn;
    public List<string> turnMarker;
    // Start is called before the first frame update
    void Start()
    {
        
        displayTurn = GM1.turn;
        turnMarker = new List<string>();
        turnPosition();
    }

    void OnEnable()
    {

        //GM2.onChangePhase += turnPosition;

    }

    void OnDisable()
    {

        //GM2.onChangePhase -= turnPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    void turnPosition()
    {
        GM2.resetMap();
        displayTurn = GM1.turn;
        RectTransform pos = gameObject.GetComponent<RectTransform>();
        
        pos.localPosition = new Vector2(195 + (displayTurn-1) * 40.375f, -485);
        
    }

    public void putMarker()
    {
        GM2.resetMap();
        for (int i = 0; i < turnMarker.Count(); i++)
        {
            GameObject newObject = new GameObject("turnMarker_" + i.ToString(), typeof(RectTransform), typeof(Image));
            newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(turnMarker.ElementAt(i));
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(27, 27);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(195 + (displayTurn - 1) * 40.375f, -485-30*i);
            newObject.transform.SetParent(gameObject.transform);
        }
    }
}
