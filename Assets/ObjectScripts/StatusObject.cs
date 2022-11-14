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

    public virtual int setVP(int index)
    {
        return 0;
    }

    public virtual void setUp()
    {
        return;
    }
}
