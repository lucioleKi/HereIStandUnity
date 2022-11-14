using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Status3 : StatusObject
{
    public int cardTrack;
    public int chateauxTrack;
    public int[] CardsKey;
    public int[] VPKey;
    public int[] chateaux;

    public override int setVP(int index)
    {
        return VPKey[index]; 
    }

    public int getChateaux(int index)
    {
        return chateaux[index];
    }

    public override void setUp()
    {
        if (cardTrack < 4)
        {
            for (int i = cardTrack + 1; i < 4; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_3"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + i * offsetX[0] + 960, posY[0] + 540);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
            for (int i = 4; i < 11; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_3"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 4) * offsetX[0] + 960, posY[0] + 540 + offsetY[0]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
        else
        {
            for (int i = cardTrack + 1; i < 11; i++)
            {
                GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_3"), new Vector3(0, 0, 0), Quaternion.identity);
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + (i - 4) * offsetX[0] + 960, posY[0] + 540 + offsetY[0]);
                newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
            }
        }
        if (chateauxTrack < 4)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_14"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + chateauxTrack * offsetX[0] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);

        }
        else
        {

            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_14"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (chateauxTrack - 3) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);

        }
    }
}
