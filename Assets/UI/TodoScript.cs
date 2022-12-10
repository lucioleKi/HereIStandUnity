using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class TodoScript : MonoBehaviour
{
    bool filled1 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void post()
    {
        gameObject.transform.Find("Todo1").GetComponent<CanvasGroup>().alpha = 1;
    }

    public void put1() {
        if (GM1.toDo.Count != 0)
        {
            gameObject.transform.Find("Todo1").GetComponent<CanvasGroup>().alpha = 1;
            filled1 = true;
            Text mtext = GameObject.Find("TodoText1").GetComponent<Text>();
            mtext.text = GM1.toDo.Dequeue();
            gameObject.transform.Find("Todo1").GetComponent<Toggle>().isOn = false;
        }
        else
        {
            filled1= false;
        }
        gameObject.transform.Find("Todo2").GetComponent<CanvasGroup>().alpha = 0;
    }

    public void moveUp()
    {
        Text mtext = GameObject.Find("TodoText1").GetComponent<Text>();
        mtext.text = GameObject.Find("TodoText2").GetComponent<Text>().text;
        if(GM1.toDo.Count != 0 )
        {
            Text mtext2 = GameObject.Find("TodoText2").GetComponent<Text>();
            mtext2.text = GM1.toDo.Dequeue();
        }
        else
        {
            gameObject.transform.Find("Todo2").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("TodoText2").GetComponent<Text>().text = "";
        }
       

    }

    public void check1()
    {
        gameObject.transform.Find("Todo1").GetComponent<Toggle>().isOn = true;
    }

    public void check2()
    {
        gameObject.transform.Find("Todo2").GetComponent<Toggle>().isOn = true;
    }

    public void put2()
    {
        if (GM1.toDo.Count != 0&&filled1)
        {
            gameObject.transform.Find("Todo2").GetComponent<CanvasGroup>().alpha = 1;
            Text mtext = GameObject.Find("TodoText2").GetComponent<Text>();
            mtext.text = GM1.toDo.Dequeue();
        }
    }

    public void remove2()
    {
        gameObject.transform.Find("Todo2").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("TodoText2").GetComponent<Text>().text = "";
    }
}
