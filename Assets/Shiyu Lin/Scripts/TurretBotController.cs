using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretBotController : MonoBehaviour
{
    #region Private Fields

    private FieldOfView _fieldOfView;
    private LookWhereYouGo _lookWhereYouGo;
    private Health _health;
    private Attack _attack;
    private Dodge _dodge;
    private float counter;
    private float randomTime;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _lookWhereYouGo = GetComponent<LookWhereYouGo>();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        _dodge = GetComponent<Dodge>();
    }

    private void Start()
    {
        randomTime = Random.Range(2.5f, 4.5f);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        // The turret bot will dodge in a random time interval after it has taken damage
        if (_health.GetCurrentHealth() != _health.getMaxHealth())
        {
            if (counter >= randomTime)
            {
                _dodge.DodgeRoll();
                // if the bot's current health is less than 45% of the maximum health
                // the bot will dodge faster
                if (_health.GetCurrentHealth() <= _health.getMaxHealth() * 0.45)
                {
                    randomTime = Random.Range(1.5f, 2.0f);
                }
                else
                {
                    randomTime = Random.Range(2.5f, 4.5f); 
                }
                counter = 0f;
            }
            
        }
        if (_fieldOfView.visibleTargets.Count > 0)
        {
            _attack.AttackTarget(_fieldOfView.visibleTargets[0].position);
        }
    }

    #endregion
}
