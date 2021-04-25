using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockAttack : MonoBehaviour
{
    #region Private Fields

    private Animator _animator;
    
    // Shoot once for every 3 second
    private float fireRate = 3f;
    private float fireRateCounter;

    #endregion

    #region Private Serializable Fields

    [SerializeField] 
    private int damage;
    [SerializeField] 
    private LayerMask targetMask;

    [SerializeField] private Transform targetPosition;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
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
    public void AttackTarget(Vector3 targetPosition,float distance,GameObject p)
    {
        
        Vector3 targetDirection = targetPosition - transform.position;
        transform.forward=targetDirection;
        _animator.SetBool("isAttacking",true);
        /*Player player = p.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }*/
        
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
                // Deal damage 
                Player player = p.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
            fireRateCounter += Time.deltaTime;
    }
        

    public void SetTarget(Transform targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    #endregion
}
