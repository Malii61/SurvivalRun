using UnityEngine;
public enum GunType
{
    pistol,
    m16,
    bomb
}
[CreateAssetMenu()]
public class SoldierSO : ScriptableObject
{
    public string soldierName;
    public Transform soldierPrefab;
    public int health;
    public int soldierPointRequired;
    public GunType gunType;
    public int damage;
    public float fireRate;
}
