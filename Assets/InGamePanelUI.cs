using UnityEngine;
using TMPro;
public class InGamePanelUI : MonoBehaviour
{
    public static InGamePanelUI Instance { get; private set; }
    [SerializeField] TextMeshProUGUI pointText;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        pointText.text = "0";
        PointManager.Instance.OnGamePointChanged += PointManager_OnGamePointChanged; ;
    }

    private void PointManager_OnGamePointChanged(object sender, PointManager.OnGamePointChangedEventArgs e)
    {
        pointText.text = e.point.ToString();
    }

}
