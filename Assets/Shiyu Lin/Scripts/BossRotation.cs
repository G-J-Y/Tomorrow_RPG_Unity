using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float targetAngle;
    
    public void LookAtDirection(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
            turnSmoothTime);
        // Rotating to look at the target
        transform.rotation = Quaternion.Euler(0f,angle,0f);
    }
}
