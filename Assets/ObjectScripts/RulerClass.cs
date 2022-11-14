using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerClass
{

    public string name;
    public int adminRating;
    public int cardBonus;

    public RulerClass(string name, int adminRating, int cardBonus)
    {
        this.name = name;
        this.adminRating = adminRating;
        this.cardBonus = cardBonus;
    }
    public void toString()
    {
        UnityEngine.Debug.Log(name + ", " + adminRating.ToString() + ", " + cardBonus.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
