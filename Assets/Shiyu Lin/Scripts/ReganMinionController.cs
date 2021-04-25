using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The script will be used to control the bot's behaviour with
// given inputs
public class ReganMinionController : MonoBehaviour
{
    #region Public Fields
    
    #endregion

    #region Private Fields
    // Current state of the AI
    private Attack _attack;
    private FieldOfView _fieldOfView;
    private Arrive _arrive;
    private Transform playerTransform;
    private Animator _animator;
    private GameObject playerInfo;
    #endregion



    #region MonoBehaviour Callbacks

    private void Awake()
    {
        // At the beginning the bot should just wonder
        // We also need to get all behaviours' scripts
        _attack = GetComponent<Attack>();
        _fieldOfView = GetComponent<FieldOfView>();
        _arrive = GetComponent<Arrive>();
        // We will use the start position of the AI as well
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player");
        if (_fieldOfView.visibleTargets.Count != 0)
        {
            _animator.SetBool("isWalking",false);
            _animator.SetBool("isRunning",false);
            _attack.AttackTarget(_fieldOfView.visibleTargets[0].position);
        }
        if (_fieldOfView.visibleTargets.Count == 0)
        {
            _animator.SetBool("isAttacking",false);
            _arrive.SetTarget(playerInfo.transform);
        }
    }
    

    #endregion
}
