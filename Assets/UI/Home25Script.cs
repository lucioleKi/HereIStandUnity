using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home25Script : MonoBehaviour
{
    int place2;
    int place5;
    // Start is called before the first frame update
    void Start()
    {
        place2 = GM1.englishSpaces;
        place5 = GM1.protestantSpaces;
        HomePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (place2 != GM1.englishSpaces|| place5 != GM1.protestantSpaces)
        {
            place2 = GM1.englishSpaces;
            place5 = GM1.protestantSpaces;
            HomePosition();
        }
    }

    void HomePosition()
    {
        RectTransform pos = gameObject.GetComponent<RectTransform>();
        if (gameObject.name == "EnglishHome")
        {
            if (place2 < 13)
            {
                pos.anchoredPosition = new Vector2(592 + place2 * 26.92f, 252);
            }
            else if (place2 < 26)
            {
                pos.anchoredPosition = new Vector2(592 + (place2 - 13) * 26.92f, 223);
            }
            else if (place2 < 39)
            {
                pos.anchoredPosition = new Vector2(592 + (place2 - 26) * 26.92f, 194);
            }
            else
            {
                pos.anchoredPosition = new Vector2(592 + (place2 - 39) * 26.92f, 165);
            }
        }
        else
        {
            if (place5 < 13)
            {
                pos.anchoredPosition = new Vector2(592 + place5 * 26.92f, 252);
            }
            else if (place5 < 26)
            {
                pos.anchoredPosition = new Vector2(592 + (place5 - 13) * 26.92f, 223);
            }
            else if (place5 < 39)
            {
                pos.anchoredPosition = new Vector2(592 + (place5 - 26) * 26.92f, 194);
            }
            else
            {
                pos.anchoredPosition = new Vector2(592 + (place5 - 39) * 26.92f, 165);
            }
        }
        
    }
}
