using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective<T>
{
    private T m_Progress;
    
    public T Goal {get; private set;}
    public T Progress
    {
        get => m_Progress;
        set
        {
            m_Progress = value;
            OnProgressChanged?.Invoke();
            if(m_Progress.Equals(Goal))
            {
                OnGoalAchieved?.Invoke();
            }
        }
    }
    
    public UnityAction OnProgressChanged;
    public UnityAction OnGoalAchieved;

    public Objective(T goal, T progress)
    {
        Goal = goal;
        Progress = progress;
    }
    
    /// if objective T has default
    public Objective(T goal)
    {
        Goal = goal;
    }
}
