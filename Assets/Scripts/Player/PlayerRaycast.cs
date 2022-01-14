using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] EnemyCollector enemyCollector;
    PlayerMovement playerMovement;

    List<float> enemyDistance;
    List<RaycastHit> hits;

    float closestDistance;
    int closestindex;

    public bool enemySpotted;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        enemyDistance = new List<float>();
        hits = new List<RaycastHit>();
        enemySpotted = false;
    }

    void Update()
    {
        if (EnemyCollector.enemiesInLevel.Count != 0)
        {
            enemySpotted = true;
            ShootingRay();
            GetClosestEnemy();
        }
        else
        {
            enemySpotted = false;
            Clear();
        }            
    }

    void ShootingRay()
    {
        for (int i = 0; i < EnemyCollector.enemiesInLevel.Count; i++)
        {
            RaycastHit hit;

            if (Physics.Linecast(transform.position, EnemyCollector.enemiesInLevel[i].transform.position, out hit))
            {
                hits.Add(hit);
                Debug.DrawLine(transform.position, EnemyCollector.enemiesInLevel[i].transform.position, Color.red);
            }
        }
    }

    void GetClosestEnemy()
    {
        for (int i = 0; i < EnemyCollector.enemiesInLevel.Count; i++)
        {
            enemyDistance.Add((EnemyCollector.enemiesInLevel[i].transform.position - transform.position).sqrMagnitude);
        }

        if (hits.TrueForAll(AllBlocked))
        {
            closestDistance = enemyDistance.Min();
            closestindex = enemyDistance.IndexOf(closestDistance);

            if (!playerMovement.isMoving)
                transform.LookAt(EnemyCollector.enemiesInLevel[closestindex].transform);

            return;
        }

        for (int i = 0; i < enemyDistance.Count; i++)
        {
            closestDistance = enemyDistance.Min();
            closestindex = enemyDistance.IndexOf(closestDistance);

            if (hits[closestindex].transform.CompareTag("Enemy"))
            {                
                break;
            }
            else
            {
                hits.RemoveAt(closestindex);
                enemyDistance.Remove(closestDistance);
            }                
        }
                
        if (!playerMovement.isMoving)
            transform.LookAt(EnemyCollector.enemiesInLevel[closestindex].transform);
               
        Debug.DrawLine(transform.position, EnemyCollector.enemiesInLevel[closestindex].transform.position, Color.blue);

        enemyDistance.Clear();
    }

    void Clear()
    {
        EnemyCollector.enemiesInLevel.Clear();
        enemyDistance.Clear();
        hits.Clear();
    }

    bool AllBlocked(RaycastHit hit)
    {
        return hit.transform.name == "Wall";
    }
}
