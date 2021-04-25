using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrikeDamage : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }
}
