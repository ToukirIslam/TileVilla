using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.right);
    }
    [SerializeField] float bulletSpeed = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void 
    
}
 