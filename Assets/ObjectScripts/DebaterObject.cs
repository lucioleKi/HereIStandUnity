using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;

public class DebaterObject
{
    public string name;
    public int id;
    public int posX;
    public int posY;
    public int value;
    public int turn;
    public int type;
    public Language language;

    public DebaterObject debater;

    public override bool Equals(System.Object obj)
    {
        var other = obj as DebaterObject;
        if (this.id == other.id || this.name.Equals(other.name) || this.name.Equals(other.name))
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
