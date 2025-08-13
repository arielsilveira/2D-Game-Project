using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class BossFightTrigger : MonoBehaviour
{
    public event Action OnPlayerEnterBossFight;
    
    [SerializeField] private GameObject bossDoor;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            OnPlayerEnterBossFight?.Invoke();
            //bossDoor.SetActive(true);
        }
    }
}
