using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumSpaceScript;



public class SpaceGM
{
    public string name;
    public int id;
    public int regular;
    public int cavalry;
    public int squadron;
    public int corsair;
    public int controlPower;
    public int controlMarker;//0 = empty, 1 = HCM, 2 = hcm, 3 = SCM, 4 = merc
    public int leader1;
    public int leader2;

    public SpaceGM()
    {
        this.regular = 0;
        this.cavalry = 0;
        this.squadron = 0;
        this.corsair = 0;
        this.controlMarker = 0;
        this.controlPower = 0;
        this.leader1 = 0;
        this.leader2 = 0;
    }

    public SpaceGM(CitySetup city)
    {
        this.name = city.name;
        this.id = city.id;
        this.regular = city.regular;
        this.cavalry = city.cavalry;
        this.squadron = city.squadron;
        this.corsair = 0;
        this.controlMarker = city.controlMarker;
        this.controlPower = city.controlPower;
        this.leader1 = city.leader1;
        this.leader2 = city.leader2;

    }

    public override bool Equals(System.Object obj)
    {
        var other = obj as SpaceGM;
        if (this.id == other.id || this.name.Equals(other.name) )
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

    public void removeLeader(int leaderIndex)
    {
        if(leaderIndex == leader1&& leader2!=0)
        {
            leader1 = leader2;
            leader2 = 0;
        }else if (leaderIndex == leader2)
        { 
            leader2 = 0;
        }
    }

    public void addLeader(int leaderIndex)
    {
        if (leader1 != 0)
        {
            leader2 = leaderIndex;
        }
        else if (leader2 != 0)
        {
            leader1 = leaderIndex;
        }
    }

    public void flipControl()
    {
        switch (controlMarker)
        {
            case 1:
                controlMarker = 2;
                break;
                case 2:
                controlMarker = 1;
                break;
            case 3:
                controlMarker = 4;
                break;
            case 4:
                controlMarker = 3;
                break;
        }
    }
}
