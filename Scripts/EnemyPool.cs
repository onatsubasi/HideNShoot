using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyPool : MonoBehaviour
{
    // Start is called before the first frame update

    public Queue<EnemyController> enemies = new();


    void Start()
    {

        foreach (EnemyController enemy in GetComponentsInChildren<EnemyController>())
        {
            enemy.EnemyPool = this;
            enemies.Enqueue(enemy);
            enemy.gameObject.SetActive(false);
        }

    }



    public EnemyController GetEnemy()
    {
        EnemyController enemy = enemies.Dequeue();
        enemy.gameObject.GetComponent<Enemy>().State = Constants.EnemyState.MOVE;
        enemy.gameObject.SetActive(true);
        enemy.hp = 3;
        return enemy;
    }


    public void ReturnEnemy(EnemyController e)
    {
        e.gameObject.SetActive(false);
        enemies.Enqueue(e);


    }

}
