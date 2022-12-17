using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hover = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
        if(hover) {
            float factor = Mathf.Clamp(gameObject.transform.localScale.x + Input.GetAxis("Mouse ScrollWheel"), 1f, 4f);
            
            gameObject.transform.localScale = new Vector3(factor, factor, 1);
        }
        

    }

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover= true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover= false;
    }
}
