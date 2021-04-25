using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We will handle reduce health function here
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Destroy(this.gameObject,3f);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy" || other.gameObject.tag == "Helper")
        {
            other.gameObject.GetComponent<Health>().UpdateHealth(-15);
        }
        if (other.gameObject.tag == "Drone")
        {
            other.gameObject.GetComponent<Health>().UpdateHealth(-10);
        }
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Health>().UpdateHealth(-15);    
        }
    }
}
