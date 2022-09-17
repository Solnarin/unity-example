using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    private Image Healthbar;
    public float currentHealth;
    public float MaxHealth = 200f;
    public Boss_G boss;

    // Start is called before the first frame update
    void Start()
    {
        Healthbar = GetComponent<Image>();
        boss = FindObjectOfType<Boss_G>();
    }

    private void Update()
    {
        currentHealth = boss.Health;
        Healthbar.fillAmount = currentHealth / MaxHealth;
    }

}
