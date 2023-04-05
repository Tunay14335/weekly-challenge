using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoneLiterals
{
    /// Scale : [2,6]
    public static readonly CountInterval Scale = new CountInterval(2,6);

    public enum Level
    {
        green       = 1,    
        blue        = 2,
        magenta     = 3,
        red         = 4,
    }
    
    public static readonly Dictionary<Level,CountInterval> LevelRanges = new Dictionary<Level, CountInterval>
    {
        {
            Level.green,
            new CountInterval(1,300)
        },
        {
            Level.blue,
            new CountInterval(301,700)
        },
        {
            Level.magenta,
            new CountInterval(701,1200)
        },
        {
            Level.red,
            new CountInterval(1201,3000)
        },
    };
}
