using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Status2 : StatusObject
{
    public int cardTrack;
    public int[] CardsKey;
    public int[] VPKey;
    public bool[] maritalStatus;

    public override int setVP(int index)
    {
        return VPKey[index];
    }



    public override void setUp()
    {
        if (cardTrack < 5)
        {
            for (int i = cardTrack + 1; i < 5; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_2"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + i * offsetX[0] + 960, posY[0] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
            for (int i = 5; i < 9; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_2"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 5) * offsetX[0] + 960, posY[0] + 540 + offsetY[0]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
        else
        {
            for (int i = cardTrack + 1; i < 9; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_2"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 5) * offsetX[0] + 960, posY[0] + 540 + offsetY[0]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
        if (maritalStatus[0])
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_8"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + 2 * offsetX[1] + 962, posY[1] + 530);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        for (int i = 5; i >= 0; i--)
        {
            if (!maritalStatus[i])
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_" + (i + 8).ToString()), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (i + 1) * offsetX[1] + 960, posY[1] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
            else
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_7"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (i + 1) * offsetX[1] + 960, posY[1] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
    }
}
