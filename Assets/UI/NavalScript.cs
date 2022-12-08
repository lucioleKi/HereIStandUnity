using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using static DeckScript;

public class NavalScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spaces.Count(); i++)
        {
            initUnits(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {

        GM2.onChangeSquadron += changeSquadron;
        GM2.onChangeCorsair += changeCorsair;
    }

    void OnDisable()
    {

        GM2.onChangeSquadron -= changeSquadron;
        GM2.onChangeCorsair -= changeCorsair;
    }

    void initUnits(int i)
    {
        SpaceGM temp = spacesGM.ElementAt(i);
        if (temp.squadron != 0)
        {
            makeSquadron(i, temp.controlPower);

        }
    }

    void changeSquadron(int index, int power)
    {
        GM2.resetMap();
        if (gameObject.transform.Find("squadron_" + (index + 1).ToString()) != null)
        {
            //destroy if changed to 0
            if (spacesGM.ElementAt(index).squadron == 0)
            {
                GameObject tempObject = GameObject.Find("squadron_" + (index + 1).ToString());
                Destroy(tempObject.gameObject);
                GameObject number = GameObject.Find((index + 1).ToString() + "_1");
                Destroy(number.gameObject);
            }
            else
            //update if not 0
            {
                GameObject tempObject = GameObject.Find((index + 1).ToString() + "_1");
                tempObject.GetComponent<TextMeshProUGUI>().text = spacesGM.ElementAt(index).squadron.ToString();
            }

        }
        //make new marker and #
        else if (spacesGM.ElementAt(index).squadron != 0)
        {
            makeSquadron(index, power);

        }
    }

    void makeSquadron(int index, int power)
    {
        GM2.resetMap();
        GameObject newObject = new GameObject("squadron_" + (index + 1).ToString(), typeof(RectTransform), typeof(Image));
        GameObject number1 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(index).posX + 970f + 14, spaces.ElementAt(index).posY + 532f - 9f, 0), Quaternion.identity);

        if (index == 28)//london
        {
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(index).posX + 955 - 217, spaces.ElementAt(index).posY + 528 + 70);
            number1.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(index).posX + 955 - 225 + 37, spaces.ElementAt(index).posY + 523 + 70);
        }

        else
        {

            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(index).posX + 955f, spaces.ElementAt(index).posY + 528f);
        }
        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NavalUnits/" + power.ToString());
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(47.5f, 25f);
        newObject.transform.SetParent(gameObject.transform);
        number1.transform.SetParent(gameObject.transform);
        number1.GetComponent<TextMeshProUGUI>().text = spacesGM.ElementAt(index).squadron.ToString();
        number1.name = (index + 1).ToString() + "_1";
    }

    void changeCorsair(int index)
    {
        GM2.resetMap();
        if (gameObject.transform.Find("corsair_" + (index + 1).ToString()) != null)
        {
            //destroy if changed to 0
            if (spacesGM.ElementAt(index).corsair == 0)
            {
                GameObject tempObject = GameObject.Find("corsair_" + (index + 1).ToString());
                Destroy(tempObject.gameObject);
                GameObject number = GameObject.Find((index + 1).ToString() + "_2");
                Destroy(number.gameObject);
            }
            else
            //update if not 0
            {
                GameObject tempObject = GameObject.Find((index + 1).ToString() + "_2");
                tempObject.GetComponent<TextMeshProUGUI>().text = spacesGM.ElementAt(index).corsair.ToString();
            }

        }
        //make new marker and #
        else if (spacesGM.ElementAt(index).corsair != 0)
        {
            GameObject newObject = new GameObject("corsair_" + (index + 1).ToString(), typeof(RectTransform), typeof(Image));
            GameObject number1 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(index).posX + 970f + 14, spaces.ElementAt(index).posY + 532f - 9f, 0), Quaternion.identity);

            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(index).posX + 955f, spaces.ElementAt(index).posY + 528f);

            newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NavalUnits/10");
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(47.5f, 25f);
            newObject.transform.SetParent(gameObject.transform);
            number1.transform.SetParent(gameObject.transform);
            number1.GetComponent<TextMeshProUGUI>().text = spacesGM.ElementAt(index).corsair.ToString();
            number1.name = (index + 1).ToString() + "_2";


        }
    }
}
