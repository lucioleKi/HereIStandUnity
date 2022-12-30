using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static EnumSpaceScript;

[System.Serializable]
public class CardObject
{
    public int id;
    public string name;
    public string description;
    public CardType cardType;
    public bool canRandomDraw;
    public int CP;
    public bool canActionPhase;
    public int specialPhase;
    public int turn;//0 = variable
    public int turnSpecial;
    public int remove;//0= no, 1 = yes, 2 = leader, 3 = special
    public int options;//number of options
    public string matching;

    public CardObject card;

    public override bool Equals(System.Object obj)
    {
        var other = obj as CardObject;
        if (this.id == other.id || this.name.Equals(other.name) || this.matching.Equals(other.matching))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
