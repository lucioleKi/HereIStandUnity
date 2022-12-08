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


public class MercScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {

        GM2.onChangeMerc += changeMerc;

    }

    void OnDisable()
    {

        GM2.onChangeMerc -= changeMerc;

    }

    void changeMerc(int index, int power)
    {
        GM2.resetMap();
        SpaceGM temp = spacesGM.ElementAt(index);


        if (gameObject.transform.Find(spaces.ElementAt(index).name + "_2") != null)
        {
            //destroy if changed to 0
            //UnityEngine.Debug.Log(regulars[index]);
            if (temp.merc == 0 )
            {
                GameObject tempObject = GameObject.Find(spaces.ElementAt(index).name + "_2");
                Destroy(tempObject.gameObject);
                GameObject number = GameObject.Find((index + 1).ToString() + "_2");
                Destroy(number.gameObject);
            }
            else
            //update if not 0
            {
                GameObject tempObject = GameObject.Find((index + 1).ToString() + "_2");
                tempObject.GetComponent<TextMeshProUGUI>().text = temp.merc.ToString();
            }

        }
        //make new marker and #
        else if (temp.merc != 0)
        {

            string tempName = power.ToString();
            GameObject tempObject = new GameObject(spaces.ElementAt(index).name + "_2", typeof(RectTransform), typeof(Image));
            tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/LandMercs/"+tempName);
            tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(index).posX + 970, spaces.ElementAt(index).posY + 547, 0);
            tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(34, 34);
            tempObject.transform.SetParent(gameObject.transform);
            GameObject number0 = Instantiate((GameObject)Resources.Load("Objects/Number"), new Vector3(spaces.ElementAt(index).posX + 970 + 22, spaces.ElementAt(index).posY + 547 - 4, 0), Quaternion.identity);
            number0.transform.SetParent(gameObject.transform);
            number0.GetComponent<TextMeshProUGUI>().text = temp.merc.ToString();
            number0.name = (index + 1).ToString() + "_2";
        }
    }
}
