using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplayScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int thisPlayer;
    bool hover = false;
    // Start is called before the first frame update
    void Start()
    {
        chooseCard();

    }

    void OnEnable()
    {

        GM2.onChosenCard += chooseCard;
        
        //chooseCard();
    }

    void OnDisable()
    {

        GM2.onChosenCard -= chooseCard;

    }

    // Update is called once per frame
    void Update()
    {
        if (GM2.chosenCard != "")
        {
            if (hover)
            {
                gameObject.transform.localScale = new Vector3(2, 2, 1);


            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void chooseCard()
    {
        if (GM2.chosenCard == "")
        {
            var tempColor = gameObject.GetComponent<Image>().color;
                tempColor.a = 0f;
            gameObject.GetComponent<Image>().color = tempColor;
        }
        else
        {
            var tempColor = gameObject.GetComponent<Image>().color;
            tempColor.a = 255f;
            gameObject.GetComponent<Image>().color = tempColor;
        }
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/jpg/" + GM2.chosenCard);
        gameObject.SetActive(true);
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.fullyExited) return;
        hover = false;
    }
}
