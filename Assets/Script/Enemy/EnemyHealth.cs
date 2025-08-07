using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int lives = 10;

    public event Action OnDead;
    public event Action OnHurt;

    public void TakeDamage(int damage)
    {
        lives -= damage;
        HandleDamageTaken();
    }

    private void HandleDamageTaken()
    {
        if(lives < 0)
        {
            OnDead?.Invoke();
        } 
        else
        {
            OnHurt?.Invoke();
        }
    }

    public void SetLives(int maxLives)
    {
        lives = maxLives;
    }

    public int GetLives()
    {
        return lives;
    }
}
