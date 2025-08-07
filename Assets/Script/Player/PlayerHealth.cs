using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int lives;
    public event Action OnDeath;
    public event Action OnHurt;
    public Slider healthBarSlider;
    public Gradient gradient;
    private bool isDead;

    void Awake()
    {
        isDead = false;
    }

    public void SetMaxHealth(int maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        SetHealth(lives);
        HandleDamageTaken();
    }    

    private void HandleDamageTaken()
    {
        if(lives <= 0 && !isDead)
        {   
            isDead = true;
            OnDeath?.Invoke();
        } 
        else if(lives > 0)
        {
            OnHurt?.Invoke();
        }
    }

    public void SetLives(int maxLives)
    {
        lives = maxLives;
        SetMaxHealth(lives);
    }

    public float GetLives()
    {
        return healthBarSlider.value;
    }
}
