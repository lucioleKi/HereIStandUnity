using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM2;

public class ControlMarkerDisplay : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spaces.Count(); i++)
        {
            initXY(i);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        GM2.onAddSpace += addControlMarker;
        GM2.onRemoveSpace += removeControlMarker;
        GM2.onFlipSpace += flipControlMarker;


    }

    void OnDisable()
    {
        GM2.onAddSpace -= addControlMarker;
        GM2.onRemoveSpace -= removeControlMarker;
        GM2.onFlipSpace -= flipControlMarker;

    }

    public void initSpaces()
    {
        GM2.resetMap();
        foreach (Transform child in gameObject.transform)
        {
            
                GameObject.Destroy(child.gameObject);
            

        }
        for (int i = 0; i < spaces.Count(); i++)
        {

            initXY(i);
        }
    }

    void initXY(int i)
    {
        SpaceGM temp = spacesGM.ElementAt(i);
        if (temp.regular != 0 || temp.controlMarker!=0)
        {
            //UnityEngine.Debug.Log(spaces.ElementAt(i - 1).name);
            
            if (temp.controlPower == 5 || temp.controlMarker == 0)
            {
                return;
            }
            string tempName = (temp.controlPower * 4 + temp.controlMarker - 1).ToString() + "_" + temp.controlPower.ToString();
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/ControlMarker21/" + tempName), new Vector3(spaces.ElementAt(i).posX+960, spaces.ElementAt(i).posY+540, 0), Quaternion.identity);
            tempObject.transform.SetParent(gameObject.transform);
            tempObject.name = spaces.ElementAt(i).name;
            tempObject.SetActive(true);
        }
        

    }

    

    void addControlMarker(int index, int power, int marker)
    {
        GM2.resetMap();
        UnityEngine.Debug.Log(index.ToString() + ", " + power.ToString() + ", " + marker.ToString());
        if (power == 5 || marker == 0)
        {
            return;
        }
        string tempName = (power * 4 + marker - 1).ToString() + "_" + power.ToString();
        GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/ControlMarker21/" + tempName), new Vector3(spaces.ElementAt(index).posX + 960, spaces.ElementAt(index).posY + 540, 0), Quaternion.identity);
        tempObject.transform.SetParent(GameObject.Find("SpacesDisplay").transform);
        tempObject.name = spaces.ElementAt(index).name;
        tempObject.SetActive(true);
        
    }

    void removeControlMarker(int index)
    {
        GM2.resetMap();
        if (gameObject.transform.Find(spaces.ElementAt(index).name) != null)
        {

            GameObject tempObject = GameObject.Find(spaces.ElementAt(index).name);
            Destroy(tempObject.gameObject);
        }
        
    }

    void flipControlMarker(int index, int power, int marker)
    {
        removeControlMarker(index);
        addControlMarker(index, power, marker);
    }
    
    //0-indexing
    void updateUnrest(int index)
    {
        if (spacesGM.ElementAt(index).unrest)
        {
            GameObject newObject = new GameObject("unrest_" + index.ToString(), typeof(RectTransform), typeof(Image));
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(spaces.ElementAt(index).posX + 965, spaces.ElementAt(index).posY + 545, 0);
            newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Unrest");
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
        }
        else
        {
            GameObject tempObject = GameObject.Find("unrest_" + index.ToString());
            Destroy(tempObject.gameObject);
        }
        
    }
    
}
