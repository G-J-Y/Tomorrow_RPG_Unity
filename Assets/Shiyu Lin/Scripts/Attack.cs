using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LookWhereYouGo))]
public class Attack : MonoBehaviour
{
    #region Private Fields

    private Animator _animator;
    private AudioSource _audioSrc;
    private FieldOfView _fieldOfView;
    private LookWhereYouGo _lookWhereYouGo;
    // Shoot once for every 3 second
    private float fireRate = 2f;
    private float fireRateCounter;

    #endregion

    #region Private Serializable Fields

    [SerializeField] 
    private int damage;
    [SerializeField] 
    private LayerMask targetMask;

    [SerializeField] private Transform targetPosition;
    [SerializeField] private float offset;

    [SerializeField] AudioClip shooting;
    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSrc = GetComponent<AudioSource>();
        _fieldOfView = GetComponent<FieldOfView>();
        _lookWhereYouGo = GetComponent<LookWhereYouGo>();
    }

    private void Start()
    {
        if (damage == 0)
        {
            damage = 2;
        }
    }

    // private void Update()
    // {
    //     AttackTarget(targetPosition.position);
    // }

    #endregion

    #region Custom Methods
    /// <summary>
    /// Character would turn to face at the target, cast a ray toward the target.
    /// Fire rate: 3 second, damage : around 5 each shot
    /// </summary>
    /// <param name="targetPosition"></param>
    public void AttackTarget(Vector3 targetPosition)
    {
        _lookWhereYouGo.LookAtDirection(targetPosition);
        Vector3 offsetedTargetPosition = new Vector3(targetPosition.x, targetPosition.y + offset, targetPosition.z);
        Vector3 targetDirection = offsetedTargetPosition - transform.position;
        Debug.DrawRay(_fieldOfView.offsetedSelfPosition, targetDirection,Color.red,3);
        RaycastHit hit;
        if (Physics.Raycast(_fieldOfView.offsetedSelfPosition, targetDirection, out hit, _fieldOfView.viewRadius + 1f, targetMask))
        {
            if (fireRateCounter >= fireRate)
            {
                if (!_animator.GetBool("isAttacking"))
                {
                    _animator.SetBool("isAttacking",false);
                }
                fireRateCounter = 0f;
            }
            if (fireRateCounter == 0f)
            {
                //Debug.DrawRay(_fieldOfView.offsetedSelfPosition,targetDirection,Color.red,2f);
                _animator.SetBool("isAttacking",true);
                _audioSrc.PlayOneShot(shooting);
                // Deal damage 
                Player player = hit.collider.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
            fireRateCounter += Time.deltaTime;
        }
        else
        {
            fireRateCounter = 0f;
        }
    }

    public void SetTarget(Transform targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    #endregion
}
