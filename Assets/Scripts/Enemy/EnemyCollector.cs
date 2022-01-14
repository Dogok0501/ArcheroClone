using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollector : MonoBehaviour
{
    public static List<EnemyController> enemiesInLevel;

    private void Start()
    {
        enemiesInLevel = new List<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemiesInLevel.Add(enemy);
        }            
    }
}
