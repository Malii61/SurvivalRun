using UnityEngine;
[CreateAssetMenu()]
public class ZombieSO : ScriptableObject
{
    public string zombieName;
    public Transform zombiePrefab;
    public float movementSpeed;
    public float attackSpeed;
    public int health;
    public int damage;
    public int zombiePointRequired;
}
