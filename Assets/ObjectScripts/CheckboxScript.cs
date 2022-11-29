using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CheckboxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("EndWar").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus(); });
        gameObject.transform.Find("EndWar_0").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus(); });
        gameObject.transform.Find("EndWar_1").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus(); });
        gameObject.transform.Find("EndWar_2").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus(); });
        gameObject.transform.Find("EndWar_3").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus(); });
        gameObject.transform.Find("EndWar_4").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus(); });
        gameObject.transform.Find("EndWar_5").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus(); });

        gameObject.transform.Find("Alliance").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus1(); });
        gameObject.transform.Find("Alliance_0").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus1(); });
        gameObject.transform.Find("Alliance_1").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus1(); });
        gameObject.transform.Find("Alliance_2").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus1(); });
        gameObject.transform.Find("Alliance_3").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus1(); });
        gameObject.transform.Find("Alliance_4").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus1(); });
        gameObject.transform.Find("Alliance_5").GetComponent<Toggle>().onValueChanged.AddListener(delegate { changeStatus1(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeStatus()
    {
        
        if (gameObject.transform.Find("EndWar_0").GetComponent<Toggle>().isOn|| gameObject.transform.Find("EndWar_1").GetComponent<Toggle>().isOn|| gameObject.transform.Find("EndWar_2").GetComponent<Toggle>().isOn|| gameObject.transform.Find("EndWar_3").GetComponent<Toggle>().isOn|| gameObject.transform.Find("EndWar_4").GetComponent<Toggle>().isOn|| gameObject.transform.Find("EndWar_5").GetComponent<Toggle>().isOn)
        {
            gameObject.transform.Find("EndWar").GetComponent<Toggle>().isOn = true;
        }
        else
        {
            gameObject.transform.Find("EndWar").GetComponent<Toggle>().isOn = false;
        }
    }

    void changeStatus1()
    {
        if (gameObject.transform.Find("Alliance_0").GetComponent<Toggle>().isOn || gameObject.transform.Find("Alliance_1").GetComponent<Toggle>().isOn || gameObject.transform.Find("Alliance_2").GetComponent<Toggle>().isOn || gameObject.transform.Find("Alliance_3").GetComponent<Toggle>().isOn || gameObject.transform.Find("Alliance_4").GetComponent<Toggle>().isOn || gameObject.transform.Find("Alliance_5").GetComponent<Toggle>().isOn)
        {
            gameObject.transform.Find("Alliance").GetComponent<Toggle>().isOn = true;
        }
        else
        {
            gameObject.transform.Find("Alliance").GetComponent<Toggle>().isOn = false;
        }
    }
}
