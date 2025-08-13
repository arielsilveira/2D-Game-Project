using UnityEngine;

public class BossBehaviour : MonoBehaviour {
    private Rigidbody2D rigidbody;
    private Transform playerPosition;
    private bool canAttack = false;
    private bool isFlipped = true;
    private Vector3 attackPosition;
    private Animator animator;

    [Header("Boss Behaviour")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private GameObject projectilePrefab;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerPosition = GameManager.Instance.GetPlayer().transform;
        animator = GetComponent<Animator>();
    }

    public void FollowPlayer()
    {
        Vector2 target = new Vector2(playerPosition.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);
        LookAtPlayer();
        CheckDistanceFromPlayer();
    }

    private void CheckDistanceFromPlayer() 
    {
        float distanceFromPlayer = Vector2.Distance(playerPosition.position, transform.position);
        if (distanceFromPlayer <= attackRange)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    private void Attack()
    {
        attackPosition = transform.position;
        attackPosition += transform.right * attackOffset.x;
        attackPosition += transform.up * attackOffset.y;

        Collider2D collisionInfo = Physics2D.OverlapCircle(attackPosition, attackRange, attackMask);
        if (collisionInfo != null)
        {
            collisionInfo.GetComponent<PlayerHealth>().TakeDamage(2);
        }
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;
        if (transform.position.x > playerPosition.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
            else if (transform.position.x < playerPosition.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void StartChasing()
    {
       animator.SetBool("canChase", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition, attackRange);
    }
}