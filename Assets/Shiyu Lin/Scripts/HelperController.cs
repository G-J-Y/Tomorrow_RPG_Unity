using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperController : MonoBehaviour
{
    #region Private Fields

    private Attack _attack;
    private Arrive _arrive;
    private Return _return;
    private FieldOfView _fieldOfView;
    private rescue _rescue;
    private Animator _animator;
    private Vector3 startPosition;
    private float timer;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        startPosition = transform.position;
        _attack = GetComponent<Attack>();
        _arrive = GetComponent<Arrive>();
        _return = GetComponent<Return>();
        _rescue = GetComponent<rescue>();
        _fieldOfView = GetComponent<FieldOfView>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_fieldOfView.visibleTargets.Count > 0)
        {
            _arrive.target = null;
            _rescue.patient = null;
            _animator.SetBool("isWalking",false);
            _animator.SetBool("isRunning",false);
            _attack.AttackTarget(_fieldOfView.visibleTargets[0].position);
        }
        else if (_rescue.patient == null && !_rescue.available && _fieldOfView.visibleTargets.Count == 0)
        {
            _animator.SetBool("isAttacking",false);
            _return.target = startPosition;
            _return.CalculateAcceleration();
            _return.ApplyAcceleration();
            if (Vector3.Distance(transform.position,startPosition) <= 5f)
            {
                _rescue.available = true;
                _animator.SetBool("isWalking",false);
                _animator.SetBool("isRunning",false);
            }
        }
        
    }

    #endregion
}
