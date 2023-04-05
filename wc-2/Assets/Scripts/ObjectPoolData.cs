using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolData
{
    [field:SerializeField] public Transform poolParent {get; set;}
    [field:SerializeField] public GameObject objectPrefab {get; set;}
    public uint objectCount {get; set;}
}
