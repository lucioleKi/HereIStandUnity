using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Status0 : StatusObject
{
    public int cardTrack;
    public int piracyTrack;
    public int[] CardsKey;
    public int[] VPKey;
    public int[] piracy;

    public override void setVP()
    {
        VP = VPKey[cardTrack];
    }

    public override void setUp() {
        if (cardTrack < 6)
        {
            for (int i = cardTrack + 1; i < 6; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_0"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + i * offsetX[1] + 960, posY[1] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
            for (int i = 6; i < 11; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_0"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (i - 6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }

        }
        else
        {
            for (int i = cardTrack + 1; i < 11; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_0"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (i - 6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
        if (piracyTrack < 5)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_6"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + piracyTrack * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);

        }
        else
        {

            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_6"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (piracyTrack - 6) * offsetX[0] + 960, posY[0] + 540 + offsetY[0]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);

        }
    }
}
