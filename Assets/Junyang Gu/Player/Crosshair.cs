using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Camera cam;
    float distanceFromCamera = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 position = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        //var crosshair = GameObject.Find("crosshair");
        //crosshair.transform.position = new Vector3(position.x, position.y, 0);

        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera);
        position = cam.ScreenToWorldPoint(position);

        transform.position = position;

        transform.LookAt(transform.position + cam.transform.forward);
    }
}
