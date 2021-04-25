using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoDamageSkill : MonoBehaviour
{
    //[SerializeField] private int healthValueToDodge;
    [SerializeField] private bool isNoDamageSkill;
    [SerializeField] private GameObject effect;
    [SerializeField] private float skillTimer;
    [SerializeField] private float skillTimeLeft;
    //[SerializeField] private int constHealth;


    private void Awake()
    {
        //healthValueToDodge = 90;
        isNoDamageSkill = false;
        skillTimer = 10f;
    }
    

    void Update()
    {
        //if (GetComponent<Health>().GetCurrentHealth() <= healthValueToDodge)
        //{
        //    skillTimer += Time.deltaTime;
        //}
        skillTimer += Time.deltaTime;

        if (/*GetComponent<Health>().GetCurrentHealth() <= healthValueToDodge && */!isNoDamageSkill && skillTimer >= 10f)
        {
            skillTimer = 0f;
            skillTimeLeft = 5f;
            effect.GetComponent<ParticleSystem>().Play();
            isNoDamageSkill = true;
            //constHealth = GetComponent<Health>().GetCurrentHealth();
        }

        if (isNoDamageSkill)
        {
            skillTimeLeft -= Time.deltaTime;
            //GetComponent<Health>().SetCurrentHealth(constHealth);

        }

        if (skillTimeLeft <= 0)
        {
            effect.GetComponent<ParticleSystem>().Stop();
            isNoDamageSkill = false;
        }

    }

    public bool GetIfNoDamageSkill()
    {
        return isNoDamageSkill;
    }

}
