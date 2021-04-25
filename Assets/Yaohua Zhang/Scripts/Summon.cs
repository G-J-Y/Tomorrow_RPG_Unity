using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Summon : MonoBehaviour
{
    [SerializeField] private int healthValueToSummon;
    [SerializeField] private bool isSummon;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject summonEffect;

    private void Awake()
    {
        if (healthValueToSummon == 0)
        {
            healthValueToSummon = 50;
        }
        isSummon = false;
    }
    
    void Update()
    {
        CheckHealthValue();
    }

    void SpawnObjects()
    {
        for(int i= 0; i < spawnPoints.Length; i++)
        {
            //float randomX = Random.Range(transform.position.x - 10f, transform.position.x + 10f);
            //float randomZ = Random.Range(transform.position.z - 10f, transform.position.z + 10f);
            //Vector3 instantiatePosition = new Vector3(randomX, transform.position.y, randomZ);
            //Vector3 instantiateRotation = new Vector3(randomX, transform.position.y, randomZ);
            Instantiate(summonEffect, spawnPoints[i].position, Quaternion.identity);
            GameObject instantiateMonster =  Instantiate(spawnObject, spawnPoints[i].position, Quaternion.identity) as GameObject;
            // instantiateMonster.GetComponent<Attack>().SetTarget(target.transform);
            // instantiateMonster.GetComponent<Arrive>().SetTarget(target.transform);
        }

    }

    void CheckHealthValue()
    {
        if (GetComponent<Health>().GetCurrentHealth() < healthValueToSummon && !isSummon)
        {
            SpawnObjects();
            isSummon = true;
        }
    }
}
