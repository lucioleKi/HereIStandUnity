using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Status5 : StatusObject
{
    
    public int[] translationInit;
    public int[] NewTestamentTrack;
    public int[] FullTrack;
    public int protestantSpaces;
    public int englishSpaces;
    public int[] spacesTrack;

    public override int setVP(int index)
    {
        return spacesTrack[index];
    }

    

    public override void setUp()
    {
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_18"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + GM1.translations[0] * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_19"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + GM1.translations[1] * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (true)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_20"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[0] + GM1.translations[2] * offsetX[0] + 960, posY[0] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (GM1.translations[3] < 6)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_21"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + GM1.translations[3] * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        else
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_21"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (GM1.translations[3] - 6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if (GM1.translations[4] < 6)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_22"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + GM1.translations[4] * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        else
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_22"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (GM1.translations[4] - 6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        if(GM1.translations[5] < 6)
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_23"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + GM1.translations[5] * offsetX[1] + 960, posY[1] + 540);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
        else
        {
            GameObject newObject = Instantiate((GameObject)Resources.Load("Objects/HandMarker/Hand_23"), new Vector3(0, 0, 0), Quaternion.identity);
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX[1] + (GM1.translations[5] - 6) * offsetX[1] + 960, posY[1] + 540 + offsetY[1]);
            newObject.transform.SetParent(GameObject.Find("HandMarkerDisplay").transform);
        }
    }
}
