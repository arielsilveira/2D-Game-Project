using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Player properties")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private int dashForce = 1;
    [SerializeField] private bool freezeRotation = true;
    [SerializeField] private int lives = 10;
    [SerializeField] private Rigidbody2D rigidbody;

    [Header("Attack properties")]
    [SerializeField] private float  attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    private IsGroundedChecker isGroundedChecker;
    private InputManager inputManager;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        inputManager = new InputManager();
        inputManager.OnJump += HandleJump;
        playerHealth.SetLives(lives);

    }

    private void Update()
    {
        //It keeps the character stand up
        rigidbody.freezeRotation = freezeRotation;

        float moveDirection = inputManager.Movement;
        if(playerHealth.GetLives() <= 0) return;
        MovePlayer(moveDirection);
        FlipPlayer(moveDirection);

        //DashPlayer(moveDirection);

    }

    private void DashPlayer(float moveDirection){
        transform.Translate(moveDirection * Time.deltaTime * moveSpeed * dashForce, 0, 0);
    }

    private void HandleJump()
    {
        if (isGroundedChecker.IsGrounded() == false) return;
        //rigidbody.linearVelocity += Vector2.up * jumpForce;
        rigidbody.AddForce(new Vector2(rigidbody.linearVelocity.x, jumpForce));
    }
    
    private void MovePlayer(float moveDirection)
    {
        //transform.Translate(moveDirection * Time.deltaTime * moveSpeed, 0, 0);
        Vector2 currentVelocity = rigidbody.linearVelocity;
        rigidbody.linearVelocity = new Vector2(moveDirection * moveSpeed, rigidbody.linearVelocity.y);
    }

    private void FlipPlayer(float moveDirection)
    {
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Attack()
    {
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            if (hittedEnemy.TryGetComponent(out EnemyHealth enemyHealth))
            {
               int damage = 1;
               int criticalDamage = Random.Range(1, 10);

               if(criticalDamage <= 1) damage = 10;
               enemyHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
