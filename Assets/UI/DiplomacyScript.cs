using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM2;

public class DiplomacyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        changeDip();
    }

    void OnEnable()
    {

        GM2.onChangeDip += changeDip;

    }

    void OnDisable()
    {

        GM2.onChangeDip -= changeDip;

    }

    void changeDip()
    {
        GM2.resetMap();
        foreach (Transform child in GameObject.Find("DiplomacyDisplay").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i=0; i<6; i++)
        {
            for(int j=0; j < 10; j++)
            {
                if (diplomacyState[i, j] == 1)
                {
                    GameObject tempObject1 = Instantiate((GameObject)Resources.Load("Objects/Diplomacy2/AtWar"), new Vector3(309f + 27.25f*j+932.75f, 361f - 27.2f*i+540f, 0), Quaternion.identity);
                    tempObject1.transform.SetParent(GameObject.Find("DiplomacyDisplay").transform);
                    tempObject1.name = i.ToString()+j.ToString();
                }else if (diplomacyState[i, j] == 2)
                {
                    GameObject tempObject1 = Instantiate((GameObject)Resources.Load("Objects/Diplomacy2/Allied"), new Vector3(309f + 27.25f * j + 932.75f, 361f - 27.2f * i + 540f, 0), Quaternion.identity);
                    tempObject1.transform.SetParent(GameObject.Find("DiplomacyDisplay").transform);
                    tempObject1.name = i.ToString() + j.ToString();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
