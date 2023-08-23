using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The class that holds the all enemy data 
public class Enemy : MonoBehaviour
{
    private Constants.EnemyState state; // enemy current state
    private Constants.EnemyState prevState; // enemy previous state
    public GameObject bulletSpawnPosition; // bullet position to be spawned

    public Constants.EnemyState State
    {
        get { return state;}
        set { state = value;}
    }
    public Constants.EnemyState PrevState
    {
        get { return prevState;}
        set { prevState = value;}
    }

    public Vector3 Position{
        get{return transform.position;}
        set{transform.position = value;}
    }

}
