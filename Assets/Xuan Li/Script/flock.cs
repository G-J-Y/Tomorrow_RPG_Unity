using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class flock : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int amount;

    [SerializeField] private float generateRange;
    [SerializeField] private GameObject[] patrolPoints;

    // Start is called before the first frame update
    void Awake()
    {
        generateFlock();
        
    }

    void generateFlock()
    {
        Vector3 randomPosInASquare;
        for (int count = 0; count < amount; ++count)
        {
            randomPosInASquare = new Vector3(Random.Range(-generateRange, generateRange), 0, Random.Range(-generateRange, generateRange));
            GameObject flocking = (GameObject)Instantiate(enemyPrefab, transform.position + randomPosInASquare, Quaternion.identity);
            flocking.transform.parent = transform;
            for(int i=0;i<patrolPoints.Length;i++){
                flocking.GetComponent<patrol>().targetList.Enqueue(patrolPoints[i]);
            }
        }
    }
}
