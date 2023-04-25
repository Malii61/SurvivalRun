using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }
    [SerializeField] private List<Transform> platforms = new List<Transform>();
    [SerializeField] NavMeshSurface surface;
    private int platformOrder;
    private float distance;
    private bool firstAttempt = true;
    private void Awake()
    {
        Instance = this;
        platformOrder = platforms.Count - 1;
        distance = platforms[0].position.x - platforms[1].position.x;
    }
    private void Start()
    {
        platforms.Reverse();
        foreach (var platform in platforms)
        {
            platform.GetComponent<ZombiePlatform>().SetPlatform();
        }
        platforms.Reverse();
    }
    public void CreatePlatform()
    {
        if (firstAttempt)
        {
            firstAttempt = false;
            return;
        }
        Transform platform = platforms[platformOrder];
        platform.Translate(new Vector3(distance * 4, 0, 0));
        surface.BuildNavMesh();
        platform.GetComponent<ZombiePlatform>().SetPlatform();
        SetPlatformOrder();
    }

    private void SetPlatformOrder()
    {
        platformOrder--;
        if (platformOrder < 0)
        {
            platformOrder = platforms.Count - 1;
        }

    }
}
