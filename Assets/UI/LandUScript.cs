using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM2;
using System.ComponentModel;

public class LandUScript : MonoBehaviour
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
        
        GM2.onChangeReg += changeReg;

    }

    void OnDisable()
    {
        
        GM2.onChangeReg -= changeReg;

    }

    void initUnits(int i)
    {
        CitySetup temp = Resources.Load("Objects/1517/" + (i + 1).ToString()) as CitySetup;
        if (temp != null && temp.regular != 0)
        {

            //UnityEngine.Debug.Log(spaces.ElementAt(i).name);
            int number = temp.regular;
            if (i < 6)
            {
                switch (i)
                {
                    case 0:
                        GameObject tempObject1 = Instantiate((GameObject)Resources.Load("Objects/LandU11/5"), new Vector3(86 + 960, 297 + 540, 0), Quaternion.identity);
                        tempObject1.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        tempObject1.name = "135";
                        GameObject number1 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(86 + 960 + 22, 297 + 540 - 4, 0), Quaternion.identity);
                        number1.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        number1.GetComponent<TextMeshProUGUI>().text = number.ToString();
                        number1.name = "135_0";

                        break;
                    case 1:
                        GameObject tempObject2 = Instantiate((GameObject)Resources.Load("Objects/LandU11/5"), new Vector3(165 + 960, 297 + 540, 0), Quaternion.identity);
                        tempObject2.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        tempObject2.name = "136";
                        GameObject number2 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(165 + 960 + 22, 297 + 540 - 4, 0), Quaternion.identity);
                        number2.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        number2.GetComponent<TextMeshProUGUI>().text = number.ToString();
                        number2.name = "136_0";
                        break;
                    case 2:
                        GameObject tempObject3 = Instantiate((GameObject)Resources.Load("Objects/LandU11/5"), new Vector3(126 + 960, 297 + 540, 0), Quaternion.identity);
                        tempObject3.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        tempObject3.name = "137";
                        GameObject number3 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(126 + 960 + 22, 297 + 540 - 4, 0), Quaternion.identity);
                        number3.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        number3.GetComponent<TextMeshProUGUI>().text = number.ToString();
                        number3.name = "137_0";
                        break;
                    case 3:
                        GameObject tempObject4 = Instantiate((GameObject)Resources.Load("Objects/LandU11/5"), new Vector3(165 + 960, 355 + 540, 0), Quaternion.identity);
                        tempObject4.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        tempObject4.name = "138";
                        GameObject number4 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(165 + 960 + 22, 355 + 540 - 4, 0), Quaternion.identity);
                        number4.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        number4.GetComponent<TextMeshProUGUI>().text = number.ToString();
                        number4.name = "138_0";
                        break;
                    case 4:
                        GameObject tempObject5 = Instantiate((GameObject)Resources.Load("Objects/LandU11/5"), new Vector3(126 + 960, 355 + 540, 0), Quaternion.identity);
                        tempObject5.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        tempObject5.name = "139";
                        GameObject number5 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(126 + 960 + 22, 355 + 540 - 4, 0), Quaternion.identity);
                        number5.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        number5.GetComponent<TextMeshProUGUI>().text = number.ToString();
                        number5.name = "139_0";
                        break;
                    case 5:
                        GameObject tempObject6 = Instantiate((GameObject)Resources.Load("Objects/LandU11/5"), new Vector3(86 + 960, 355 + 540, 0), Quaternion.identity);
                        tempObject6.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        tempObject6.name = "140";
                        GameObject number6 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(86 + 960 + 22, 355 + 540 - 4, 0), Quaternion.identity);
                        number6.transform.SetParent(GameObject.Find("LandUDisplay").transform);
                        number6.GetComponent<TextMeshProUGUI>().text = number.ToString();
                        number6.name = "140_0";
                        break;

                }

                return;
            }

            string tempName = temp.controlPower.ToString();
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/LandU11/" + tempName), new Vector3(spaces.ElementAt(i).posX + 970, spaces.ElementAt(i).posY + 547, 0), Quaternion.identity);
            tempObject.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            tempObject.name = spaces.ElementAt(i).name + "_0";
            tempObject.SetActive(true);
            GameObject number0 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(i).posX + 970 + 22, spaces.ElementAt(i).posY + 547 - 4, 0), Quaternion.identity);
            number0.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            number0.GetComponent<TextMeshProUGUI>().text = number.ToString();
            number0.name = (i + 1).ToString() + "_0";

            //leader init
            if (temp.leader1 != 0)
            {
                GameObject newObject = new GameObject("leader_" + temp.leader1.ToString(), typeof(RectTransform), typeof(Image));
                if (temp.id == 22)//vienna
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 945+103, spaces.ElementAt(i).posY + 543+63, 0);
                }else if (temp.id == 28)//london
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 945-217, spaces.ElementAt(i).posY + 543+70, 0);
                }
                else if(temp.id == 42)//paris
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 945+39, spaces.ElementAt(i).posY + 543+249, 0);
                }
                else
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 945, spaces.ElementAt(i).posY + 543, 0);
                }
                
                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Leader/" + temp.leader1.ToString());
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
                newObject.transform.SetParent(GameObject.Find("LeaderDisplay").transform);
            }
            if(temp.leader2 != 0)
            {
                GameObject newObject = new GameObject("leader_" + temp.leader2.ToString(), typeof(RectTransform), typeof(Image));
                if (temp.id == 28)//london
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 945 - 217, spaces.ElementAt(i).posY + 510 + 70, 0);
                }
                else if (temp.id == 42)//paris
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 945 + 39, spaces.ElementAt(i).posY + 510 + 249, 0);
                }
                else
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 945, spaces.ElementAt(i).posY + 510, 0);
                }
                
                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Leader/" + temp.leader2.ToString());
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
                newObject.transform.SetParent(GameObject.Find("LeaderDisplay").transform);
            }

            //naval init
            if (temp.squadron != 0)
            {
                int number2 = temp.squadron;
                GameObject newObject = new GameObject("squadron_" + temp.controlPower.ToString(), typeof(RectTransform), typeof(Image));
                GameObject number1 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(i).posX + 970f + 14, spaces.ElementAt(i).posY + 532f - 9f, 0), Quaternion.identity);

                if (temp.id == 28)//london
                {
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 955 - 217, spaces.ElementAt(i).posY + 528 + 70, 0);
                    number1.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 955 - 225+37, spaces.ElementAt(i).posY + 523 + 70, 0);
                }

                else
                {
                    
                    newObject.GetComponent<RectTransform>().localPosition = new Vector3(spaces.ElementAt(i).posX + 955f, spaces.ElementAt(i).posY + 528f, 0);
                }
                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/NavalUnits/" + temp.controlPower.ToString());
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(47.5f, 25f);
                newObject.transform.SetParent(GameObject.Find("NavalDisplay").transform);
                number1.transform.SetParent(GameObject.Find("NavalDisplay").transform);
                number1.GetComponent<TextMeshProUGUI>().text = number2.ToString();
                number1.name = (i + 1).ToString() + "_1";
            }
            
        }
    }

    void changeReg(int index, int power)
    {

        //if reg marker and # already exist
        if (index > 133)
        {
            switch (index)
            {
                case 134:
                    GameObject tempObject1 = GameObject.Find("135");
                    Destroy(tempObject1.gameObject);
                    GameObject number1 = GameObject.Find("135_0");
                    Destroy(number1.gameObject);
                    return;
                case 135:
                    GameObject tempObject2 = GameObject.Find("136");
                    Destroy(tempObject2.gameObject);
                    GameObject number2 = GameObject.Find("136_0");
                    Destroy(number2.gameObject);
                    return;
                case 136:
                    GameObject tempObject3 = GameObject.Find("137");
                    Destroy(tempObject3.gameObject);
                    GameObject number3 = GameObject.Find("137_0");
                    Destroy(number3.gameObject);
                    return;
                case 137:
                    GameObject tempObject4 = GameObject.Find("138");
                    Destroy(tempObject4.gameObject);
                    GameObject number4 = GameObject.Find("138_0");
                    Destroy(number4.gameObject);
                    return;
                case 138:
                    GameObject tempObject5 = GameObject.Find("139");
                    Destroy(tempObject5.gameObject);
                    GameObject number5 = GameObject.Find("139_0");
                    Destroy(number5.gameObject);
                    return;
                case 139:
                    GameObject tempObject6 = GameObject.Find("140");
                    Destroy(tempObject6.gameObject);
                    GameObject number6 = GameObject.Find("140_0");
                    Destroy(number6.gameObject);
                    return;

            }
        }

        if (gameObject.transform.Find(spaces.ElementAt(index).name + "_0") != null)
        {
            //destroy if changed to 0
            //UnityEngine.Debug.Log(regulars[index]);
            if (regulars[index] == 0&&index<134)
            {
                GameObject tempObject = GameObject.Find(spaces.ElementAt(index).name + "_0");
                Destroy(tempObject.gameObject);
                GameObject number = GameObject.Find((index + 1).ToString() + "_0");
                Destroy(number.gameObject);
            }
            else
            //update if not 0
            {
                GameObject tempObject = GameObject.Find((index + 1).ToString() + "_0");
                tempObject.GetComponent<TextMeshProUGUI>().text = regulars[index].ToString();
            }

        }
        //make new marker and #
        else
        {
            
            string tempName = power.ToString();
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/LandU11/" + tempName), new Vector3(spaces.ElementAt(index).posX + 970, spaces.ElementAt(index).posY + 547, 0), Quaternion.identity);
            tempObject.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            tempObject.name = spaces.ElementAt(index).name + "_0";
            tempObject.SetActive(true);
            GameObject number0 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(index).posX + 970 + 22, spaces.ElementAt(index).posY + 547 - 4, 0), Quaternion.identity);
            number0.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            number0.GetComponent<TextMeshProUGUI>().text = regulars[index].ToString();
            number0.name = (index + 1).ToString() + "_0";
        }
    }
}
