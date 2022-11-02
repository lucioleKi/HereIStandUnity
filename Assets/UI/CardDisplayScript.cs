using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDisplayScript : MonoBehaviour
{
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
}
