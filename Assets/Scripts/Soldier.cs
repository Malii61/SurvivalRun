using UnityEngine;
public class Soldier : MonoBehaviour
{
    private readonly float attackRange = 10f;
    private int health;
    private int maxHealth;
    private float fireRate;
    private int soldierPoint;
    private float fireRateTimer;
    private int damage;
    private Quaternion firstRotation;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] SoldierAnimationController soldierAnimationController;
    [SerializeField] Gun gun;
    private Transform target;
    private readonly float rotationDamping = 20f;
    private void Start()
    {
        firstRotation = transform.rotation;
    }
    public void InitializeSoldier(SoldierSO soldierSO)
    {
        SetHealth(soldierSO.health);
        soldierPoint = soldierSO.soldierPointRequired;
        fireRate = soldierSO.fireRate;
        damage = soldierSO.damage;
    }
    private void FixedUpdate()
    {
        CheckZombiesAround();

        if (target != null)
            transform.LookAt(target);
        else
            RotateSoldier(firstRotation);
    }

    private void CheckZombiesAround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.TryGetComponent(out Zombie zombie))
            {
                if (zombie.GetHealth() <= 0)
                    continue;
                target = zombie.transform;
                if (fireRateTimer >= fireRate)
                {
                    gun.Fire(zombie.transform.position);
                    zombie.GetDamage(damage);
                    fireRateTimer = 0;
                }
                break;
            }
        }
        fireRateTimer += Time.fixedDeltaTime;
    }

    private void RotateSoldier(Quaternion rotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    public void SetHealth(int _health)
    {
        maxHealth = _health;
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Die()
    {
        PointManager.Instance.SetPoint(PersonType.soldier, PointManager.Instance.GetPoint(PersonType.soldier) - soldierPoint);
        Destroy(gameObject);
    }
    public void GetDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }
}
