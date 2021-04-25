using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class steeringSeek : MonoBehaviour
{
    [SerializeField]
    private float currentVelocityMagnitude;
    private Vector3 velocity;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float maxAcceleration;
    public Transform target;
    [SerializeField] 
    private float drag;
    [SerializeField]
    private float arriveRadius = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        velocity = currentVelocityMagnitude * transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            seek();
        }
    }

    public void seek()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float distance = (target.position - transform.position).magnitude;
            Vector3 acceleration = direction * maxAcceleration;
            
            if(distance<=arriveRadius){
                velocity=Vector3.zero;
                //target = null;
            }
            else
            {
                if ((velocity + acceleration * Time.deltaTime).magnitude < maxVelocity)
                {
                    velocity += acceleration * Time.deltaTime;
                }
                else
                {
                    Vector3 dragDirection = -velocity.normalized;
                    Vector3 deceleration = dragDirection * drag;
                    velocity += deceleration * Time.deltaTime;
                }
            }

            transform.forward = velocity;
            transform.position += velocity * Time.deltaTime;
        }
        

    }
}
