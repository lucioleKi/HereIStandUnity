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
        gameObject.transform.Find("Layer6").GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { changeLayer(); });
        gameObject.transform.Find("Layer7").GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { changeLayer(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {

        GM2.onSpaceLayer += highlightSpace;
        GM2.onRegLayer += highlightRegular;
        GM2.onLeaderLayer += highlightLeader;
        GM2.onNoLayer += highlightDark;
        GM2.onMercLayer += highlightMerc;
        GM2.onNavalLayer+=highlightNaval;
}

    void OnDisable()
    {

        GM2.onSpaceLayer -= highlightSpace;
        GM2.onRegLayer -= highlightRegular;
        GM2.onLeaderLayer -= highlightLeader;
        GM2.onNoLayer -= highlightDark;
        GM2.onMercLayer -= highlightMerc;
        GM2.onNavalLayer -= highlightNaval;
    }

    public int getMax()
    {
        int max = 0;
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer1").GetComponent<TMP_InputField>().text))
        {
            if(max< int.Parse(gameObject.transform.Find("Layer1").GetComponent<TMP_InputField>().text))
            {
                max = int.Parse(gameObject.transform.Find("Layer1").GetComponent<TMP_InputField>().text);
            }
        }
        
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer2").GetComponent<TMP_InputField>().text))
        {
            if (max < int.Parse(gameObject.transform.Find("Layer2").GetComponent<TMP_InputField>().text))
            {
                max = int.Parse(gameObject.transform.Find("Layer2").GetComponent<TMP_InputField>().text);
            }
        }
        
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer3").GetComponent<TMP_InputField>().text))
        {
            if (max < int.Parse(gameObject.transform.Find("Layer3").GetComponent<TMP_InputField>().text))
            {
                max = int.Parse(gameObject.transform.Find("Layer3").GetComponent<TMP_InputField>().text);
            }
        }
        
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer4").GetComponent<TMP_InputField>().text))
        {
            if (max < int.Parse(gameObject.transform.Find("Layer4").GetComponent<TMP_InputField>().text))
            {
                max = int.Parse(gameObject.transform.Find("Layer4").GetComponent<TMP_InputField>().text);
            }
        }
        
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer5").GetComponent<TMP_InputField>().text))
        {
            if (max < int.Parse(gameObject.transform.Find("Layer5").GetComponent<TMP_InputField>().text))
            {
                max = int.Parse(gameObject.transform.Find("Layer5").GetComponent<TMP_InputField>().text);
            }
        }
        
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer6").GetComponent<TMP_InputField>().text))
        {
            if (max < int.Parse(gameObject.transform.Find("Layer6").GetComponent<TMP_InputField>().text))
            {
                max = int.Parse(gameObject.transform.Find("Layer6").GetComponent<TMP_InputField>().text);
            }

        }
        
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer7").GetComponent<TMP_InputField>().text))
        {
            if (max < int.Parse(gameObject.transform.Find("Layer7").GetComponent<TMP_InputField>().text))
            {
                max = int.Parse(gameObject.transform.Find("Layer7").GetComponent<TMP_InputField>().text);
            }
        }
        if (max == 0)
        {
            return 7;
        }
        return max;
    }

    public void changeLayer()
    {
        int[] l = new int[7];
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
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer6").GetComponent<TMP_InputField>().text))
        {
            l[5] = int.Parse(gameObject.transform.Find("Layer6").GetComponent<TMP_InputField>().text);
            
        }
        else
        {
            
            l[5] = 6;
        }
        if (!string.IsNullOrEmpty(gameObject.transform.Find("Layer7").GetComponent<TMP_InputField>().text))
        {
            l[6] = int.Parse(gameObject.transform.Find("Layer7").GetComponent<TMP_InputField>().text);
        }
        else
        {
            l[6] = 7;
        }
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("Darken").transform.SetSiblingIndex(3);
        int invis = 0;
        GameObject[] count = new GameObject[7];
        count[0] = GameObject.Find("SpacesDisplay");
        count[1] = GameObject.Find("LandUDisplay");
        count[2] = GameObject.Find("MercDisplay");
        count[3] = GameObject.Find("CavDisplay");
        count[4] = GameObject.Find("LeaderDisplay");
        count[5] = GameObject.Find("NavalDisplay");
        count[6] = GameObject.Find("OtherDisplay");
        for(int i = 0; i < count.Length; i++)
        {
            count[i].transform.SetSiblingIndex(l[i]+4);
        }
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
            GameObject.Find("MercDisplay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("MercDisplay").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("MercDisplay").GetComponent<CanvasGroup>().interactable = false;
            invis++;
        }
        else
        {
            GameObject.Find("MercDisplay").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("MercDisplay").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("MercDisplay").GetComponent<CanvasGroup>().interactable = true;
        }
        if (l[3] == 0)
        {
            GameObject.Find("CavDisplay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("CavDisplay").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("CavDisplay").GetComponent<CanvasGroup>().interactable = false;
            invis++;
        }
        else
        {
            GameObject.Find("CavDisplay").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("CavDisplay").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("CavDisplay").GetComponent<CanvasGroup>().interactable = true;
        }
        if (l[4] == 0)
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
        if (l[5] == 0)
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
        
        if (l[6] == 0)
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
        
    }

    public void highlightSpace()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
        GameObject.Find("SpacesDisplay").transform.SetSiblingIndex(13);

    }
    
    public void highlightRegular()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
        GameObject.Find("LandUDisplay").transform.SetSiblingIndex(14);
    }

    public void highlightMerc()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
        GameObject.Find("MercDisplay").transform.SetSiblingIndex(15);
    }


    public void highlightCav()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
        GameObject.Find("CavDisplay").transform.SetSiblingIndex(16);
    }



    public void highlightLeader()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
        GameObject.Find("LeaderDisplay").transform.SetSiblingIndex(17);
    }

    public void highlightNaval()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
        GameObject.Find("NavalDisplay").transform.SetSiblingIndex(18);
    }

    public void highlightOther()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
        GameObject.Find("OtherDisplay").transform.SetSiblingIndex(19);
    }


    public void highlightDark()
    {
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Darken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Darken").transform.SetSiblingIndex(12);
    }

    public void highlightLeaderPower()
    {
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("LeaderDarken").transform.SetSiblingIndex(7);
        GameObject.Find("Leaders" + GM1.player.ToString()).transform.SetSiblingIndex(8);
        GameObject.Find("LeaderDisplay").transform.SetSiblingIndex(17);
    }

    public void resetLeaderPower()
    {
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("LeaderDarken").transform.SetSiblingIndex(0);
        changeLayer();
    }

    public void highlight1Leader(string unit)
    {
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("LeaderDarken").transform.SetSiblingIndex(7);
        GameObject.Find(unit).transform.SetParent(GameObject.Find("LeaderDisplay").transform);
        GameObject.Find(unit).transform.SetSiblingIndex(8);
        GameObject.Find("LeaderDisplay").transform.SetSiblingIndex(17);
    }

    public void reset1Leader()
    {
        foreach (Transform child in GameObject.Find("LeaderDisplay").transform)
        {
            if(child.gameObject.name.Substring(0, 1) == "l")
            {
                GameObject.Destroy(child.gameObject);
            }
            
        }
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("LeaderDarken").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("LeaderDarken").transform.SetSiblingIndex(0);
        changeLayer();
    }
}
