using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmptyCard : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //UnityEngine.Debug.Log("here");


        ConfirmScript.cardSelected = eventData.pointerCurrentRaycast.gameObject.name;
        ConfirmScript.btn.interactable = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
