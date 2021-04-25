using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject effect;
    [SerializeField] private bool isNoDamageSkill;
    [SerializeField] private float offset;

    [SerializeField] AudioClip dead;


    private void Awake()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        isNoDamageSkill = false;
    }


    void Update()
    {
        if (GetComponent<NoDamageSkill>() != null)
           isNoDamageSkill = GetComponent<NoDamageSkill>().GetIfNoDamageSkill();

        if (currentHealth <= 0 && gameObject.tag != "Boss")
        {
            effect.SetActive(true);
            AudioSource.PlayClipAtPoint(dead, transform.position);
            Instantiate(effect, new Vector3(transform.position.x,transform.position.y+offset,transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(int value)
    {
        if (!isNoDamageSkill)
        {
            currentHealth += value;
            healthBar.SetHealth(currentHealth);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
        healthBar.SetHealth(currentHealth);
    }

    public void SetIsNoDamageSkill(bool boolValue)
    {
        this.isNoDamageSkill = boolValue;
    }
}
