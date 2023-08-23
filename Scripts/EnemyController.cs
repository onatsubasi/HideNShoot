using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    public int hp;

    [SerializeField]
    public Manager manager;

    [SerializeField]
    public EnemyPool EnemyPool;
    public Enemy enemy;
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();

        hp = Constants.ENEMY_MAX_HEALTH;
    }

    // TakeDamage is called in collisions

    // decreases enemy health, if 0 removes enemy
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            EnemyPool.ReturnEnemy(this);
            manager.current_enemy_count--;

            //Destroy (self);
        }
    }
}
