using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusObject : ScriptableObject
{
    public int playerIndex;
    public int[] posX;
    public int[] posY;
    public float[] offsetX;
    public float[] offsetY;
    public int VP;

    public virtual void setVP()
    {
        return;
    }

    public virtual void setUp()
    {
        return;
    }
}
