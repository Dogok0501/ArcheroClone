using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    new Rigidbody rigidbody;
    int bounce = Define.BOUNCE_COUNT;
    int wallBounce = Define.WALL_BOUNCE_COUNT;
    Vector3 bulletForward = Vector3.forward;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * bulletSpeed;

        if (PlayerSkill.abllityList[3] > 0)
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }

        Destroy(this.gameObject, 3.0f);
    }

    Vector3 NextTarget(int index)
    {
        int closestIndex = -1;
        float distance = 300.0f;
        float currentDistance = 0.0f;

        for(int i = 0; i < EnemyCollector.enemiesInLevel.Count; i++)
        {            
            if (i == index)
                continue;

            currentDistance = Vector3.Distance(EnemyCollector.enemiesInLevel[i].transform.position, transform.position);
            
            if (currentDistance > 10.0f)
                continue;

            if (distance > currentDistance)
            {
                distance = currentDistance;
                closestIndex = i;
            }            
        }

        if (closestIndex == -1)
        {
            Destroy(this.gameObject, 0.1f);
            return Vector3.zero;
        }

        Vector3 adjustment = new Vector3(EnemyCollector.enemiesInLevel[closestIndex].transform.position.x - EnemyCollector.enemiesInLevel[index].transform.position.x, 0.0f, EnemyCollector.enemiesInLevel[closestIndex].transform.position.z - EnemyCollector.enemiesInLevel[index].transform.position.z);

        return adjustment.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            if(PlayerSkill.abllityList[1] > 0 && EnemyCollector.enemiesInLevel.Count > 1)
            {
                int index = EnemyCollector.enemiesInLevel.IndexOf(other.gameObject.GetComponent<EnemyController>());
                
                if (bounce > 0)
                {
                    bounce--;
                    rigidbody.velocity = NextTarget(index) * 25f;
                    return;
                }
                rigidbody.velocity = Vector3.zero;
                Destroy(this.gameObject, 0.1f);
            }
        }
        else if (other.transform.CompareTag("Wall"))
        {
            if(PlayerSkill.abllityList[2] == 0)
            {                
                rigidbody.velocity = Vector3.zero;
                Destroy(this.gameObject);
            }                
        }            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
            Destroy(this.gameObject);
        else if (collision.transform.CompareTag("Wall"))
        {
            if (PlayerSkill.abllityList[2] > 0)
            {
                if (wallBounce > 0)
                {
                    wallBounce--;
                    bulletForward = Vector3.Reflect(bulletForward, collision.contacts[0].normal);
                    rigidbody.velocity = bulletForward * 25.0f;
                    return;
                }
            }
            rigidbody.velocity = Vector3.zero;
            Destroy(this.gameObject);
        }
    }
}
