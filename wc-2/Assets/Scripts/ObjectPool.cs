using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool : Queue<GameObject>
{
    public ObjectPoolData content;

    public ObjectPool(){}

    public ObjectPool(ObjectPoolData data)
    {
        content = data;
    }

    public ObjectPool(Transform poolParent, GameObject objectPrefab, uint objectCount)
    {
        content.poolParent = poolParent;
        content.objectPrefab = objectPrefab;
        content.objectCount = objectCount;
    }

    public void Allocate(uint count)
    {
        new Queue<GameObject>((int)count);
        
        for(uint i = 0; i < count; i++)
        {
            GameObject obj = MonoBehaviour.Instantiate<GameObject>((GameObject)content.objectPrefab, Vector3.zero, Quaternion.identity , content.poolParent);
            obj.SetActive(false);
            Enqueue(obj);
        }
    }

    public void Allocate()
    {
        this.Allocate(content.objectCount);
    }
}