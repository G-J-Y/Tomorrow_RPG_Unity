using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rescue : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform patient;
    public bool available = true;

    // void Update()
    // {
    //     if (patient == null)
    //         available = true;
    // }

    public void goRescue()
    {
        Debug.Log("Moving");
        if (available)
        {
            GetComponent<Arrive>().target = patient;
            available = false;
        }
            
    }
}
