using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockEnemyController : MonoBehaviour
{
    #region Public Fields

    // We will use enum to represent the state of the AI
    public enum State
    {
        Patrol,
        Attack

    }

    public float attackDistance;

    #endregion

    #region Private Fields
    // Current state of the AI
    public State currentState;
    private patrol _patrol;
    private flockAttack _attack;
    private Transform playerTransform;
    private GameObject player;
    private Animator _animator;
    private flockAgent _flocking;
    #endregion



    #region MonoBehaviour Callbacks

    private void Awake()
    {
        // At the beginning the bot should just wonder
        currentState = State.Patrol;
        // We also need to get all behaviours' scripts
        _patrol = GetComponent<patrol>();
        _attack = GetComponent<flockAttack>();
        _flocking = GetComponent<flockAgent>();
        // We will use the start position of the AI as well
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        float distance = (playerTransform.position - transform.position).magnitude;
        switch (currentState)
        {
            case State.Patrol:
                _patrol.switchTarget();
                //_flocking.Composite();
                if (distance<attackDistance)
                {
                    //playerTransform = _fieldOfView.visibleTargets[0];
                    // Before we switch to another state, we will clear the animator
                    _animator.SetBool("isWalking",false);
                    _animator.SetBool("isAttacking",false);
                    currentState = State.Attack;
                }
                break;
            case State.Attack:
                // While we are attacking the player, if the player run away we will move to
                // player's last visible position and try to find the player
                if (distance<attackDistance)
                {
                    _attack.AttackTarget(playerTransform.position,distance,player);

                }
                if (distance>attackDistance)
                {
                    // Before we switch to another state, we will clear the animator
                    _animator.SetBool("isWalking",false);
                    _animator.SetBool("isAttacking",false);
                    currentState = State.Patrol;
                }
                break;
            
        }
    }
    

    #endregion
}

