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
            Vector3 mousePos = Input.mousePosition;
            
            
            float factor = Mathf.Clamp(gameObject.transform.localScale.x + Input.GetAxis("Mouse ScrollWheel"), 1f, 4f);
            
            gameObject.transform.localScale = new Vector3(factor, factor, 1);
            Vector3 newMousePos = Input.mousePosition;
            /*if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - (mousePos.x-846)/factor, gameObject.transform.position.y - (mousePos.y-500)/factor, 0);

            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + (mousePos.x - 846) / factor, gameObject.transform.position.y + (mousePos.y - 500) / factor, 0);

            }*/
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
