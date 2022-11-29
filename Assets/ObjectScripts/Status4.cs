using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GM1;

[CreateAssetMenu]
public class Status4 : StatusObject
{
    public int cardTrack;
    public int CPIndex;
    public int VPIndex;
    public int[] CardsKey;
    public int[] VPKey;
    public int[] CPTrack;
    public int[] VPTrack;
    public bool[] excommunicated;
    public int protestantSpaces;
    public int[] spacesTrack;

    public override int setVP(int index)
    {
        return VPKey[index];
        
    }

    public int getBonus1(int index)
    {
        return VPTrack[index];
    }

    public override void setUp()
    {
        for (int i = cardTracks[4] + 1; i < 7; i++)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_4"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i) * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_15"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + GM1.StPeters[0] * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_16"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[2] + GM1.StPeters[1] * offsetX[2] + 960, posY[2] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        for (int i = 0; i < 7; i++)
        {
            if (GM1.excommunicated[i])
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_17"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[3] + i * offsetX[3] + 960, posY[3] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
    }
}
