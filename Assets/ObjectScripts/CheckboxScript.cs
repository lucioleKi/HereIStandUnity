using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class CheckboxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        

        gameObject.transform.Find("RandomDraw_0").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(0); });
        gameObject.transform.Find("RandomDraw_1").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(1); });
        gameObject.transform.Find("RandomDraw_2").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(2); });
        gameObject.transform.Find("RandomDraw_3").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(3); });
        gameObject.transform.Find("RandomDraw_4").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(4); });
        gameObject.transform.Find("RandomDraw_5").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(5); });

        gameObject.transform.Find("GiveMerc_1").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(6); });
        gameObject.transform.Find("GiveMerc_2").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(7); });
        gameObject.transform.Find("GiveMerc_3").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(8); });
        gameObject.transform.Find("GiveMerc_4").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(9); });
        gameObject.transform.Find("GiveMerc_5").GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { inputValidation(10); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void inputValidation(int index)
    {
        
        //UnityEngine.Debug.Log("input validation");
        if (index<6)
        {
            if (string.IsNullOrEmpty(GameObject.Find("RandomDraw_" + index.ToString()).GetComponent<TMP_InputField>().text))
            {
                return;
            }
            if (int.Parse(gameObject.transform.Find("RandomDraw_"+index.ToString()).GetComponent<TMP_InputField>().text) > 2)
            {
                gameObject.transform.Find("RandomDraw_" + index.ToString()).GetComponent<TMP_InputField>().text = "";
            }
            //UnityEngine.Debug.Log("random draw");
        }
        else if (index<11)
        {
            if(string.IsNullOrEmpty(gameObject.transform.Find("GiveMerc_" + (index - 5).ToString()).GetComponent<TMP_InputField>().text))
            {
                return;
            }
            if (int.Parse(gameObject.transform.Find("GiveMerc_" + (index-5).ToString()).GetComponent<TMP_InputField>().text) > 4)
            {
                gameObject.transform.Find("GiveMerc_" + (index - 5).ToString()).GetComponent<TMP_InputField>().text = "";
            }
            //UnityEngine.Debug.Log("give merc");
        }
    }
}
