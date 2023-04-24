using UnityEngine;
using TMPro;
public class OptionPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mathematicalOperationText; 
    private void OnTriggerEnter(Collider other)
    {
        int result =  MathematicalOperationCalculator.FindResult(mathematicalOperationText.text);
        Debug.Log(result);
        PointManager.Instance.SetPoint(PersonType.soldier, result);
        SoldierCreator.Instance.CreateSoldiers();
    }
}
