using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWind : MonoBehaviour
{
    public bool inWindZone = false;
    public GameObject windZone;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (inWindZone)
        {
            rb.AddForce(windZone.GetComponent<windarea>().direction * windZone.GetComponent<windarea>().strength);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "windarea")
        {
            windZone = coll.gameObject;
            inWindZone = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "windarea")
        {
            inWindZone = false;
        }
    }


}
