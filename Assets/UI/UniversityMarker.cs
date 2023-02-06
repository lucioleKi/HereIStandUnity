using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UniversityMarker : MonoBehaviour
{
    public List<int> universities;
    // Start is called before the first frame update
    void Start()
    {
        universities = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {

        GM2.onAddUni += addUni;

    }

    void OnDisable()
    {

        GM2.onAddUni -= addUni;

    }

    void addUni(int index)
    {
        //0-index
        GM2.resetMap();
        SpaceGM temp = DeckScript.spacesGM.ElementAt(index);

        GameObject tempObject = new GameObject("uni" + index.ToString(), typeof(RectTransform), typeof(Image));
        tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Jesuit_Univ");
        tempObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(DeckScript.spaces.ElementAt(index).posX + 970, DeckScript.spaces.ElementAt(index).posY + 547, 0);
        tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
        tempObject.transform.SetParent(gameObject.transform);

    }
}
