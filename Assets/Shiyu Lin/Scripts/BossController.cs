using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    // The boss has 2 phases, in the first phase it only has 2 skills, spitfire and strike
    // once its health is less than 50%, the boss changes to its second phase then increase 
    // to 4 skills (summon, spitfire, strike, wingstrike) and reduce the time interval between
    // each attack

    // Boss is always aware of the position for the player

    #region Private Fields

    private Animator _animator;
    private Health _health;
    private float flameStrikeCounter;
    private float spitFireCounter;
    private float wingStrikeCounter;
    private float summonCounter;
    private GameObject player;
    private BossRotation _bossRotation;
    private BossSummon _bossSummon;
    private bool shouldDo = true;
    

    #endregion

    #region Private Serializable Fields

    [SerializeField] private GameObject flameStrike;
    [SerializeField] private GameObject spitFire;
    [SerializeField] private GameObject wingStrike;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform head;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _bossRotation = GetComponent<BossRotation>();
        _bossSummon = GetComponent<BossSummon>();
    }

    private void Update()
    {
        if (_health.GetCurrentHealth() == 0)
        { 
            _animator.SetBool("shouldStrike",false);
            _animator.SetBool("shouldSpitFire",false);
            _animator.SetBool("shouldSummon",false);
            _animator.SetBool("shouldWingStrike",false);
            _animator.SetBool("shouldDie",true);
            return;
        }
        // Updating the player's information
        player = GameObject.FindGameObjectWithTag("Player");
        // The boss will always look at the player
        _bossRotation.LookAtDirection(player.transform.position);
        // Getting the number of minions present
        GameObject[] numOfMinions = GameObject.FindGameObjectsWithTag("enemy");
        Debug.Log(Vector3.Distance(transform.position,player.transform.position));
        // if the current health for the boss is more than 50%, phase 1
        if (_health.GetCurrentHealth() >= _health.getMaxHealth() * 0.5)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= 28f)
            {
                flameStrikeCounter += Time.deltaTime;
                // strike on a constant time interval
                if (flameStrikeCounter >= 9f)
                {
                    _animator.SetBool("shouldStrike",true);
                    Strike();
                    flameStrikeCounter = 0f;
                }
                else
                {
                    _animator.SetBool("shouldStrike",false);
                }
            }
            else
            {
                spitFireCounter += Time.deltaTime;
                if (spitFireCounter >= 5f)
                {
                    _animator.SetBool("shouldSpitFire",true);
                    StartCoroutine(SpitFire());
                    spitFireCounter = 0f;
                }
                else
                {
                    _animator.SetBool("shouldSpitFire",false);
                }
            }
        }
        // else phase 2
        else if (_health.GetCurrentHealth() < _health.getMaxHealth() * 0.5)
        {
            // The animation for Summon will be used as an indicator for phase 2 as well
            // The animation should only play once
            if (shouldDo)
            {
                _animator.SetBool("shouldSummon",true);
                // Reset all counters
                flameStrikeCounter = 0f;
                spitFireCounter = 0f;
                wingStrikeCounter = 0f;
                summonCounter = 0f;
                shouldDo = false;
            }
            else
            {
                _animator.SetBool("shouldSummon",false);
            }
            // Based on the distance between the boss and the player, it uses different skills
            if (Vector3.Distance(transform.position,player.transform.position) >= 28f)
            {
                flameStrikeCounter += Time.deltaTime;
                if (flameStrikeCounter >= 11.5f)
                {
                    _animator.SetBool("shouldStrike",true);
                    Strike();
                    flameStrikeCounter = 0f;
                }
                else
                {
                    _animator.SetBool("shouldStrike",false);
                }
            }
            else if (Vector3.Distance(transform.position,player.transform.position) < 28f && Vector3.Distance(transform.position,player.transform.position) >= 15f)
            {
                wingStrikeCounter += Time.deltaTime;
                spitFireCounter += Time.deltaTime;
                if (wingStrikeCounter >= 6f)
                {
                    _animator.SetBool("shouldWingStrike",true);
                    WingStrike();
                    wingStrikeCounter = 0f;
                }
                else
                {
                    _animator.SetBool("shouldWingStrike",false);
                }
                if (spitFireCounter >= 6f && player.GetComponent<Player>().shouldSlowDown)
                {
                    _animator.SetBool("shouldSpitFire",true);
                    StartCoroutine(SpitFire());
                    spitFireCounter = 0f;
                }
                else
                {
                    _animator.SetBool("shouldSpitFire",false);
                }
            }
            if (numOfMinions.Length <= 5)
            {
                summonCounter += Time.deltaTime;
                if (summonCounter >= 5f)
                {
                    _animator.SetBool("shouldSummon",true);
                    _bossSummon.SpawnMinions();
                    summonCounter = 0f;
                }
                else
                {
                    _animator.SetBool("shouldSummon",false);
                }
            }
            
        }
    }

    #endregion


    #region Custom
    // instantiate flame on the ground, it would only hit the player once
    private void Strike()
    {
        for (int i = 0; i < 40; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
            GameObject temp = Instantiate(flameStrike, pos, Quaternion.AngleAxis(-90,Vector3.right));
            Destroy(temp,3f);
        }
    }
    
    // Continuous damage, if the player does not touch the flame, stop dealing damamge
    private IEnumerator SpitFire()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject temp = Instantiate(spitFire, head.position, transform.rotation);
        Destroy(temp,1.5f);
    }

    // Once the player touches the smoke, it gets slow down for some time
    private void WingStrike()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
            GameObject temp = Instantiate(wingStrike, pos, Quaternion.identity);
            // Using a raycast to determine if the player is on the flame or not
            Destroy(temp,3f);
        }
    }

    #endregion
}
