using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiplomacyDisplay : MonoBehaviour
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

        GM2.onChangeSegment += changeDisplay;
    }

    void OnDisable()
    {
        GM2.onChangeSegment -= changeDisplay;
    }

    void changeDisplay()
    {
        switch (GM1.segment)
        {
            case 1:
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().alpha = 1;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().blocksRaycasts = true;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().interactable = true;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().interactable = false;
                break;
            case 2:
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().interactable = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().alpha = 1;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().blocksRaycasts = true;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().interactable = true;
                break;
            case 3:
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().interactable = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().interactable = false;
                break;
            case 4:
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().interactable = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().interactable = false;
                break;
            case 5:
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Negotiation").GetComponent<CanvasGroup>().interactable = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().alpha = 0;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.transform.Find("Peace").GetComponent<CanvasGroup>().interactable = false;
                break;
        }
    }
}
