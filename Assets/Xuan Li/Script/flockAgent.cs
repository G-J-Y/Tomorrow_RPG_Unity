using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(steeringSeek))]
public class flockAgent : MonoBehaviour
{
    public float repulsionQueryRadius = 5.0f;
    public float cohesionQueryRadius = 10.0f;
    public float cohesionFactor = 1.5f;
    public float repulsionFactor = 2.0f;
    public float alignmentFactor = 1.0f;
    
    //parameter to let flock go back in range after target get lost
    public Transform center;
    public float radiusOfActivity = 15f;
    public float stayFactor = 0.3f;
    
    //parameter to smooth behaviour
    private Vector3 currentVelocity;
    public float smoothTime = 0.5f;
    
    //set a speed limit to ensure they don't move super fast
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        Composite();
    }
    
    Vector3 Cohesion()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, cohesionQueryRadius);
        Vector3 middlePoint=Vector3.zero;

        foreach (Collider neighbor in nearbyColliders)
        {
            if(neighbor.CompareTag("enemy"))
                middlePoint += neighbor.transform.position;
        }

        middlePoint /= nearbyColliders.Length;

        Vector3 cohesionOffset = (middlePoint - transform.position) * cohesionFactor;
        //cohesionOffset = Vector3.SmoothDamp(transform.forward, cohesionOffset, ref currentVelocity, smoothTime);

        return cohesionOffset;
        //transform.position += cohesionOffset * Time.deltaTime;
    }

    
    Vector3 Repulsion()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, repulsionQueryRadius);
        Vector3 middlePoint=Vector3.zero;
        
        foreach (Collider neighbor in nearbyColliders)
        {
            if(neighbor.CompareTag("enemy"))
                middlePoint += neighbor.transform.position;
        }
        
        middlePoint /= nearbyColliders.Length;

        Vector3 repulsionOffset = -(middlePoint - transform.position) * repulsionFactor;

        return repulsionOffset;
        //transform.position+=repulsionOffset* Time.deltaTime;

        //GetComponent<Rigidbody>().AddForce(repulsionOffset);
    }

    
    void Alignment()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, cohesionQueryRadius);
        Vector3 avgHeading=Vector3.zero;
        
        foreach (Collider neighbor in nearbyColliders)
        {
            if(neighbor.CompareTag("enemy"))
                avgHeading += neighbor.transform.forward;
        }
        
        avgHeading/=nearbyColliders.Length;
        //avgHeading += velocity;
        Quaternion goal = Quaternion.LookRotation(avgHeading, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goal, alignmentFactor);
    }

    /*Vector3 stayInRadius()
    {
        Vector3 centerOffset = center.position - transform.position;
        float distanceToCenter = centerOffset.magnitude;
        float ratio = distanceToCenter / radiusOfActivity;
        if (ratio < 0.9)
        {
            return Vector3.zero;
        }

        return centerOffset * stayFactor;
        //transform.position += centerOffset * (stayFactor * Time.deltaTime);
    }*/

    public void Composite()
    {
        Vector3 velocity = Cohesion() + Repulsion() ;
        
        if (velocity.magnitude > speed)
            velocity = velocity.normalized * speed;
        
        transform.position += velocity * Time.deltaTime;
        //velocity.Normalize();
        Alignment();
    }
}
