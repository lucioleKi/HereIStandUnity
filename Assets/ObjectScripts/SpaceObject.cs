using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static EnumSpaceScript;

public class SpaceObject
{
    public string name;
    public int id;
    public int posX;
    public int posY;
    public SpaceType spaceType;
    public PowerType2 homePower;
    public Language language;
    public string matching;
    public List<string> adjacent;
    public List<string> pass;
    
    public SpaceObject space;

    public override bool Equals(System.Object obj)
    {
        var other = obj as SpaceObject;
        if (this.id == other.id||this.name.Equals(other.name)||this.matching.Equals(other.matching))
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
