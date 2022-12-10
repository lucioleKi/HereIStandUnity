using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static GM1;
using static DeckScript;

public class ContainerScript : MonoBehaviour
{
    public int playerIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {

        //GM2.onPlayerChange += showHands;

        //chooseCard();
    }

    void OnDisable()
    {

        //GM2.onPlayerChange -= showHands;

    }

    public void showHands()
    {
        //hands.Clear();

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
        UnityEngine.Debug.Log(temp.Count());
        for (int i = 0; i < temp.Count; i++)
        {

            //UnityEngine.Debug.Log(temp.ElementAt(i).id);
            int x = i % 4;
            int y = i / 4;
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/EmptyCard"), new Vector3(400 + 250 * x, 790 - y * 280, 0), Quaternion.identity);
            newObject.tag = player.ToString()+i.ToString();


            if (temp.ElementAt(i).id < 10)
            {
                newObject.name = "HIS-00" + temp.ElementAt(i).id.ToString();
                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/HIS-00" + temp.ElementAt(i).id.ToString());
            }
            else if (temp.ElementAt(i).id < 100)
            {
                newObject.name = "HIS-0" + temp.ElementAt(i).id.ToString();
                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/HIS-0" + temp.ElementAt(i).id.ToString());
            }
            else
            {
                newObject.name = "HIS-" + temp.ElementAt(i).id.ToString();
                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/HIS-" + temp.ElementAt(i).id.ToString());
            }
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(189, 265);


            newObject.transform.SetParent(GameObject.Find("CardContainer").transform);

        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
