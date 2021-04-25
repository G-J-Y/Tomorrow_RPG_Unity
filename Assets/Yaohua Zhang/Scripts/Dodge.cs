using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dodge : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private float offset;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         DodgeRoll();
    //     }
    // }

    public void DodgeRoll()
    {
        if(Random.Range(0, 2) == 0)
        {
            Instantiate(effect, new Vector3(transform.position.x,transform.position.y+offset,transform.position.z), Quaternion.identity);
            // To avoid transformed into the map We use current y position since the player
            // might be transformed to a place that has different y position
            transform.position = new Vector3(startPosition.x + Random.Range(-10f, 10f), transform.position.y + 3f,
                startPosition.z + Random.Range(-10f, 10f));
        }
        else
        {
            Instantiate(effect, new Vector3(transform.position.x,transform.position.y+offset,transform.position.z), Quaternion.identity);
            transform.position = new Vector3(startPosition.x + Random.Range(-10f, 10f), transform.position.y + 3f,
                startPosition.z + Random.Range(-10f, 10f));
        }
    }
}
