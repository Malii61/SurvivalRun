using UnityEngine;
using TMPro;
using System;

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
        if (zombiePoint == PointManager.maxZombiePoint)
            zombiePointText.text = "MAX";
        else
            zombiePointText.text = zombiePoint.ToString();

        ZombieCreator.Instance.SetZombieSpawnTransform(zombieSpawnTransform);
        ZombieCreator.Instance.CreateZombies();
        SetTextActivation(true);
        SetRandomAttributesForPanels();
    }

    private void SetRandomAttributesForPanels()
    {
        int randomOperationValue = UnityEngine.Random.Range(0, 2);
        optionPanels[0].SetOperationText(randomOperationValue == 0);
        optionPanels[1].SetOperationText(randomOperationValue == 1);

        int randomColliderPositionValue = UnityEngine.Random.Range(0, 2);
        optionPanels[0].ResetPanel(randomColliderPositionValue == 0);
        optionPanels[1].ResetPanel(randomColliderPositionValue == 1);
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
    public int GetZombiePoint()
    {
        return zombiePoint;
    }
}
