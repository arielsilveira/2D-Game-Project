using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [Header("Attack properties")]
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform launchPoint;

    [Header("Audio properties")] 
    [SerializeField] private AudioClip[] audioClips;

    private float cooldownTimer;

    protected override void Update()
    {
        cooldownTimer += Time.deltaTime;
        VerifyCanAttack();
    }

    private void VerifyCanAttack()
    {
        if (cooldownTimer < attackCooldown || canAttack == false) return;
        var detectedArea = CheckPlayerInDetectArea();

        if(detectedArea != null && detectedArea.TryGetComponent(out PlayerHealth playerHealth) && playerHealth.GetLives() > 0)
        {
            animator.SetTrigger("attack");
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        cooldownTimer = 0;
        if(CheckPlayerInDetectArea().TryGetComponent(out PlayerHealth playerHealth))
        {
            print("Player hit");
            Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private bool PlayerInSight()
    {
        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }
}