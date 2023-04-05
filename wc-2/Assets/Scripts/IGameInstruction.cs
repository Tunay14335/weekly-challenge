using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameInstruction
{
    void Setup();
    void Clear();
    void Init();
    void End();
}
