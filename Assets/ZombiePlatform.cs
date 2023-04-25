using UnityEngine;
using TMPro;
public class ZombiePlatform : MonoBehaviour
{
    [SerializeField] Transform zombieSpawnTransform;
    [SerializeField] TextMeshProUGUI zombiePointText;
    //private void Start()
    //{
    //    SetPlatform();
    //}
    public void SetPlatform()
    {
        int zombiePoint = PointManager.Instance.GetPoint(PersonType.zombie) + 10;
        PointManager.Instance.SetPoint(PersonType.zombie, zombiePoint);
        zombiePointText.text = zombiePoint.ToString();
        ZombieCreator.Instance.SetZombieSpawnTransform(zombieSpawnTransform); 
        ZombieCreator.Instance.CreateZombies();
    }
}
