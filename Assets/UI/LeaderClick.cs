using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static GM2;

public class LeaderClick : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);

        

        leaderSelected = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(7));

    }
}
