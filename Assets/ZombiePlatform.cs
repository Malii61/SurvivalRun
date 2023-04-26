using UnityEngine;
using TMPro;
public class ZombiePlatform : MonoBehaviour
{
    [SerializeField] Transform zombieSpawnTransform;
    [SerializeField] TextMeshProUGUI zombiePointText;
    [SerializeField] OptionPanel[] optionPanels;
    private int zombiePoint;
    public void SetPlatform()
    {
        zombiePoint = PointManager.Instance.GetPoint(PersonType.zombie) + 10;
        PointManager.Instance.SetPoint(PersonType.zombie, zombiePoint);
        zombiePointText.text = zombiePoint.ToString();
        ZombieCreator.Instance.SetZombieSpawnTransform(zombieSpawnTransform); 
        ZombieCreator.Instance.CreateZombies();
        foreach(var optionPanel in optionPanels)
        {
            optionPanel.ResetPanel();
        }
    }
    public void DecreaseZombiePoint(int point)
    {
        zombiePoint -= point;
        if (zombiePoint <= 0)
        {
            zombiePointText.enabled = false;
        }
        else
        {
            zombiePointText.text = zombiePoint.ToString();
        }
    }
}
