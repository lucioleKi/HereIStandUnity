using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RulerClass
{
    public int index;
    public string name;
    public int adminRating;
    public int cardBonus;

    public RulerClass(int index, string name, int adminRating, int cardBonus)
    {
        this.index = index;
        this.name = name;
        this.adminRating = adminRating;
        this.cardBonus = cardBonus;
    }
    public void toString()
    {
        UnityEngine.Debug.Log(name + ", " + adminRating.ToString() + ", " + cardBonus.ToString());
    }

}
