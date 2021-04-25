using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamthrowerDamage : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("aaaaa");
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }
}
