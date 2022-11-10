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

    }

    void OnDisable()
    {

        GM2.onChangeSquadron -= changeSquadron;

    }

    void initUnits(int i)
    {
        CitySetup temp = Resources.Load("Objects/1517/" + (i + 1).ToString()) as CitySetup;
        if (temp != null && temp.squadron != 0)
        {

            //UnityEngine.Debug.Log(spaces.ElementAt(i).name);
            int number = temp.regular;




            int number2 = temp.squadron;
            GameObject newObject = new GameObject("squadron_" + temp.controlPower.ToString(), typeof(RectTransform), typeof(Image));
            GameObject number1 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(i).posX + 970f + 14, spaces.ElementAt(i).posY + 532f - 9f, 0), Quaternion.identity);

            if (temp.id == 28)//london
            {
                newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 955 - 217, spaces.ElementAt(i).posY + 528 + 70, 0);
                number1.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 955 - 225 + 37, spaces.ElementAt(i).posY + 523 + 70, 0);
            }

            else
            {

                newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 955f, spaces.ElementAt(i).posY + 528f, 0);
            }
            newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NavalUnits/" + temp.controlPower.ToString());
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(47.5f, 25f);
            newObject.transform.SetParent(gameObject.transform);
            number1.transform.SetParent(gameObject.transform);
            number1.GetComponent<TextMeshProUGUI>().text = number2.ToString();
            number1.name = (i + 1).ToString() + "_1";


        }
    }

    void changeSquadron(int index, int power)
    {
        if (gameObject.transform.Find("squadron_" + (index + 1).ToString()) != null)
        {
            //destroy if changed to 0
            //UnityEngine.Debug.Log(regulars[index]);
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

            string tempName = power.ToString();
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/LandU11/" + tempName), new Vector3(spaces.ElementAt(index).posX + 970, spaces.ElementAt(index).posY + 547, 0), Quaternion.identity);
            tempObject.transform.SetParent(gameObject.transform);
            tempObject.name = "squadron_" + (index + 1).ToString();
            tempObject.SetActive(true);
            GameObject number0 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(index).posX + 970 + 22, spaces.ElementAt(index).posY + 547 - 4, 0), Quaternion.identity);
            number0.transform.SetParent(gameObject.transform);
            number0.GetComponent<TextMeshProUGUI>().text = spacesGM.ElementAt(index).squadron.ToString();
            number0.name = (index + 1).ToString() + "_1";
        }
    }
}
