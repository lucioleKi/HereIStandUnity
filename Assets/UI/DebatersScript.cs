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
    int index2;
    int index3;
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
        //GM2.onPhase2 += updateDebater;



    }

    void OnDisable()
    {
        //GM2.onPhase2 -= updateDebater;

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
                if (temp.status == (DebaterStatus)1)
                {
                    newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater");
                }
                else
                {
                    newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater_back");
                }
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(27, 27);
                newObject.transform.SetParent(gameObject.transform);
                newObject.AddComponent<DebaterClick>();

                //debater init
                if (temp.type == 0)
                {

                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-110 + index0 * 27, 80);
                    index0++;
                }
                else if(temp.type == 1)
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-164 + index1 % 2 * 27, 5 + index1 / 2 * 27);
                    index1++;
                }else if(temp.type==2)
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-74 + index2 * 27, 5 + index2 / 2 * 27);
                    index2++;
                }
                else if (temp.type == 3)
                {
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(12 + index3 * 27, 5 + index3 / 2 * 27);
                    index3++;
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
        else
        {
            if (temp.status == (DebaterStatus)1)
            {
                GameObject.Find("debater_" + temp.id.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater");
            }
            else if(temp.status==(DebaterStatus)2)
            {
                GameObject.Find("debater_" + temp.id.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater_back");
            }
            if (temp.type == 0)
            {
                index0++;
            }
            else if (temp.type == 1)
            {
                index1++;
            }else if(temp.type==2)
            {
                index2++;
            }else if (temp.type == 3)
            {
                index3++;
            }
        }
        

        
       

    }

    public void updateDebater()
    {
        GM2.resetReligious();
        index0 = 0;
        index1 = 0;
        index2 = 0;
        index3 = 0;
        for (int i = 0; i < debaters.Count(); i++)
        {
            initUnits(i);
        }
    }

    public void putPapal(int i)
    {
        GM2.resetReligious();
        DebaterObject temp = debaters.ElementAt(i);
        UnityEngine.Debug.Log(temp.id);
        GameObject newObject = new GameObject("papal_" + temp.id.ToString(), typeof(RectTransform), typeof(Image));
        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater");
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
        newObject.transform.SetParent(gameObject.transform);
        newObject.AddComponent<DebaterClick>();
        newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(114, 10);
        
    }

    public void putProtestant(int i)
    {
        GM2.resetReligious();
        DebaterObject temp = debaters.ElementAt(i);
        UnityEngine.Debug.Log(temp.id);
        GameObject newObject = new GameObject("protestant_" + (i + 1).ToString(), typeof(RectTransform), typeof(Image));
        newObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/Debaters/" + temp.name + "Debater");
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
        newObject.transform.SetParent(gameObject.transform);
        newObject.AddComponent<DebaterClick>();
        newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(157, 10);
        
    }

    public void removePapal(int i)
    {
        GM2.resetReligious();
        DebaterObject temp = debaters.ElementAt(i);
        GameObject tempObject1 = GameObject.Find("papal_" + temp.id.ToString());
        Destroy(tempObject1.gameObject);
    }

    public void removeProtestant(int i)
    {
        GM2.resetReligious();
        DebaterObject temp = debaters.ElementAt(i);
        GameObject tempObject1 = GameObject.Find("protestant_" + temp.id.ToString());
        Destroy(tempObject1.gameObject);
    }

    public void toUncommited()
    {
        for(int i=0; i< debaters.Count(); i++)
        {
            if (debaters.ElementAt(i).status == (DebaterStatus)2)
            {
                debaters.ElementAt(i).status = (DebaterStatus)1;
                
            }
        }
        updateDebater();
    }
}
