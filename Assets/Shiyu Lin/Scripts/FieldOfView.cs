using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // View radius is 7 and view angle is 90
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    public LayerMask obstacleMask;
    public LayerMask targetMask;
    public List<Transform> visibleTargets = new List<Transform>();
    // will be used for raycast, since the pivot of the character is on the foot.
    public Vector3 offsetedSelfPosition;
    public float offset;

    private void Update()
    {
        offsetedSelfPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        FindVisibleTarget();
    }

    // Find the target within the given view radius and view angle
    void FindVisibleTarget()
    {
        // Remove deplicates
        visibleTargets.Clear();
        // General idea: Find all targets with in the view radius, then loop through them and check by using raycast
        // if they are blocked by any obstacles, if they are then we just ignore it, if they are not, we cast a ray there
        // to indicate it is visible and store it into a list.
        Collider[] objectsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < objectsInViewRadius.Length; i++)
        {
            Transform target = objectsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // if the angle between the character's forward direction and the direction of the target is 
            // within the view angle, we should consider them as within view angle.
            // Otherwise they are just within the view radius but should not be visible
            if (Vector3.Angle(transform.forward,dirToTarget) < viewAngle/2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(offsetedSelfPosition,dirToTarget,disToTarget,obstacleMask))
                {
                    //Debug.DrawRay(offsetedSelfPosition,dirToTarget * 10, Color.magenta, 1f);
                    visibleTargets.Add(target);
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
         var directionRight= Quaternion.AngleAxis(viewAngle/2, transform.up) * transform.forward;
         var directionLeft = Quaternion.AngleAxis(-viewAngle/2, transform.up) * transform.forward;
         Gizmos.DrawRay(offsetedSelfPosition,directionLeft * 7);
         Gizmos.DrawRay(offsetedSelfPosition,directionRight * 7);
         Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
