using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GM1;
using static GM2;

public class HandMarkerScript : MonoBehaviour
{
    int player;
    // Start is called before the first frame update
    void Start()
    {
        player = GM1.player;
        showHandMarker();
    }

    void OnEnable()
    {
        onVP += showHandMarker;
        onPlayerChange += showHandMarker;
    }

    void OnDisable()
    {
        onVP -= showHandMarker;
        onPlayerChange -= showHandMarker;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void showHandMarker()
    {
        player = GM1.player;

        foreach (Transform child in GameObject.Find("HandMarkerDisplay").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        //GameObject newObject = null;
        switch (player)
        {
            case 0:
                status0.setUp();
                break;
            case 1:
                status1.setUp();
                break;
            case 2:
                status2.setUp();
                break;
            case 3:
                status3.setUp();
                break;
            case 4:
                status4.setUp();
                break;
            case 5:
                status5.setUp();
                break;
        }

    }
}
