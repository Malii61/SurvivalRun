using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }
    [SerializeField] private Transform[] platforms;
    private int platformOrder = 0;
    private void Awake()
    {
        Instance = this;
    }
    public void CreatePlatform()
    {
        Transform platform = platforms[platformOrder];
        platform.Translate(new Vector3(120, 0, 0));
        PointManager.Instance.SetPoint(PersonType.zombie, PointManager.Instance.GetPoint(PersonType.zombie) + 10);
        ZombieCreator.Instance.SetZombieSpawnTransform(platform.GetChild(0));
        ZombieCreator.Instance.CreateZombies();
        SetPlatformOrder();
    }

    private void SetPlatformOrder()
    {
        if(platformOrder >= platforms.Length - 1)
        {
            platformOrder = 0;
        }
        else
        {
            platformOrder++;
        }
    }
}
