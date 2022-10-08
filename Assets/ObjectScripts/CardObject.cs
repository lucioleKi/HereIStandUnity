using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;

[CreateAssetMenu]
public class CardObject : ScriptableObject
{
    public new string name;
    public string description;
    public CardType cardType;
    public bool canRandomDraw;
    public int CP;
    public bool canActionPhase;
    public int specialPhase;
    int turn;
    int turnSpecial;
    public bool remove;
}
