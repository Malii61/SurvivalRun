using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    private int health;
    private int maxHealth;
    private float fireRate;
    private int soldierPoint;
    [SerializeField] private HealthBar healthBar;
    public void InitializeSoldier(SoldierSO soldierSO)
    {
        SetHealth(soldierSO.health);
        soldierPoint = soldierSO.soldierPointRequired;
    }
    public void SetHealth(int _health)
    {
        maxHealth = _health;
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Die()
    {
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
