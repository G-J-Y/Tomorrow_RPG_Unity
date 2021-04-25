using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LookWhereYouGo))]
public class Wonder : MonoBehaviour
{
    #region Private Field
    // Stores the position of the enemy at the start of the game
    private Vector3 startPosition;
    // Getting the class and we will use its method LookAtDirection(vector3 targetPosition)
    private LookWhereYouGo _lookWhereYouGo;
    // Counter for moving
    private float movingCounter = 0f;
    // Counter for stop and standing
    private float standingCounter = 0f;
    private float randomMovingTime;
    private float randomStandingTime;
    
    private Vector3 targetPosition;
    
    private Animator _animator;
    #endregion

    #region Private Serializable Field
    // Maximum distance from the current position of the player to the start position
    [SerializeField] 
    private float maxDistance;

    [SerializeField] 
    private float speed;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        startPosition = transform.position;
        _lookWhereYouGo = GetComponent<LookWhereYouGo>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (maxDistance == 0f)
        {
            maxDistance = 40f;
        }

        if (speed == 0f)
        {
            speed = 5f;
        }
        
    }

    // private void Update()
    // {
    //     WonderAround();
    // }

    #endregion

    #region Custom Methods
    /// <summary>
    /// From the start position, randomly generate a direction, the character will
    /// move toward that direction for a random seconds, stop for a random seconds,
    /// and keep looping the process unless new actions is assigned to the character
    /// </summary>
    public void WonderAround()
    {
        if (movingCounter <= 0.01f)
        {
            randomMovingTime = Random.Range(2.5f, 5.5f);
            randomStandingTime = Random.Range(2.5f, 5.5f);
            float angleInDeg = Random.Range(-360f, 360f);
            float xPos = maxDistance * Mathf.Cos(angleInDeg * Mathf.Deg2Rad) + startPosition.x;
            float zPos = maxDistance * Mathf.Sin(angleInDeg * Mathf.Deg2Rad) + startPosition.z;
            targetPosition = new Vector3(xPos, 0, zPos);

        }
        else
        {
            if (movingCounter <= randomMovingTime && Vector3.Distance(transform.position,targetPosition) >= 5f)
            {
                _lookWhereYouGo.LookAtDirection(targetPosition);
                if (Vector3.Distance(transform.position,targetPosition) >= 5f)
                {
                    transform.Translate(Vector3.forward * (Time.deltaTime * speed));
                    _animator.SetBool("isWalking",true);
                }
            }
            else
            {
                standingCounter += Time.deltaTime;
                _animator.SetBool("isWalking",false);
                if (standingCounter >= randomStandingTime)
                {
                    movingCounter = 0f;
                    standingCounter = 0f;
                }
            }
        }
        movingCounter += Time.deltaTime;
    }

    #endregion
}
