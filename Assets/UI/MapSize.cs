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
            float factor = Mathf.Clamp(gameObject.transform.localScale.x + Input.GetAxis("Mouse ScrollWheel"), 0.25f, 4f);
            if (gameObject.transform.localScale.x < 1)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);

                return;
            }
            else if (gameObject.transform.localScale.x > 4 && factor > 1)
            {
                return;
            }
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
