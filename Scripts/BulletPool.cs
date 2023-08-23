using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Start is called before the first frame update

    public Queue<Bullet> bullets  = new();


    void Start()
    {

        foreach(Bullet bullet in GetComponentsInChildren<Bullet>()) 
        {
            bullet.bulletPool = this;
            bullets.Enqueue(bullet);
            bullet.gameObject.SetActive(false);
        }

    }



    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet = bullets.Dequeue();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.gameObject.SetActive(true);

        Debug.Log(bullet);
        return bullet;
    }


    public void ReturnBullet(Bullet b) 
    {
        b.gameObject.SetActive(false);
        bullets.Enqueue(b);
    }

}
