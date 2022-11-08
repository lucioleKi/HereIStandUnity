using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;

[CreateAssetMenu]
public class PowerObject : ScriptableObject
{
    public new string name;
    public Sprite art;
    public int order;
    public PowerType1 powerType1;
    public RulerObject ruler;
    public StatusObject status;
}
