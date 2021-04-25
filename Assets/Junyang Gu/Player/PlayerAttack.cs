using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAttack : MonoBehaviour
{
    public CharacterController controller;

    public GameObject bulletPrefab;
    //public Transform player;
    public Camera playerCamera;

    public Vector3 bulletOffset = new Vector3(0,1,0);
    float fireRate = 4f;
    float nextFireTime = 0f;

    //Animation
    private static readonly int Attacking = Animator.StringToHash("isAttacking");
    Animator animator;

    // audio
    private AudioSource _audioSrc;
    [SerializeField] AudioClip PlayerShooting;


    private void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool(Attacking, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.canShoot)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFireTime && controller.isGrounded)
            {
                //fireRate = how many bullet per second
                nextFireTime = Time.time + 1f / fireRate;
                Fire();

            }
            if (Input.GetButtonUp("Fire1"))
            {
                //animator.SetBool(Attacking, false);
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        _audioSrc.PlayOneShot(PlayerShooting);
        //bullet.transform.position = player.position + player.forward + bulletOffset;
        //bullet.transform.forward = player.forward;

        ////bulletOffset = new Vector3(0,0, distance);
        
        //float distance = (playerCamera.transform.position - transform.position).magnitude;
        bullet.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
        bullet.transform.forward = playerCamera.transform.forward;

        //animator.SetBool(Attacking, true);
    }
}
