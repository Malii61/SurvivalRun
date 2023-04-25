using UnityEngine;
public class Soldier : MonoBehaviour
{
    private float range = 9f;
    private int health;
    private int maxHealth;
    private float fireRate;
    private int soldierPoint;
    private float fireRateTimer;
    private int damage;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] SoldierAnimationController soldierAnimationController;
    public void InitializeSoldier(SoldierSO soldierSO)
    {
        SetHealth(soldierSO.health);
        soldierPoint = soldierSO.soldierPointRequired;
        fireRate = soldierSO.fireRate;
        damage = soldierSO.damage;
    }
    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.TryGetComponent(out Zombie zombie))
            {
                soldierAnimationController.SetTargetPosition(zombie.transform.position);
                if (fireRateTimer >= fireRate)
                {
                    zombie.GetDamage(damage);
                    fireRateTimer = 0;
                }
                break;
            }
            else
            {
                soldierAnimationController.SetTargetPosition(Vector3.zero);
            }
        }
        fireRateTimer += Time.fixedDeltaTime;
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
