using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DeckScript;

public class EmptyCard : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        if (GM1.phase != 1 && GM1.phase != 4 && GM1.phase != 6)
        {
            return;
        }


        ConfirmScript.cardSelected = eventData.pointerCurrentRaycast.gameObject.name;
        ConfirmScript.btn.interactable = true;
        int index = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(4)) - 1;
        UnityEngine.Debug.Log(index);
        UnityEngine.Debug.Log(cardsLib.Count());
        if ((int)cardsLib.ElementAt(index).cardType != 1)
        {
            CPButtonScript.cardSelected = eventData.pointerCurrentRaycast.gameObject.name;
            CPButtonScript.cardTag = eventData.pointerCurrentRaycast.gameObject.tag;
            CPButtonScript.btn.interactable = true;
        }
        else
        {
            CPButtonScript.btn.interactable = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
