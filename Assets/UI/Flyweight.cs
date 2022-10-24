using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using System.ComponentModel;

public class Flyweight : MonoBehaviour
{
    List<SpaceObject> HCM = new List<SpaceObject>();
    List<Vector3> positions0;
    List<Vector3> positions1;
    List<Vector3> positions2;
    List<Vector3> positions3;
    List<Vector3> positions4;
    List<Vector3> positions6;


    // Start is called before the first frame update
    void Start()
    {
        positions0 = new List<Vector3>();
        positions1 = new List<Vector3>();
        positions2 = new List<Vector3>();
        positions3 = new List<Vector3>();
        positions4 = new List<Vector3>();
        positions6 = new List<Vector3>();
        GameObject canvas = GameObject.Find("CanvasBoard");
        for (int i = 0; i < instanceDeck.spaces.Count(); i++)
        {
            switch ((int)instanceDeck.spaces.ElementAt(i).homePower)
            {
                case 0:
                    positions0.Add(new Vector3(instanceDeck.spaces.ElementAt(i).posX, instanceDeck.spaces.ElementAt(i).posY, 0));
                    break;
                case 1:
                    positions1.Add(new Vector3(instanceDeck.spaces.ElementAt(i).posX, instanceDeck.spaces.ElementAt(i).posY, 0));
                    break;
                case 2:
                    positions2.Add(new Vector3(instanceDeck.spaces.ElementAt(i).posX, instanceDeck.spaces.ElementAt(i).posY, 0));
                    break;
                case 3:
                    positions3.Add(new Vector3(instanceDeck.spaces.ElementAt(i).posX, instanceDeck.spaces.ElementAt(i).posY, 0));
                    break;
                case 4:
                    positions4.Add(new Vector3(instanceDeck.spaces.ElementAt(i).posX, instanceDeck.spaces.ElementAt(i).posY, 0));
                    break;
                default:
                    positions6.Add(new Vector3(instanceDeck.spaces.ElementAt(i).posX, instanceDeck.spaces.ElementAt(i).posY, 0));
                    break;
            }
            initXY(i);

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initXY(int i)
    {
        //List<Vector3> positions = new List<Vector3>();


        CitySetup temp = Resources.Load("Objects/1517/" + i.ToString()) as CitySetup;
        if(temp != null)
        {
            //UnityEngine.Debug.Log(instanceDeck.spaces.ElementAt(i-1).name);
            
            string tempName = (temp.controlPower * 4 + temp.controlMarker).ToString() + "_" + temp.controlPower.ToString();
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/ControlMarker21/" + tempName), new Vector3(instanceDeck.spaces.ElementAt(i-1).posX+960, instanceDeck.spaces.ElementAt(i-1).posY+540, 0), Quaternion.identity);
            tempObject.transform.SetParent(GameObject.Find("SpacesDisplay").transform);
            tempObject.name = instanceDeck.spaces.ElementAt(i - 1).name;
            tempObject.SetActive(true);
        }
        

    }
}
