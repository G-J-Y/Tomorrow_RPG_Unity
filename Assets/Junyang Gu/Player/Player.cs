using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform camera;

    public float playerSpeed = 5f;
    public float rotateSpeed = 5f;
    public bool shouldGoback;
    float smoothTime = 0.1f;
    float smoothVelocity;

    float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    Vector3 velocity;

    //Health
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;
    //health recover
    public float healthrecover = 5f;
    bool canRecoverHealth = false;
    bool isRecoverHealth = false;
    float timeNotBeAttacked = 0f;
    float timeConfirmSafety = 4f;

    //Player slow down effect indicator, once a player is hit by slow smoke, reduce the speed of the
    //player by 50% for 2.5 second
    public bool shouldSlowDown;
    private float slowDownCounter;
    private float playerOriginalSpeed;

    //Animation
    private static readonly int Running = Animator.StringToHash("isRunning");
    private static readonly int Jump = Animator.StringToHash("isJumping");
    Animator animator;

    // Third person camera without cinemechine
    Vector2 rotation = Vector2.zero;
    //public Transform playerCamera;
    public float mouseSensitivity = 1.5f;
    public float lookXLimit = 60.0f;

    //FPS
    float maxYRotation = 45;
    float minYRotation = -45;
    float rotateX = 0.0f;
    float rotateY = 0.0f;
    [SerializeField] Canvas canvasFPS;
    public static bool canShoot = false; 

    //camera switch
    [SerializeField] Camera CameraMain;
    [SerializeField] Camera CameraCinemechine;
    [SerializeField] Camera CameraFPS;

    //Restart Point
    GameObject restartPos;

    //audio
    private AudioSource _audioSrc;
    [SerializeField] AudioClip foot_step;
    [SerializeField] AudioClip hurt;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller = this.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        _audioSrc = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerOriginalSpeed = playerSpeed;

        //camera switch
        CameraMain.enabled = false;
        CameraCinemechine.enabled = true;
        CameraFPS.enabled = false;

        //Canvas
        canvasFPS.enabled = false;

        //Restart
        restartPos = GameObject.FindWithTag("Restart");
        Debug.Log("Restart Position = " + restartPos.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // With cinemachine
        if (CameraCinemechine.enabled)
        {
            MoveWithCinemachine(direction);
        }
        if (CameraFPS.enabled)
        {
            //MoveWithFPS();
            rotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotation.x += -Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            CameraFPS.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
        

        if (shouldSlowDown)
        {
            slowDownCounter += Time.deltaTime;
            playerSpeed = 2.5f;
            if (slowDownCounter >= 3.5f)
            {
                slowDownCounter = 0f;
                shouldSlowDown = false;
                playerSpeed = playerOriginalSpeed;
            }
        }

        if (shouldGoback)
        {
            controller.enabled = false;
            this.transform.position = restartPos.transform.position;
            controller.enabled = true;
            shouldGoback = false;
        }
        
        // WithOut cinemachine
        //if (direction.magnitude >= 0.1f)
        //{
        //    //world xyz
        //    Vector3 moveDirection = transform.TransformDirection(direction * Time.deltaTime * playerSpeed);
        //    controller.Move(moveDirection);
        //    animator.SetBool(Running, true);
        //}
        //else
        //{
        //    animator.SetBool(Running, false);
        //}

        // Player and Camera rotation
        //if (CameraMain.enabled)
        //{
        //    rotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;
        //    rotation.x += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        //    rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        //    playerCamera.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        //    transform.eulerAngles = new Vector2(0, rotation.y);
        //}

        
        //camera switch
        if (Input.GetMouseButtonDown(1))
        {
           // Debug.Log("Active the second camera.");
            //CameraMain.enabled = !CameraMain.enabled;
            CameraFPS.enabled = !CameraFPS.enabled;
            CameraCinemechine.enabled = !CameraCinemechine.enabled;
            canvasFPS.enabled = !canvasFPS.enabled;
            canShoot = true;
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            //Debug.Log("Back to the first camera.");
            //CameraMain.enabled = !CameraMain.enabled;
            CameraFPS.enabled = !CameraFPS.enabled;
            CameraCinemechine.enabled = !CameraCinemechine.enabled;
            canvasFPS.enabled = !canvasFPS.enabled;
            canShoot = false;
        }

        // Jump
        //Debug.Log(controller.isGrounded);
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
            animator.SetBool(Jump, true);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y<0)
        {
            velocity.y = -1f;
            animator.SetBool(Jump, false);
        }

        // Recover health
        timeNotBeAttacked += Time.deltaTime;
        if (timeNotBeAttacked > timeConfirmSafety)
        {
            canRecoverHealth = true;
        }
        if (canRecoverHealth && !isRecoverHealth)
        {
            StartCoroutine(RegainHealthOverTime());
        }

        // test take damage
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(10);
        }

        if (currentHealth <= 0f)
        {
            RestartLevel();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        _audioSrc.PlayOneShot(hurt);
        //
        timeNotBeAttacked = 0f;
        canRecoverHealth = false;
        Debug.Log("Stop Health Recover, Current health = "+currentHealth);
    }


    void RestartLevel()
    {
        controller.enabled = false;
        this.transform.position = restartPos.transform.position;
        controller.enabled = true;
        Debug.Log("Restart");
        currentHealth = maxHealth;
        Debug.Log("Current health = " + currentHealth);
        healthBar.SetHealth(maxHealth);
    }

    void MoveWithCinemachine(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z) + camera.eulerAngles.y;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, angle, 0f), rotateSpeed);
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = (Quaternion.Euler(0f, smoothAngle, 0f) * Vector3.forward).normalized;
            controller.Move(moveDirection * Time.deltaTime * playerSpeed);

            animator.SetBool(Running, true);

        }
        else
        {
            animator.SetBool(Running, false);
        }
    }

    void MoveWithFPS()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        rotateX += camera.transform.localEulerAngles.y + rotateHorizontal * mouseSensitivity;
        rotateY -= rotateVertical * mouseSensitivity;
        rotateY = Mathf.Clamp(rotateY, minYRotation, maxYRotation);

        transform.eulerAngles = new Vector3(0, rotateX, 0);
        camera.transform.eulerAngles = new Vector3(rotateY, rotateX, 0);
    }



    IEnumerator RegainHealthOverTime()
    {
        isRecoverHealth = true;
        while (currentHealth < maxHealth)
        {
            Healthrecover();
            yield return new WaitForSeconds(0.5f);
        }
        isRecoverHealth = false;
    }

    void Healthrecover()
    {
        //while (currentHealth < maxHealth)
        //{
            currentHealth += healthrecover;
            healthBar.SetHealth(currentHealth);
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Potion")
        {
            Destroy(other.gameObject);
            currentHealth += 20;
            healthBar.SetHealth(currentHealth);
        }
    }

    private void PlayStepSound()
    {
        _audioSrc.PlayOneShot(foot_step);
    }
}
