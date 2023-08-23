using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_movement : MonoBehaviour
{
    public Animator animator; 
    public GameObject player;

    private Enemy enemy;

    [SerializeField]
    float speed = 7f;

    private Vector3 destination;

    private Vector3 startingPosition;

    private int obstacleCount;

    private int lastObstacleIndex;

    private int currentObstacleIndex;

    private GameObject[] obstacles;

    public GameObject bullet;

    public BulletPool bulletPool;

    private bool canFire = false;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        // TODO obstacle info in game manager
        animator = GetComponent<Animator>();
        animator.enabled = true;

        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        obstacleCount = obstacles.Length;

        //player = GameObject.FindGameObjectWithTag("Player");
        startingPosition = enemy.Position;
        lastObstacleIndex = -1;
        SetRandomIndex();
        destination = GetNewDestination();
        enemy.State = Constants.EnemyState.MOVE;
    }

    private Vector3 GetNewDestination()
    {
        return obstacles[currentObstacleIndex].transform.position;
    }

    private void SetRandomIndex()
    {
        do
        {
            currentObstacleIndex = Random.Range(0, obstacleCount);
        }
        while (currentObstacleIndex == lastObstacleIndex);
    }

    void Update()
    {
        RotateFaceToPlayer();
        DecideNextStep();
    }

    private void DecideNextStep()
    {

        // in the move state enemy starts to moving towards to target obstacle
        if (enemy.State == Constants.EnemyState.MOVE)
        {
            enemy.State = Constants.EnemyState.PATROLLING;
            canFire = true;
            StartCoroutine(MoveToTarget());
        }
    
        else if (enemy.State == Constants.EnemyState.IDLE)
        {

            // when enemy reached to obstacle, it starts hiding for an interval
            if (enemy.PrevState == Constants.EnemyState.PATROLLING)
            {
                enemy.State = Constants.EnemyState.HIDING;
                StartCoroutine(HideForRandomInterval());
            }
            // when the hiding interval ends, new target obstacle is determined 
            else
            {
                SetRandomIndex();
                destination = GetNewDestination();
                enemy.State = Constants.EnemyState.MOVE;
            }
        }
        // while patrolling, enemy firing shoots with one second intervals 
        else if (enemy.State == Constants.EnemyState.PATROLLING)
        {
            if (canFire)
            {
                canFire = false;
                Fire();
                StartCoroutine(OneSecond());
            }
        }
    }

    // Fire bullet to direction that enemy face
    private void Fire()
    {
        animator.SetBool("Shoot",true);
 

        Bullet bullet = bulletPool.GetBullet(enemy.bulletSpawnPosition.transform.position + new Vector3(0, Random.Range(-1f, 1f), 0),enemy.bulletSpawnPosition.transform.rotation );
        
        animator.SetBool("Shoot", false);
    }

    private IEnumerator OneSecond()
    {
        yield return new WaitForSeconds(1f);
        canFire = true;
    }

    // Hide behind an obstacle for a random interval
    private IEnumerator HideForRandomInterval()
    {
        int interval = Random.Range(3, 6);
        yield return new WaitForSeconds(interval);
        enemy.PrevState = Constants.EnemyState.HIDING;
        enemy.State = Constants.EnemyState.IDLE;
        yield return null;
    }

    IEnumerator MoveToTarget()
    {
        animator.SetBool("Run",true);
        Vector3 currentPosition = enemy.Position;
        Vector3 targetPosition = destination;
        Vector3 midpoint = new Vector3();
        bool isCrossMove = false;

        // If movement contains a move in x axis
        // enemy first move in x axis then z axis or vice versa
        if (currentPosition.x > targetPosition.x)
        {
            midpoint =
                new Vector3(targetPosition.x,
                    currentPosition.y,
                    currentPosition.z);
            isCrossMove = true;
        }
        else if (currentPosition.x < targetPosition.x)
        {
            midpoint =
                new Vector3(currentPosition.x,
                    currentPosition.y,
                    targetPosition.z);
            isCrossMove = true;
        }

        

        // if target is not aligned with our current position we either move x or z axis
        if (isCrossMove)
        {
            midpoint.y = 0; 
            while (Vector3.Distance(enemy.Position, midpoint) > 0.1f)
            {
                Vector3 direction = (midpoint - enemy.Position).normalized;
                enemy.Position += direction * speed * Time.deltaTime;
                yield return null;
            }
        }
        
        targetPosition.y = 0;
        while (Vector3.Distance(enemy.Position, targetPosition) > 0.1f //
        )
        {

            // Calculate the direction towards the target
            Vector3 direction = (targetPosition - enemy.Position).normalized;

            // Move the object towards the target
            enemy.Position += direction * speed * Time.deltaTime;
            yield return null;
        }

        enemy.State = Constants.EnemyState.IDLE;
        enemy.PrevState = Constants.EnemyState.PATROLLING;
        lastObstacleIndex = currentObstacleIndex;
        animator.SetBool("Run",false);
        yield return null;
    }

    private void RotateFaceToPlayer()
    {
        Vector3 relativePos = player.transform.position - enemy.Position;
        enemy.transform.rotation =
            Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
    }
}
