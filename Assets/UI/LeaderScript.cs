using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static EnumSpaceScript;
using static DeckScript;

public class LeaderScript : MonoBehaviour
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

        GM2.onChangeLeader += changeLeader;

    }

    void OnDisable()
    {

        GM2.onChangeLeader -= changeLeader;

    }

    void initUnits(int i)
    {
        SpaceGM temp = spacesGM.ElementAt(i);
            //Resources.Load("Objects/1517/" + (i + 1).ToString()) as CitySetup;
        

            //leader init
            if (temp.leader1 != 0)
            {
                GameObject newObject = new GameObject("leader_" + temp.leader1.ToString(), typeof(RectTransform), typeof(Image));
                if (temp.id == 22)//vienna
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(i).posX + 945 + 103, spaces.ElementAt(i).posY + 543 + 63, 0);
                }
                else if (temp.id == 28)//london
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(i).posX + 945 - 217, spaces.ElementAt(i).posY + 543 + 70, 0);
                }
                else if (temp.id == 42)//paris
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(i).posX + 945 + 39, spaces.ElementAt(i).posY + 543 + 249, 0);
                }
                else
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(i).posX + 945, spaces.ElementAt(i).posY + 543, 0);
                }

                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Leader/" + temp.leader1.ToString());
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
                newObject.transform.SetParent(gameObject.transform);
                newObject.AddComponent<LeaderClick>();
            }
            if (temp.leader2 != 0)
            {
                GameObject newObject = new GameObject("leader_" + temp.leader2.ToString(), typeof(RectTransform), typeof(Image));
                if (temp.id == 28)//london
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(i).posX + 945 - 217, spaces.ElementAt(i).posY + 510 + 70, 0);
                }
                else if (temp.id == 42)//paris
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(i).posX + 945 + 39, spaces.ElementAt(i).posY + 510 + 249, 0);
                }
                else
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(i).posX + 945, spaces.ElementAt(i).posY + 510, 0);
                }

                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Leader/" + temp.leader2.ToString());
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
                newObject.transform.SetParent(gameObject.transform);
                newObject.AddComponent<LeaderClick>();
            }



        
    }

    void changeLeader(int to, int index)
    {

        if(gameObject.transform.Find("leader_" + (index).ToString()) == null)
        {
            //UnityEngine.Debug.Log("cannot find leader_" + (index).ToString());
            //create leader
            GameObject newObject = new GameObject("leader_" + (index).ToString(), typeof(RectTransform), typeof(Image));
            if (to == 21)//vienna
            {
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX + 945 + 103, spaces.ElementAt(to).posY + 543 + 63);
            }
            else if (to == 28)//london
            {
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX + 945 - 217, spaces.ElementAt(to).posY + 543 + 70);
            }
            else if (to == 42)//paris
            {
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX + 945 + 39, spaces.ElementAt(to).posY + 543 + 249);
            }
            else
            {
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX + 945, spaces.ElementAt(to).posY + 543);
            }

            newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Leader/" + index.ToString());
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
            newObject.transform.SetParent(gameObject.transform);
            newObject.AddComponent<LeaderClick>();
        }else if (to != -1)
        {
            GameObject tempObject = GameObject.Find("leader_" + (index).ToString());
            UnityEngine.Debug.Log("leader_" + (index).ToString());
            if (to == 21)//vienna
            {
                tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX + + 103, spaces.ElementAt(to).posY + 33 + 63);
            }
            else if (to == 27)//london
            {
                tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX + - 217, spaces.ElementAt(to).posY + 70);
            }
            else if (to == 41)//paris
            {
                tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX + 39, spaces.ElementAt(to).posY + 249);
            }
            else
            {
                tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(to).posX-15, spaces.ElementAt(to).posY);
            }
            return;
        }
        
        
        //destroy leader
        if (to==-1)
        {
            GameObject tempObject = GameObject.Find("leader_" + (index).ToString());
            Destroy(tempObject.gameObject);
            

        }
        
        
    }
}