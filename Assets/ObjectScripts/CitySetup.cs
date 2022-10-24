using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;


[CreateAssetMenu]
public class CitySetup : ScriptableObject
{
    public new string name;
    public int id;
    public int regular;
    public int cavalry;
    public int squadron;
    public int controlPower;
    public int controlMarker;//0 = empty, 1 = HCM, 2 = hcm, 3 = SCM, 4 = merc
    public int leader1;
    public int leader2;

    public CitySetup()
    {
        this.regular = 0;
        this.cavalry = 0;
        this.squadron = 0;
        this.controlMarker = 0;
        this.leader1 = 0;
        this.leader2 = 0;
    }
}
