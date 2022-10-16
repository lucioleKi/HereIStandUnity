using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;

[CreateAssetMenu]
public class CardObject : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public CardType cardType;
    public Sprite art;
    public bool canRandomDraw;
    public int CP;
    public bool canActionPhase;
    public int specialPhase;
    public int turn;//0 = variable
    public int turnSpecial;
    public int remove;//0= no, 1 = yes, 2 = leader, 3 = special
    public int options;//number of options
    public string matching;

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
