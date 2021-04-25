using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    #region Private Serializable Fields

    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;
    
    #endregion

    #region Private Fields

    private Arrive _arrive;
    private Attack _attack;
    private FieldOfView _fieldOfView;
    private int patrolNodeCounter;
    private Animator _animator;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        _arrive = GetComponent<Arrive>();
        _attack = GetComponent<Attack>();
        _fieldOfView = GetComponent<FieldOfView>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_fieldOfView.visibleTargets.Count > 0)
        {
            _animator.SetBool("isWalking",false);
            _animator.SetBool("isRunning",false);
            _arrive.target = null;
            _attack.AttackTarget(_fieldOfView.visibleTargets[0].position);
        }
        else
        {
            _animator.SetBool("isAttacking",false);
            if (patrolNodeCounter == 0)
            {
                _arrive.target = pos1;
                if (Vector3.Distance(transform.position,pos1.position) <= 8f)
                {
                    patrolNodeCounter = patrolNodeCounter + 1;
                }
                
            }
            else if (patrolNodeCounter == 1)
            {
                _arrive.target = pos2;
                if (Vector3.Distance(transform.position,pos2.position) <= 8f)
                {
                    patrolNodeCounter = patrolNodeCounter + 1;
                }
            }
            else if (patrolNodeCounter == 2)
            {
                _arrive.target = pos3;
                if (Vector3.Distance(transform.position,pos3.position) <= 8f)
                {
                    patrolNodeCounter = 0;
                }
            }
        }
    }

    #endregion
}
