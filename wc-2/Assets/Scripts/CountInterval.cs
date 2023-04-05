using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountInterval
{
    public uint Min {get; private set;}
    public uint Max {get; private set;}

    public CountInterval(uint min, uint max)
    {
        Min = min;
        Max = max;
    }
    public bool InRange(uint value)
    {
        return Min <= value && Max >= value;
    }
}
