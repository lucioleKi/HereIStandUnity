using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnrestMarker : MonoBehaviour
{
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

        GM2.onChangeUnrest += changeUnrest;

    }

    void OnDisable()
    {

        GM2.onChangeUnrest -= changeUnrest;

    }

    void changeUnrest(int index)
    {
        //0-index
        GM2.resetMap();
        SpaceGM temp = DeckScript.spacesGM.ElementAt(index);
        if (temp.unrest)
        {
            GameObject tempObject = new GameObject("unrest" + index.ToString(), typeof(RectTransform), typeof(Image));
            tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Unrest");
            tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(DeckScript.spaces.ElementAt(index).posX + 970, DeckScript.spaces.ElementAt(index).posY + 547, 0);
            tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            tempObject.transform.SetParent(gameObject.transform);
        }
        else
        {
            GameObject tempObject = GameObject.Find("unrest" + index.ToString());
            Destroy(tempObject.gameObject);
        }
    }
}
