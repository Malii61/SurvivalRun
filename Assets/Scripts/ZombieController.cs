using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private float range = 4f;
    private NavMeshAgent agent;
    private Transform target;
    private bool isMoved = false;
    private float attackSpeed;
    private int health;
    private int maxHealth;
    private int damage;
    private float attackTimer;
    [SerializeField] HealthBar healthBar;
    private void Awake()
    {
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
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliders)
            {
                if (collider.transform.TryGetComponent(out SoldierManager soldier))
                {
                    Debug.Log(soldier.transform.name);
                    target = soldier.transform;
                    agent.SetDestination(target.position);
                    isMoved = true;
                    break;
                }
            }
        }
        if (target != null)
        {
            agent.SetDestination(target.position);
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= agent.stoppingDistance && attackTimer >= attackSpeed)
            {
                target.GetComponent<SoldierManager>().GetDamage(damage);
                attackTimer = 0;
            }
        }
        attackTimer += Time.fixedDeltaTime;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= damage)
            Die();
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
