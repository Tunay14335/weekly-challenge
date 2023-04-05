using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneData
{
    private uint m_count;
    private uint m_scale;
    private Vector2 m_initVelocity;
    private StoneLiterals.Level m_level;
    private CountInterval m_currentLevelInterval;

    public UnityAction OnLevelChanged;

    public uint Count
    {
        get => m_count;
        set
        {
            m_count = value;
            CheckLevel();
        }
    }
    
    public uint Scale
    {
        get => m_scale;
        set => m_scale = value;
    }

    public StoneLiterals.Level Level => m_level;

    public StoneData(uint count, uint scale, Vector2 initVelocity)
    {
        m_count = count;
        m_scale = scale;
        m_initVelocity = initVelocity;

        foreach(var kvp in StoneLiterals.LevelRanges)
        {
            if(kvp.Value.InRange(count))
            {
                m_level = kvp.Key;
                m_currentLevelInterval = kvp.Value;
            }
        }
    }

    private void CheckLevel()
    {
        if(m_level != StoneLiterals.Level.green)
        {
            if(m_currentLevelInterval.Min > m_count)
            {
                m_level--;
                m_currentLevelInterval = StoneLiterals.LevelRanges[m_level];
                OnLevelChanged?.Invoke();
            }
        }
    }

}
