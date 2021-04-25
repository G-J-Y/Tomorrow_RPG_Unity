using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script must work together with FieldOfView
[RequireComponent(typeof(FieldOfView))] 
public class LookWhereYouGo : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float targetAngle;
    

    private void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }

    // private void Update()
    // {
    //     LookAtDirection(target.position);
    // }

    public void LookAtDirection(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        AvoidObstacle(direction);
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
            turnSmoothTime);
        // Rotating to look at the target
        transform.rotation = Quaternion.Euler(0f,angle,0f);
    }
    // We will find the field of view (view angle mainly) just by referring to the class FieldOfView
    // we will cast 6 rays and angle between each ray is 18 degrees (the default view angle is 90 degree)
    private void AvoidObstacle(Vector3 direction)
    {
        
        var directionRight= Quaternion.AngleAxis(20, transform.up) * transform.forward;
        var directionLeft = Quaternion.AngleAxis(-20, transform.up) * transform.forward;
        RaycastHit hit;
        
        // // Offset angle
        // float offset = 18f;
        // // Finding the direction of the six rays that will be used for obstacle avoidence
        // var ray1 = Quaternion.AngleAxis(fieldOfView.viewAngle/2, transform.up) * transform.forward;
        // var ray3 = Quaternion.AngleAxis(fieldOfView.viewAngle/2 - offset*2, transform.up) * transform.forward;
        // var ray6 = Quaternion.AngleAxis(-fieldOfView.viewAngle/2 + offset*2, transform.up) * transform.forward;
        
        // Handling obstacle avoidence
        // If the left most ray collide with an obstacle
        if (Physics.Raycast(fieldOfView.offsetedSelfPosition,directionLeft,3f,fieldOfView.obstacleMask))
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 70f;
        }
        else if (Physics.Raycast(fieldOfView.offsetedSelfPosition,transform.forward,4f,fieldOfView.obstacleMask))
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 70f;
        }
        else if (Physics.Raycast(fieldOfView.offsetedSelfPosition,directionRight,3f,fieldOfView.obstacleMask))
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg - 70f;
        }
        else
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        }
        // Debug.DrawRay(fieldOfView.offsetedSelfPosition,directionLeft * 3, Color.cyan);
        // Debug.DrawRay(fieldOfView.offsetedSelfPosition,transform.forward * 4, Color.cyan);
        // Debug.DrawRay(fieldOfView.offsetedSelfPosition,directionRight * 3, Color.cyan);
    }
}
