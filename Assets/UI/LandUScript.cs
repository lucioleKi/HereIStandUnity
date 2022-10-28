using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        for (int i = 0; i < instanceDeck.spaces.Count(); i++)
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

            //UnityEngine.Debug.Log(instanceDeck.spaces.ElementAt(i).name);
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
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/LandU11/" + tempName), new Vector3(instanceDeck.spaces.ElementAt(i).posX + 970, instanceDeck.spaces.ElementAt(i).posY + 547, 0), Quaternion.identity);
            tempObject.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            tempObject.name = instanceDeck.spaces.ElementAt(i).name + "_0";
            tempObject.SetActive(true);
            GameObject number0 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(instanceDeck.spaces.ElementAt(i).posX + 970 + 22, instanceDeck.spaces.ElementAt(i).posY + 547 - 4, 0), Quaternion.identity);
            number0.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            number0.GetComponent<TextMeshProUGUI>().text = number.ToString();
            number0.name = (i + 1).ToString() + "_0";
        }
    }

    void changeReg(int index, int power)
    {
        
        //if reg marker and # already exist
        if (gameObject.transform.Find(instanceDeck.spaces.ElementAt(index).name + "_0") != null)
        {
            //destroy if changed to 0
            //UnityEngine.Debug.Log(regulars[index]);
            if (regulars[index] == 0)
            {
                GameObject tempObject = GameObject.Find(instanceDeck.spaces.ElementAt(index).name + "_0");
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
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/LandU11/" + tempName), new Vector3(instanceDeck.spaces.ElementAt(index).posX + 970, instanceDeck.spaces.ElementAt(index).posY + 547, 0), Quaternion.identity);
            tempObject.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            tempObject.name = instanceDeck.spaces.ElementAt(index).name + "_0";
            tempObject.SetActive(true);
            GameObject number0 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(instanceDeck.spaces.ElementAt(index).posX + 970 + 22, instanceDeck.spaces.ElementAt(index).posY + 547 - 4, 0), Quaternion.identity);
            number0.transform.SetParent(GameObject.Find("LandUDisplay").transform);
            number0.GetComponent<TextMeshProUGUI>().text = regulars[index].ToString();
            number0.name = (index + 1).ToString() + "_0";
        }
    }
}
