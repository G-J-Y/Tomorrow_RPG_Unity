using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hide : MonoBehaviour
{
    [SerializeField] private bool isHide;
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent navAgent;

    public int frameInterval = 10;
    public int facePlayerFactor = 50;

    Vector3 randomPosition;
    Vector3 coverPoint;
    public float rangeRandPoint = 6f;

    public LayerMask coverLayer;
    Vector3 coverObj;
    public LayerMask visibleLayer;

    private float maxCovDis = 30;
    public bool coverIsClose;
    public bool coverNotReached = true;

    public float distToCoverPos = 1f;
    public float disToCoverObj = 60f;

    public float rangeDist = 15;
    private bool playerInRange = false;

    private int testCoverPos = 10;

    //bool RandomPoint(Vector3 center, float rangeRandPoint, out Vector3 resultCover)
    //{

    //    return false;
    //}

    //private void Awake()
    //{
    //    navAgent = GetComponent<NavMeshAgent>();
    //    isHide = false;
    //}


    //void Start()
    //{
        
    //}

    //void Update()
    //{
    //    CheckHealthValue();
    //    if (isSkill)
    //    {
    //        skillTimer -= Time.deltaTime;

    //    }
    //    if (skillTimer <= 0)
    //    {
    //        effect.GetComponent<ParticleSystem>().Stop();
    //        isSkill = false;
    //    }

    //}

    //void CheckHealthValue()
    //{
    //    if (GetComponent<Health>().GetCurrentHealth() < healthValueToDodge && !isDodge)
    //    {
    //        DodgeSkill();
    //        isDodge = true;
    //    }
    //}

    //void DodgeSkill()
    //{
        
    //        skillTimer = 10f;
    //        effect.GetComponent<ParticleSystem>().Play();

       
    //}

    //void DodgeObstacle1()
    //{

    //}
}
