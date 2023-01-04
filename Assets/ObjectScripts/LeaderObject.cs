using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderObject
{
    public string name;
    public int id;
    public int posX;
    public int posY;
    public int battle;
    public int command;
    public int turn;
    public int type;//0=land, 1=naval
    public string matching;
    public int power;

    public LeaderObject leader;

    public override bool Equals(System.Object obj)
    {
        var other = obj as LeaderObject;
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
