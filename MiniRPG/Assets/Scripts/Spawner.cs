using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public Enemy slime;
    private List<Enemy> enemies;

    [Range(0, 100)]
    public int numberOfEnemies = 25;
    private float range = 70f;

    void Start()
    {
        enemies = new List<Enemy>();
        
        for (int index = 0; index < numberOfEnemies; index++)
        {
			Enemy spawnedSlime = Instantiate(slime, RandomNavMeshLocation(range), Quaternion.identity) as Enemy;
            enemies.Add(spawnedSlime);
        }
	}

    public Vector3 RandomNavMeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
