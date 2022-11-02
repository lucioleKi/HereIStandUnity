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
        for (int i = 0; i < spaces.Count(); i++)
        {
            updateList(i, (int)spaces.ElementAt(i).homePower);
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
        
        
    }

    void OnDisable()
    {
        GM2.onAddSpace -= addControlMarker;
        GM2.onRemoveSpace -= removeControlMarker;
        
        
    }

    void updateList(int i, int power)
    {
        switch (power)
        {
            case 0:
                positions0.Add(new Vector3(spaces.ElementAt(i).posX, spaces.ElementAt(i).posY, 0));
                break;
            case 1:
                positions1.Add(new Vector3(spaces.ElementAt(i).posX, spaces.ElementAt(i).posY, 0));
                break;
            case 2:
                positions2.Add(new Vector3(spaces.ElementAt(i).posX, spaces.ElementAt(i).posY, 0));
                break;
            case 3:
                positions3.Add(new Vector3(spaces.ElementAt(i).posX, spaces.ElementAt(i).posY, 0));
                break;
            case 4:
                positions4.Add(new Vector3(spaces.ElementAt(i).posX, spaces.ElementAt(i).posY, 0));
                break;
            default:
                positions6.Add(new Vector3(spaces.ElementAt(i).posX, spaces.ElementAt(i).posY, 0));
                break;
        }
    }

    void initXY(int i)
    {
        //List<Vector3> positions = new List<Vector3>();


        CitySetup temp = Resources.Load("Objects/1517/" + (i+1).ToString()) as CitySetup;
        if (temp != null&&temp.regular!=0)
        {
            //UnityEngine.Debug.Log(spaces.ElementAt(i - 1).name);
            
            if (temp.controlPower == 5 || temp.controlMarker == 0)
            {
                return;
            }
            string tempName = (temp.controlPower * 4 + temp.controlMarker - 1).ToString() + "_" + temp.controlPower.ToString();
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/ControlMarker21/" + tempName), new Vector3(spaces.ElementAt(i).posX+960, spaces.ElementAt(i).posY+540, 0), Quaternion.identity);
            tempObject.transform.SetParent(GameObject.Find("SpacesDisplay").transform);
            tempObject.name = spaces.ElementAt(i - 1).name;
            tempObject.SetActive(true);
        }
        

    }

    

    void addControlMarker(int index, int power, int marker)
    {

        if (power == 5 || marker == 0)
        {
            return;
        }
        string tempName = (power * 4 + marker - 1).ToString() + "_" + power.ToString();
        GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/ControlMarker21/" + tempName), new Vector3(spaces.ElementAt(index).posX + 960, spaces.ElementAt(index).posY + 540, 0), Quaternion.identity);
        tempObject.transform.SetParent(GameObject.Find("SpacesDisplay").transform);
        tempObject.name = spaces.ElementAt(index).name;
        tempObject.SetActive(true);
        updateList(index, power);
    }

    void removeControlMarker(int index)
    {

        if (gameObject.transform.Find(spaces.ElementAt(index).name) != null)
        {

            GameObject tempObject = GameObject.Find(spaces.ElementAt(index).name);
            Destroy(tempObject.gameObject);
        }
        positions0.Remove(new Vector3(spaces.ElementAt(index).posX, spaces.ElementAt(index).posY, 0));
        positions1.Remove(new Vector3(spaces.ElementAt(index).posX, spaces.ElementAt(index).posY, 0));
        positions2.Remove(new Vector3(spaces.ElementAt(index).posX, spaces.ElementAt(index).posY, 0));
        positions3.Remove(new Vector3(spaces.ElementAt(index).posX, spaces.ElementAt(index).posY, 0));
        positions4.Remove(new Vector3(spaces.ElementAt(index).posX, spaces.ElementAt(index).posY, 0));
        positions6.Remove(new Vector3(spaces.ElementAt(index).posX, spaces.ElementAt(index).posY, 0));
        
        
    }

    
    

    
}
