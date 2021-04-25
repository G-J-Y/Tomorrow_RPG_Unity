using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField] private GameObject explodeEffect;
    private float counter;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 9f)
        {
            _health.UpdateHealth(-10);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(10);
            Instantiate(explodeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
