using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float shootRate;
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    #region Unity Impls

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Shoot) , 0f , shootRate);
    }

    #endregion

    public void Shoot()
    {
        GameObject bullet1 = GameCore.Instance.GetBulletPool.Dequeue() as GameObject;
        GameObject bullet2 = GameCore.Instance.GetBulletPool.Dequeue() as GameObject;

        bullet1.transform.position = this.transform.position + Vector3.right;
        bullet2.transform.position = this.transform.position + Vector3.left;

        bullet1.SetActive(true);
        bullet2.SetActive(true);

        bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.up * 30;
        bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.up * 30;
    }

    public void Move(Vector2 vec)
    {
        if(transform.position.x >= -8f && transform.position.x <= 8f)
        {
            rb.velocity = (vec * Vector2.right) * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            transform.position = transform.position * Vector2.up + Vector2.right * Mathf.Sign(transform.position.x) * 8f;
        }
    }

}
