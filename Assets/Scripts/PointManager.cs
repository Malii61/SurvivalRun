using UnityEngine;
public enum PersonType
{
    zombie,
    soldier,
}
public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }
    private int zombiesPoint = 5;
    private int soldiersPoint = 20;
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
    }
}
