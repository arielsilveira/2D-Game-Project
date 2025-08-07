using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;
    protected Animator animator;
    protected EnemyHealth health;
    protected bool canAttack = true;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();

        health.OnHurt += HandleHurt;
        health.OnDead += HandleDeath;
    }

    protected abstract void Update();

    private void HandleHurt()
    {
        animator.SetTrigger("hurt");
        PlayHitParticle();
    }

    private void HandleDeath()
    {
        canAttack = false;
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("death");
        PlayHitParticle();
        StartCoroutine(DestroyEnemy(2));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time * 0.5f);
        Destroy(this.gameObject);
    }

    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle  = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
    }
}
