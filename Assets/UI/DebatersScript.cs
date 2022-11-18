using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static EnumSpaceScript;
using static DeckScript;

public class DebatersScript : MonoBehaviour
{
    int index0;
    int index1;
    // Start is called before the first frame update
    void Start()
    {
        index0 = 0;
        index1 = 0;
        for(int i = 0; i < debaters.Count(); i++)
        {
            initUnits(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {

        

    }

    void OnDisable()
    {

        
    }

    void initUnits(int i)
    {

        DebaterObject temp = debaters.ElementAt(i);
        GameObject tempObject1 = GameObject.Find("debater_" + temp.id.ToString());
        if(tempObject1 == null)
        {
            if (temp.status == (DebaterStatus)1 || temp.status == (DebaterStatus)2)
            {
                GameObject newObject = new GameObject("debater_" + temp.id.ToString(), typeof(RectTransform), typeof(Image));
                newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater");
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(27, 27);
                newObject.transform.SetParent(gameObject.transform);
                newObject.AddComponent<DebaterClick>();

                //debater init
                if (temp.type == 0)
                {

                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(650 + index0 * 27, 370);
                    index0++;
                }
                else
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(595 + index1 % 2 * 27, 283 + index1 / 2 * 27);
                    index1++;
                }
            }
        }
        else if(temp.status != (DebaterStatus)1 && temp.status != (DebaterStatus)2)
        {
            tempObject1 = GameObject.Find("debater_" + temp.id.ToString());
            if(tempObject1 != null)
            {
                Destroy(tempObject1.gameObject);
            }
            
        }
        

        
       

    }

    public void updateDebater()
    {
        index0 = 0;
        index1 = 0;
        for (int i = 0; i < debaters.Count(); i++)
        {
            initUnits(i);
        }
    }

    public void putPapal(int i)
    {
        DebaterObject temp = debaters.ElementAt(i);
        GameObject newObject = new GameObject("papal_" + temp.id.ToString(), typeof(RectTransform), typeof(Image));
        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater");
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
        newObject.transform.SetParent(gameObject.transform);
        newObject.AddComponent<DebaterClick>();
        newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(862, 296);
        
    }

    public void putProtestant(int i)
    {
        DebaterObject temp = debaters.ElementAt(i);
        GameObject newObject = new GameObject("protestant_" + (i + 1).ToString(), typeof(RectTransform), typeof(Image));
        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater");
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
        newObject.transform.SetParent(gameObject.transform);
        newObject.AddComponent<DebaterClick>();
        newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(905, 296);
        
    }

    public void removePapal(int i)
    {
        DebaterObject temp = debaters.ElementAt(i);
        GameObject tempObject1 = GameObject.Find("papal_" + temp.id.ToString());
        Destroy(tempObject1.gameObject);
    }

    public void removeProtestant(int i)
    {
        DebaterObject temp = debaters.ElementAt(i);
        GameObject tempObject1 = GameObject.Find("protestant_" + temp.id.ToString());
        Destroy(tempObject1.gameObject);
    }
}
