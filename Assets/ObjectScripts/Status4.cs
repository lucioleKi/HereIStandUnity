using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void setUp()
    {
        for (int i = cardTrack + 1; i < 7; i++)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_4"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i) * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_15"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + CPIndex * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_16"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[2] + VPIndex * offsetX[2] + 960, posY[2] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        for (int i = 0; i < 7; i++)
        {
            if (excommunicated[i])
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_17"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[3] + i * offsetX[3] + 960, posY[3] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
    }
}
