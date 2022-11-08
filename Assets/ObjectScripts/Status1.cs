using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Status1 : StatusObject
{
    public int cardTrack;
    public int[] CardsKey;
    public int[] VPKey;

    public override void setUp()
    {
        if (cardTrack < 5)
        {
            for (int i = cardTrack + 1; i < 5; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_1"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + i * offsetX[0] + 960, posY[0] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
            for (int i = 5; i < 10; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_1"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 5) * offsetX[0] + 960, posY[0] + 540 + offsetY[0]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
            for (int i = 10; i < 14; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_1"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 10) * offsetX[0] + 960, posY[0] + 540 +offsetY[0] * 2);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
        else if (cardTrack < 10)
        {
            for (int i = cardTrack + 1; i < 10; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_1"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 5) * offsetX[0] + 960, posY[0] + 540 + offsetY[0]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
            for (int i = 10; i < 14; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_1"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 10) * offsetX[0] + 960, posY[0] + 540 + offsetY[0] * 2);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
        else
        {
            for (int i = cardTrack + 1; i < 14; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_1"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 10) * offsetX[0] + 960, posY[0] + 540 + offsetY[0] * 2);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
    }
}
