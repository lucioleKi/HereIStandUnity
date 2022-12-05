using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static EnumSpaceScript;

public class SeaObject
{
    public string name;
    public int id;
    public string matching;
    public List<int> adjacent;
    public List<int> ports;

    public SeaObject sea;

    public override bool Equals(System.Object obj)
    {
        var other = obj as SpaceObject;
        if (this.id == other.id || this.name.Equals(other.name))
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
