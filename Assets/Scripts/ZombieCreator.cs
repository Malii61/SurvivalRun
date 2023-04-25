using System.Collections.Generic;
using UnityEngine;

public class ZombieCreator : MonoBehaviour
{
    public static ZombieCreator Instance { get; private set; }
    [SerializeField] private List<ZombieSO> zombies;
    [SerializeField] private Transform zombieSpawnTransform;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CreateZombies();
    }
    public void CreateZombies()
    {
        int zombiesPoint = PointManager.Instance.GetPoint(PersonType.zombie);
        for (int i = zombies.Count - 1; i >= 0; i--)
        {
            if (zombiesPoint < zombies[i].zombiePointRequired)
                continue;
            int count = zombiesPoint / zombies[i].zombiePointRequired;
            CreateZombie(zombies[i], count);
            zombiesPoint -= zombies[i].zombiePointRequired * count;
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
            soldier.GetComponent<Zombie>().InitializeZombie(zombieSO);
        }
    }
}
