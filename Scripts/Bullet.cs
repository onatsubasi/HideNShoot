using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPool bulletPool;
    // life: the time the bullet disappears after initialized
    public float speed = 8f;

    private void FixedUpdate()
    {
        // bullet position calculation in each frame
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            return;
        }
        bulletPool.ReturnBullet(this);
    }

}
