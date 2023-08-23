using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    EnemyController parentScript; // Declare the variable here to make it accessible in the class
    public Manager manager;


    void Start()
    {
        // Use GetComponentInParent to find the ParentScript attached to the parent GameObject
        parentScript = GetComponentInParent<EnemyController>();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            manager.score += Constants.ENEMY_BODY_DAMAGE * 100;
            parentScript.TakeDamage(Constants.ENEMY_BODY_DAMAGE);
        }
    }
}
