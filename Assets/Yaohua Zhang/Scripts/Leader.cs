using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Leader : MonoBehaviour
{
    [SerializeField] private bool isSkill;
    [SerializeField] private List<GameObject> objectsWithSkill;
    private FieldOfView _fieldOfView;
    private LookWhereYouGo _lookWhereYouGo;
    private Attack _attack;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        objectsWithSkill = new List<GameObject>();
        isSkill = false;
        _fieldOfView = GetComponent<FieldOfView>();
        _lookWhereYouGo = GetComponent<LookWhereYouGo>();
        _attack = GetComponent<Attack>();
    }

    void Update()
    {
        if (_fieldOfView.visibleTargets.Count > 0)
        {
            _animator.SetBool("isAttacking",true);
            _attack.AttackTarget(_fieldOfView.visibleTargets[0].position);
        }
        else
        {
            _animator.SetBool("isAttacking",false);
        }
        CheckSkill();
    }

    void CheckSkill()
    {
        if (GetComponent<NoDamageSkill>().GetIfNoDamageSkill() && !isSkill)
        {
            TeamSkillNoDamage(transform.position, 80f);
        }
        
        if (!GetComponent<NoDamageSkill>().GetIfNoDamageSkill())
        {
            CancleTeamSkillNoDamage();
        }
    }

    void TeamSkillNoDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            Health temp = hitCollider.GetComponent<Health>();
            if(hitCollider.tag == "enemy" && temp.GetCurrentHealth() < temp.getMaxHealth())
            {
                objectsWithSkill.Add(hitCollider.gameObject);
                GameObject effect = hitCollider.transform.Find("CFX4 Aura Bubble C").gameObject;
                effect.GetComponent<ParticleSystem>().Play();
                hitCollider.GetComponent<Health>().SetIsNoDamageSkill(true);
            }
        }
        isSkill = true;
        
    }

    void CancleTeamSkillNoDamage()
    {
        isSkill = false;
        foreach (var objectWithSkill in objectsWithSkill)
        {
            GameObject effect = objectWithSkill.transform.Find("CFX4 Aura Bubble C").gameObject;
            if (effect != null)
            {
                effect.GetComponent<ParticleSystem>().Stop();
                objectWithSkill.GetComponent<Health>().SetIsNoDamageSkill(false);
            }
        }
        // after cancelling the buff, we need to clear the list
        // or if an object was destroyed it would give null reference error
        objectsWithSkill.Clear();
    }
}
