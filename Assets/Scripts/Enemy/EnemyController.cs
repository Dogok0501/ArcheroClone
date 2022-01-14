using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float wanderRadius = 10.0f;
    [SerializeField] float wanderTimer = 5.0f;

    NavMeshAgent agent;

    public int health = Define.ENEMY_HEALTH;
    float timer;
     

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        if(health <= 0)
        {
            EnemyCollector.enemiesInLevel.Remove(transform.GetComponent<EnemyController>());
            this.gameObject.SetActive(false);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
            health -= Define.BULLET_DAMAGE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
            health -= Define.BULLET_DAMAGE;
    }
}
