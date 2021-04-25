using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossSummon : MonoBehaviour
{
    #region Private Serializable Fields

    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject spawnEffect;

    #endregion


    #region Custom

    public void SpawnMinions()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(spawnEffect, spawnPoints[i].position, Quaternion.identity);
            GameObject instantiateMonster = Instantiate(spawnObject, spawnPoints[i].position, Quaternion.identity);
        }

    }

    #endregion
    

}
