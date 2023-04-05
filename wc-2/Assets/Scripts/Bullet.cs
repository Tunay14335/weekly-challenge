using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Unity Impls
    
    private void FixedUpdate()
    {
        if(transform.position.y > 15f) Remove(this.gameObject);
    }

    #endregion

    #region Custom Impls

    public static void Remove(GameObject obj)
    {
        GameCore.Instance.GetBulletPool.Enqueue(obj);
        obj.SetActive(false);
    }

    #endregion
}
