using System.Collections.Generic;
using UnityEngine;

public class ZombieCreator : MonoBehaviour
{
    public static ZombieCreator Instance { get; private set; }
    [SerializeField] private List<ZombieSO> zombies;
    private Transform zombieSpawnTransform;
    private void Awake()
    {
        Instance = this;
    }
    public void CreateZombie()
    {
        for (int i = zombies.Count - 1; i >= 0; i--)
        {
            int zombiesPoint = PointManager.Instance.GetPoint(PersonType.zombie);
            if (zombiesPoint < zombies[i].zombieCountRequired)
                continue;
            int count = zombiesPoint / zombies[i].zombieCountRequired;
            CreateZombie(zombies[i], count);
            PointManager.Instance.SetPoint(PersonType.zombie, zombiesPoint - zombies[i].zombieCountRequired * count);
        }
    }
    public void SetZombieSpawnTransform(Transform transform)
    {
        zombieSpawnTransform = transform;
    }
    private void CreateZombie(ZombieSO zombieSO, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform soldier = Instantiate(zombieSO.zombiePrefab, zombieSpawnTransform);
            soldier.Translate(new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f)));
            soldier.GetComponent<ZombieController>().InitializeZombie(zombieSO);
        }
    }
}
