using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerStatsController : MonoBehaviour
{
    public Image healthBar;
    public float currentHealth;
    public float maxHealth = 100f;


    public void SetCurrentHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    public void Damage(float damage)
    {
        currentHealth = currentHealth - damage;
    }



    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

}
