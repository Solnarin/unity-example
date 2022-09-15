using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Image Healthbar;
    public float currentHealth;
    public float MaxHealth = 100f;
    Player_G player;

    // Start is called before the first frame update
    void Start()
    {
        Healthbar = GetComponent<Image>();
        player = FindObjectOfType<Player_G>();
    }

    private void Update()
    {
        currentHealth = player.Health;
        Healthbar.fillAmount = currentHealth / MaxHealth;
    }

}
