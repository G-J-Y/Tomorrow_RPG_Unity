using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //camera switch
	//public Transform cameraMain;
    public Transform cameraCinemechine;

    //public Camera cameraMain;
    //public Camera cameraCinemechine;

    private void Start()
    {
        if (cameraCinemechine == null)
        {
            cameraCinemechine = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
    }

    void LateUpdate()
    {
        //if (Input.GetMouseButton(1))
        //{
        //    Vector3 direction = (transform.position- cameraCinemechine.transform.position);
        //    transform.LookAt(direction);

        //    //transform.LookAt(transform.position + cameraCinemechine.transform.forward);
        //    //Debug.Log("Borad Camera switched");
        //    //Debug.Log("Cinemachine: "+cameraCinemechine.transform.position);
        //}

        
        transform.LookAt(transform.position + cameraCinemechine.transform.forward);
            //Debug.Log("Borad Camera1");

    }
}
