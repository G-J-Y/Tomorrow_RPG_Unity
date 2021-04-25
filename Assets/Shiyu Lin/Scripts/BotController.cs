using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The script will be used to control the bot's behaviour with
// given inputs
public class BotController : MonoBehaviour
{
    #region Public Fields

    // We will use enum to represent the state of the AI
    public enum State
    {
        Wonder,
        Attack,
        Chase,
        Return,

    }

    #endregion

    #region Private Fields
    // Current state of the AI
    private State currentState;
    private Wonder _wonder;
    private Attack _attack;
    private FieldOfView _fieldOfView;
    private LookWhereYouGo _lookWhereYouGo;
    private Arrive _arrive;
    private Return _return;
    private Transform playerTransform;
    private Vector3 startPos;
    private Animator _animator;
    #endregion



    #region MonoBehaviour Callbacks

    private void Awake()
    {
        // At the beginning the bot should just wonder
        currentState = State.Wonder;
        // We also need to get all behaviours' scripts
        _wonder = GetComponent<Wonder>();
        _attack = GetComponent<Attack>();
        _fieldOfView = GetComponent<FieldOfView>();
        _lookWhereYouGo = GetComponent<LookWhereYouGo>();
        _arrive = GetComponent<Arrive>();
        _return = GetComponent<Return>();
        // We will use the start position of the AI as well
        startPos = transform.position;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Wonder:
                _wonder.WonderAround();
                // While we are wondering, if the player is visible, we will attack the player
                if (_fieldOfView.visibleTargets.Count > 0)
                {
                    // Before we switch to another state, we will clear the animator
                    _animator.SetBool("isWalking",false);
                    _animator.SetBool("isRunning",false);
                    _animator.SetBool("isAttacking",false);
                    currentState = State.Attack;
                }
                break;
            case State.Attack:
                // While we are attacking the player, if the player run away we will move to
                // player's last visible position and try to find the player
                if (_fieldOfView.visibleTargets.Count != 0)
                {
                    playerTransform = _fieldOfView.visibleTargets[0];
                    _attack.AttackTarget(_fieldOfView.visibleTargets[0].position);
                }
                if (_fieldOfView.visibleTargets.Count == 0)
                {
                    // Before we switch to another state, we will clear the animator
                    _animator.SetBool("isWalking",false);
                    _animator.SetBool("isRunning",false);
                    _animator.SetBool("isAttacking",false);
                    currentState = State.Chase;
                }
                break;
            case State.Chase:
                // We will chase the player, if we can't catch up on the player
                // we will return to the original position, else we will attack the player again
                _arrive.SetTarget(playerTransform);
                if (_fieldOfView.visibleTargets.Count > 0)
                {
                    _arrive.target = null;
                    _animator.SetBool("isWalking",false);
                    _animator.SetBool("isRunning",false);
                    _animator.SetBool("isAttacking",false);
                    currentState = State.Attack;
                }
                if (Vector3.Distance(transform.position,playerTransform.position) >= 50)
                {
                    _animator.SetBool("isWalking",false);
                    _animator.SetBool("isRunning",false);
                    _animator.SetBool("isAttacking",false);
                    // Set the target of arrive to null
                    _arrive.SetTarget(null);
                    currentState = State.Return;
                }
                break;
            case State.Return:
                _return.SetTarget(startPos);
                _return.CalculateAcceleration();
                _return.ApplyAcceleration();
                // Once the AI is close enough to the start position, the AI will start to wonder
                if (Vector3.Distance(transform.position,startPos) <= 5f)
                {
                    _animator.SetBool("isWalking",false);
                    _animator.SetBool("isRunning",false);
                    _animator.SetBool("isAttacking",false);
                    currentState = State.Wonder;
                }
                break;
        }
    }
    

    #endregion
}
