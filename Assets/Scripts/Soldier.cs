using System;
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
    private Quaternion firstRotation;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] SoldierAnimationController soldierAnimationController;
    [SerializeField] Gun gun;
    private float damping = 20f;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.TryGetComponent(out Zombie zombie))
            {
                RotateSoldier(zombie.transform.position);
                if (fireRateTimer >= fireRate)
                {
                    gun.Fire(zombie.transform.position);
                    zombie.GetDamage(damage);
                    fireRateTimer = 0;
                }
                break;
            }
            else
            {
                //rotate soldier the first rotation value
                RotateSoldier(Vector3.zero);
            }
        }
        fireRateTimer += Time.fixedDeltaTime;
    }

    private void RotateSoldier(Vector3 zombiePos)
    {
        var lookDir = zombiePos - transform.position;
        lookDir.y = 0;
        Quaternion rotation;
        if(zombiePos == Vector3.zero)
        {
            rotation = firstRotation;
        }
        else
        {
            rotation = Quaternion.LookRotation(lookDir);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
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
