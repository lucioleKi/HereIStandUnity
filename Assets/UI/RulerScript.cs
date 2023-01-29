using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RulerScript : MonoBehaviour
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

        GM2.onChangeRuler += changeRuler;

    }

    void OnDisable()
    {

        GM2.onChangeRuler -= changeRuler;

    }

    void changeRuler(int power, int index)
    {
        GM2.resetPower();
        if (gameObject.transform.Find("ruler_" + (power).ToString()) == null)
        {
            GameObject newObject = new GameObject("ruler_" + (power).ToString(), typeof(RectTransform), typeof(Image), typeof(CanvasGroup));
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(874+960, -377+540);
            newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Mandatory/HIS-0" + index.ToString());
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(113.4f, 159);
            newObject.transform.SetParent(gameObject.transform);
            newObject.AddComponent<RulerDisplay>();
            if (GM1.player != power)
            {
                newObject.GetComponent<CanvasGroup>().alpha = 0;
                newObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                newObject.GetComponent<CanvasGroup>().interactable = false;
            }
        }
        else
        {
            GameObject tempObject = GameObject.Find("ruler_" + (index).ToString());
            tempObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Mandatory/0" + index.ToString());
        }
        
    }

    
}
