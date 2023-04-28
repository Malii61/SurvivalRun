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
        zombiePoint = PointManager.Instance.GetPoint(PersonType.zombie) * 2;
        if (zombiePoint > PointManager.maxZombiePoint)
            zombiePoint = PointManager.maxZombiePoint;
        PointManager.Instance.SetPoint(PersonType.zombie, zombiePoint);
        zombiePointText.text = zombiePoint.ToString();
        ZombieCreator.Instance.SetZombieSpawnTransform(zombieSpawnTransform);
        ZombieCreator.Instance.CreateZombies();
        SetTextActivation(true);
        foreach (var optionPanel in optionPanels)
        {
            optionPanel.ResetPanel();
        }
    }
    public void DecreaseZombiePoint(int point)
    {
        if (zombiePointText.enabled)
        {
            zombiePointText.text = zombiePoint.ToString();
            zombiePoint -= point;
        }
        if (zombiePoint <= 0)
        {
            SoldiersController.Instance.SetForwardSpeed(SoldiersController.forwardSpeedOnNotCombat);
            SetTextActivation(false);
        }
    }
    private void SetTextActivation(bool isActive)
    {
        zombiePointText.enabled = isActive;
    }
}
