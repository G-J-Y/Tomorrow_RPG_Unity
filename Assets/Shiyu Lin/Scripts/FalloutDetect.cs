using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalloutDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().shouldGoback = true;
        }
        
    }
}
