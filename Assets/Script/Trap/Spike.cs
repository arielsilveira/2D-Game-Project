using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision");
        if(collision.gameObject.tag != "Player") return;
        collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
    }
}
