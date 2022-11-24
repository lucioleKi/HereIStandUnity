using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LayerTextScript : MonoBehaviour, IPointerClickHandler
{
    //bool clicked = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LayerScript layerScript = GameObject.Find("Layers").GetComponent("LayerScript") as LayerScript;
        //UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            switch (int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(12, 1)))
            {
                case 0:
                    GM2.onSpaceLayer();
                    break;
                case 1:
                    GM2.onRegLayer();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    GM2.onLeaderLayer();
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }
        else
        {
            int max = layerScript.getMax();
            switch (int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(12, 1)))
            {
                case 0:
                    GameObject.Find("Layer1").GetComponent<TMP_InputField>().text = (max + 5).ToString();
                    GameObject.Find("SpacesDisplay").transform.SetSiblingIndex(max + 5);
                    break;
                case 1:
                    GameObject.Find("Layer2").GetComponent<TMP_InputField>().text = (max + 5).ToString();
                    GameObject.Find("LandUDisplay").transform.SetSiblingIndex(max + 5);
                    break;
                case 2:
                    GameObject.Find("Layer3").GetComponent<TMP_InputField>().text = (max + 5).ToString();
                    GameObject.Find("MercDisplay").transform.SetSiblingIndex(max + 5);
                    break;
                case 3:
                    GameObject.Find("Layer4").GetComponent<TMP_InputField>().text = (max + 5).ToString();
                    GameObject.Find("CavDisplay").transform.SetSiblingIndex(max + 5);
                    break;
                case 4:
                    GameObject.Find("Layer5").GetComponent<TMP_InputField>().text = (max + 5).ToString();
                    GameObject.Find("LeaderDisplay").transform.SetSiblingIndex(max + 5);
                    break;
                case 5:
                    GameObject.Find("Layer6").GetComponent<TMP_InputField>().text = (max + 5).ToString();
                    GameObject.Find("NavalDisplay").transform.SetSiblingIndex(max + 5);
                    break;
                case 6:
                    GameObject.Find("Layer7").GetComponent<TMP_InputField>().text = (max + 5).ToString();
                    GameObject.Find("OtherDisplay").transform.SetSiblingIndex(max + 5);
                    break;
            }
            //clicked = false;
        }
        /*else
        {
            GameObject.Find("Layer1").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("Layer2").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("Layer3").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("Layer4").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("Layer5").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("Layer6").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("Layer7").GetComponent<TMP_InputField>().text = "";
            layerScript.changeLayer();
            clicked = true;
        }*/



        //leaderSelected = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(7));

    }
}
