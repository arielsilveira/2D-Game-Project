using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker groundedChecker;
    private PlayerHealth playerHealth;
    private bool isAttacking = false;
    private bool isMoving = false;
    private bool isJumping = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<PlayerHealth>();

        playerHealth.OnHurt += PlayerHurtAnim;
        playerHealth.OnDeath += PlayerDeathAnim;
    }

    private void Start()
    {
        //Try to fix it later (move to awake)
        GameManager.Instance.InputManager.OnAttack += PlayerAttackAnim;
        GameManager.Instance.InputManager.OnDash += PlayerDashAnim;
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Death")) return;

        isJumping = !groundedChecker.IsGrounded();

        PlayerMovimentAnim(stateInfo);
        animator.SetBool("isJumping", isJumping);
    }

    private void PlayerHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    private void PlayerDeathAnim()
    {
        animator.SetTrigger("death");
    }

    private void PlayerDashAnim()
    {
        animator.SetTrigger("dash");
    }

    private void PlayerMovimentAnim(AnimatorStateInfo stateInfo)
    {
        if (stateInfo.IsName("Attack"))
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            isMoving = GameManager.Instance.InputManager.Movement != 0;
            animator.SetBool("isMoving", isMoving);
        }
    }

    private void PlayerAttackAnim()
    {
        if((!isAttacking && !isMoving) || (!isAttacking && isJumping))
        {
            isAttacking = true;
            animator.ResetTrigger("attack");
            animator.SetTrigger("attack");
        }
    }

    private void PlayerIsNotAttacking()
    {
        isAttacking = false;
    }
}
