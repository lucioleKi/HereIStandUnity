using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardObject card;
    public Text nameText; 
    public Image art;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.name;
        art.sprite = card.art;
    }

    
}
