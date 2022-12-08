using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static EnumSpaceScript;
using static DeckScript;
using static GM2;
using static GraphUtils;

public class HighlightScript : MonoBehaviour, IPointerClickHandler
{
    List<int> highlights;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        onHighlight += highlight;
        onHighlightRectangles += highlightCP;
        onRemoveHighlight += removeHighlight;
    }

    void OnDisable()
    {
        onHighlight -= highlight;
        onHighlightRectangles -= highlightCP;
        onRemoveHighlight -= removeHighlight;
    }

    void highlight(List<int> highlightSpaces)
    {
        GM2.resetMap();
        //GameObject.Find("Map1").transform.position = new Vector2(0, 0);
        for (int i = 0; i < highlightSpaces.Count; i++)
        {
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/circle"), new Vector3(0, 0, 0), Quaternion.identity);
            tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(spaces.ElementAt(highlightSpaces.ElementAt(i)).posX + 960, spaces.ElementAt(highlightSpaces.ElementAt(i)).posY + 540);
            tempObject.transform.SetParent(GameObject.Find("HighlightDisplay").transform);
            tempObject.name = "highlight_" + highlightSpaces.ElementAt(i).ToString();
        }
        highlights = highlightSpaces;
    }

    void highlightCP(int currentCP)
    {
        GM2.resetPower();

        GameObject tempObject;
        switch (GM1.player)
        {
            case 0:
                for (int i = 0; i < GM1.status0.CPcost.Length; i++)
                {
                    if (currentCP >= GM1.status0.CPcost[i])
                    {
                        //check if there are units that can move over a pass
                        if (findPassFormation(0).Count() == 0&&i==1)
                        {
                            continue;
                        }
                        //check if piracy is possible
                        if (!GM2.boolStates[3]&&(i==4||i==8))
                        {
                            continue;
                        }
                        //check if there are unfortified space
                        if (findUnfortified(0).Count() == 0 && i == 3)
                        {
                            continue;
                        }
                        if (i < 5)
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(637 + 960, -336 - 10 * i + 540, 0), Quaternion.identity);
                        }
                        else
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(745 + 960, -336 - 10 * (i-5) + 540, 0), Quaternion.identity);

                        }
                        tempObject.transform.SetParent(GameObject.Find("HighlightCPDisplay").transform);
                        //tempObject.AddComponent<HighlightCPScript>();
                        tempObject.name = "highlightRect_" + i.ToString();
                    }
                }
                break;
            case 1:
                for (int i = 0; i < GM1.status1.CPcost.Length; i++)
                {
                    if (currentCP >= GM1.status1.CPcost[i])
                    {
                        //check if there are units that can move over a pass
                        if (findPassFormation(1).Count() == 0 && i == 1)
                        {
                            continue;
                        }
                        //check if there are unfortified space
                        if (findUnfortified(1).Count() == 0 && i == 7)
                        {
                            continue;
                        }
                        if (i < 6)
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(637 + 960, -336 - 10 * i + 540, 0), Quaternion.identity);
                        }
                        else
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(745 + 960, -336 - 10 * (i-6) + 540, 0), Quaternion.identity);

                        }
                        tempObject.transform.SetParent(GameObject.Find("HighlightCPDisplay").transform);
                        //tempObject.AddComponent<HighlightCPScript>();
                        tempObject.name = "highlightRect_" + i.ToString();
                    }
                }
                break;
            case 2:
                for (int i = 0; i < GM1.status2.CPcost.Length; i++)
                {
                    if (currentCP >= GM1.status2.CPcost[i])
                    {
                        //check if there are units that can move over a pass
                        if (findPassFormation(2).Count() == 0 && i == 1)
                        {
                            continue;
                        }
                        //check if there are unfortified space
                        if (findUnfortified(2).Count() == 0 && i == 7)
                        {
                            continue;
                        }
                        if (i < 6)
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(640 + 960, -334 - 10 * i + 540, 0), Quaternion.identity);
                        }
                        else
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(747 + 960, -336 - 10 * (i-6) + 540, 0), Quaternion.identity);

                        }
                        tempObject.transform.SetParent(GameObject.Find("HighlightCPDisplay").transform);
                        //tempObject.AddComponent<HighlightCPScript>();
                        tempObject.name = "highlightRect_" + i.ToString();
                    }
                }
                break;
            case 3:
                for (int i = 0; i < GM1.status3.CPcost.Length; i++)
                {
                    if (currentCP >= GM1.status3.CPcost[i])
                    {
                        //check if there are units that can move over a pass
                        if (findPassFormation(3).Count() == 0 && i == 1)
                        {
                            continue;
                        }
                        //check if there are unfortified space
                        if (findUnfortified(3).Count() == 0 && i == 7)
                        {
                            continue;
                        }
                        tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(637 + 960, -337 - 9.4f * i + 540, 0), Quaternion.identity);
                        tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(95, 9);
                        tempObject.transform.SetParent(GameObject.Find("HighlightCPDisplay").transform);
                        //tempObject.AddComponent<HighlightCPScript>();
                        tempObject.name = "highlightRect_" + i.ToString();
                    }
                }
                break;
            case 4:
                for (int i = 0; i < GM1.status4.CPcost.Length; i++)
                {
                    if (currentCP >= GM1.status4.CPcost[i])
                    {
                        //check if there are units that can move over a pass
                        if (findPassFormation(4).Count() == 0 && i == 1)
                        {
                            continue;
                        }
                        //check if can build jesuit university
                        if (GM1.turn < 5&&i==10)
                        {
                            continue;
                        }
                        //check if there are unfortified space
                        if (findUnfortified(4).Count() == 0 && i == 7)
                        {
                            continue;
                        }
                        if (i < 6)
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(637 + 960, -349 - 10 * i + 540, 0), Quaternion.identity);
                        }
                        else
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(744 + 960, -349 - 10 * (i-6) + 540, 0), Quaternion.identity);

                        }
                        tempObject.transform.SetParent(GameObject.Find("HighlightCPDisplay").transform);
                        //tempObject.AddComponent<HighlightCPScript>();
                        tempObject.name = "highlightRect_" + i.ToString();
                    }
                }
                break;
            case 5:
                for (int i = 0; i < GM1.status5.CPcost.Length; i++)
                {
                    if (currentCP >= GM1.status5.CPcost[i])
                    {
                        //check if there are units that can move over a pass
                        if (findPassFormation(5).Count() == 0 && i == 1)
                        {
                            continue;
                        }
                        //check if can raise army
                        if (GM1.turn < 2 && i < 6)
                        {
                            continue;
                        }
                        //check if there are unfortified space
                        if (findUnfortified(5).Count() == 0 && i == 5)
                        {
                            continue;
                        }
                        if (i < 4)
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(637 + 960, -338 - 10 * i + 540, 0), Quaternion.identity);
                        }
                        else
                        {
                            tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/rectangle"), new Vector3(744 + 960, -338 - 10 * (i-4) + 540, 0), Quaternion.identity);

                        }
                        tempObject.transform.SetParent(GameObject.Find("HighlightCPDisplay").transform);
                        //tempObject.AddComponent<HighlightCPScript>();
                        tempObject.name = "highlightRect_" + i.ToString();
                    }
                }
                break;
        }
    }

    void removeHighlight() { 
  
        /*for (int i = 0; i < highlightSpaces.Count; i++)
        {

            if (gameObject.transform.Find("highlight_" + highlightSpaces.ElementAt(i).ToString()) != null)
            {

                GameObject tempObject = GameObject.Find("highlight_" + highlightSpaces.ElementAt(i).ToString());
                Destroy(tempObject.gameObject);
            }
        }*/
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }

    

    public void OnPointerClick(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        removeHighlight();
        LayerScript layerScript = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
        layerScript.changeLayer();
        if (eventData.pointerCurrentRaycast.gameObject.name[9] == '_')
        {
            highlightSelected = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(10));
        }
        else
        {
            return;
        }
        
        if (onHighlightSelected != null)
        {
            onHighlightSelected();
        }

    }
}
