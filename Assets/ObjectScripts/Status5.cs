using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Status5 : StatusObject
{
    public int NewTestamentIndex0;
    public int NewTestamentIndex1;
    public int NewTestamentIndex2;
    public int FullIndex0;
    public int FullIndex1;
    public int FullIndex2;
    public int[] NewTestamentTrack;
    public int[] FullTrack;

    public override void setUp()
    {
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_18"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + NewTestamentIndex0 * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_19"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + NewTestamentIndex1 * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_20"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + NewTestamentIndex2 * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (FullIndex0 < 6)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_21"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + FullIndex0 * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        else
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_21"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (FullIndex0 - 6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (FullIndex1 < 6)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_22"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + FullIndex1 * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        else
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_22"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (FullIndex1 - 6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if(FullIndex2 < 6)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_23"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + FullIndex2 * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        else
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_23"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (FullIndex2-6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
    }
}
