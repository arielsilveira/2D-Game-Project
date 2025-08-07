using UnityEngine;

public class CollectableGem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.UpdateGemsLeft();
            Destroy(gameObject);
        }
    }
}
