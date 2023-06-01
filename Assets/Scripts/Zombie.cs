using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private const string IS_ATTACKING = "isAttacking";
    private const string IS_RUNNING = "isRunning";
    private const string IS_HIT = "isHit";

    private readonly float lookRange = 13f;
    private NavMeshAgent agent;
    private Transform target;
    private bool isMoved = false;
    private float attackSpeed;
    private int health = 100000;
    private int maxHealth;
    private int damage;
    private float attackTimer;
    private int zombiePoint;
    [SerializeField] HealthBar healthBar;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        SoldierCreator.Instance.OnCreatedNewSoldiers += SoldierCreator_OnCreatedNewSoldiers;
    }
    public void InitializeZombie(ZombieSO zombieSO)
    {
        attackSpeed = zombieSO.attackSpeed;
        SetHealth(zombieSO.health);
        damage = zombieSO.damage;
        zombiePoint = zombieSO.zombiePointRequired;
        agent.speed = zombieSO.movementSpeed;
    }
    private void SoldierCreator_OnCreatedNewSoldiers(object sender, System.EventArgs e)
    {
        if (isMoved)
        {
            Destroy(gameObject);
        }
    }
    public void SetHealth(int _health)
    {
        maxHealth = _health;
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void OnDestroy()
    {
        SoldierCreator.Instance.OnCreatedNewSoldiers -= SoldierCreator_OnCreatedNewSoldiers;
    }


    private void FixedUpdate()
    {
        CheckTarget();
    }

    private void CheckTarget()
    {
        if (target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, lookRange);
            foreach (Collider collider in colliders)
            {
                if (collider.transform.TryGetComponent(out Soldier soldier))
                {
                    // Soldier detected from zombie
                    animator.SetBool(IS_RUNNING, true);
                    target = soldier.transform;
                    agent.SetDestination(target.position);
                    isMoved = true;
                    break;
                }
            }
        }
        if (target != null)
        {
            // Chasing the soldier
            agent.SetDestination(target.position);
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= agent.stoppingDistance && attackTimer >= attackSpeed)
            {
                // Attack the soldier
                ResetAfterPlayingAnimation(IS_ATTACKING);
                target.GetComponent<Soldier>().GetDamage(damage);
                attackTimer = 0;
            }
        }
        attackTimer += Time.fixedDeltaTime;
    }

    public void GetDamage(int damage)
    {
        //reset hit animation after play it
        ResetAfterPlayingAnimation(IS_HIT);
        // get damage
        health -= damage;
        if (health <= 0)
            //health is lower than 0 so zombie is dead 
            Die();
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    public int GetHealth()
    {
        return health;
    }
    private void Die()
    {
        transform.GetComponentInParent<ZombiePlatform>().DecreaseZombiePoint(zombiePoint);
        PointManager.Instance.IncreaseGamePoint(zombiePoint);
        Destroy(gameObject);
    }
    public void ResetAfterPlayingAnimation(string animation)
    {
        animator.SetBool(animation, true);
        StartCoroutine(ResetAnimation(animation));
    }
    private IEnumerator ResetAnimation(string animation)
    {
        yield return new WaitForEndOfFrame();

        animator.SetBool(animation, false);
    }
}
