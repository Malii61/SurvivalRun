using System;
using UnityEngine;
public enum PersonType
{
    zombie,
    soldier,
}
public class PointManager : MonoBehaviour
{
    public static int maxZombiePoint = 1000000;
    public static int maxSoldierPoint = 10000000;
    public static PointManager Instance { get; private set; }
    private int zombiesPoint = 5;
    private int soldiersPoint = 1000;
    public event EventHandler<OnGamePointChangedEventArgs> OnGamePointChanged;
    public class OnGamePointChangedEventArgs: EventArgs
    {
        public int point;
    }
    private int gamePoint = 0;
    private void Awake()
    {
        Instance = this;
    }
    public int GetPoint(PersonType type)
    {
        return type switch
        {
            PersonType.soldier => soldiersPoint,
            _ => zombiesPoint,
        };
    }
    public void SetPoint(PersonType type, int point)
    {
        switch (type)
        {
            default:
            case PersonType.zombie:
                zombiesPoint = point;
                break;
            case PersonType.soldier:
                soldiersPoint = point;
                break;
        }
        if (soldiersPoint <= 0)
            SurvivalRunManager.Instance.OnGameFinished();
    }
    public void IncreaseGamePoint(int point)
    {
        gamePoint += point;
        OnGamePointChanged?.Invoke(this, new OnGamePointChangedEventArgs
        {
            point = gamePoint
        });
    }
    public int GetGamePoint()
    {
        return gamePoint;
    }
}
