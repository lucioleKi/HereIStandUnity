using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class LayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Layer1").GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { changeLayer(); });
        gameObject.transform.Find("Layer2").GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { changeLayer(); });
        gameObject.transform.Find("Layer3").GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { changeLayer(); });
        gameObject.transform.Find("Layer4").GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { changeLayer(); });
        gameObject.transform.Find("Layer5").GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { changeLayer(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    

    void changeLayer()
    {
        int[] l = new int[5];
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer1").GetComponent<TMP_InputField>().text))
        {
            l[0] = int.Parse(gameObject.transform.Find("Layer1").GetComponent<TMP_InputField>().text);
        }
        else
        {
            l[0] = 1;
        }
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer2").GetComponent<TMP_InputField>().text))
        {
            l[1] = int.Parse(gameObject.transform.Find("Layer2").GetComponent<TMP_InputField>().text);
        }
        else { 
            l[1] = 2;
        }
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer3").GetComponent<TMP_InputField>().text))
        {
            l[2] = int.Parse(gameObject.transform.Find("Layer3").GetComponent<TMP_InputField>().text);
        }
        else
        {
            l[2] = 3;
        }
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer4").GetComponent<TMP_InputField>().text))
        {
            l[3] = int.Parse(gameObject.transform.Find("Layer4").GetComponent<TMP_InputField>().text);
        }
        else
        {
            l[3] = 4;
        }
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer5").GetComponent<TMP_InputField>().text))
        {
            l[4] = int.Parse(gameObject.transform.Find("Layer5").GetComponent<TMP_InputField>().text);
        }
        else
        {
            l[4] = 5;
        }
        
        int invis = 0;
        if (l[0] == 0)
        {
            GameObject.Find("SpacesDisplay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("SpacesDisplay").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("SpacesDisplay").GetComponent<CanvasGroup>().interactable = false;
            invis++;
        }
        else
        {
            GameObject.Find("SpacesDisplay").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("SpacesDisplay").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("SpacesDisplay").GetComponent<CanvasGroup>().interactable = true;
        }
        if (l[1] == 0)
        {
            GameObject.Find("LandUDisplay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("LandUDisplay").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("LandUDisplay").GetComponent<CanvasGroup>().interactable = false;
            invis++;
        }
        else
        {
            GameObject.Find("LandUDisplay").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("LandUDisplay").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("LandUDisplay").GetComponent<CanvasGroup>().interactable = true;
        }
        if (l[2] == 0)
        {
            GameObject.Find("NavalDisplay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("NavalDisplay").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("NavalDisplay").GetComponent<CanvasGroup>().interactable = false;
            invis++;
        }
        else
        {
            GameObject.Find("NavalDisplay").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("NavalDisplay").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("NavalDisplay").GetComponent<CanvasGroup>().interactable = true;
        }
        if (l[3] == 0)
        {
            GameObject.Find("LeaderDisplay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("LeaderDisplay").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("LeaderDisplay").GetComponent<CanvasGroup>().interactable = false;
            invis++;
        }
        else
        {
            GameObject.Find("LeaderDisplay").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("LeaderDisplay").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("LeaderDisplay").GetComponent<CanvasGroup>().interactable = true;
        }
        if (l[4] == 0)
        {
            GameObject.Find("OtherDisplay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("OtherDisplay").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("OtherDisplay").GetComponent<CanvasGroup>().interactable = false;
            invis++;
        }
        else
        {
            GameObject.Find("OtherDisplay").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("OtherDisplay").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("OtherDisplay").GetComponent<CanvasGroup>().interactable = true;
        }
        if (l.Distinct().Count()!=5)
        {
            return;
        }
        
        
    }
}
