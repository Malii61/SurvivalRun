using UnityEngine;
public enum PersonType
{
    zombie,
    soldier,
}
public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }
    private int zombiesPoint = 12;
    private int soldiersPoint = 15;
    private void Awake()
    {
        Instance = this;
    }
    public int GetPoint(PersonType type)
    {
        switch (type)
        {
            default:
            case PersonType.zombie:
                return zombiesPoint;
            case PersonType.soldier:
                return soldiersPoint;
        }
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
    }
}
