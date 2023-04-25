using UnityEngine;
using TMPro;
public class OptionPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mathematicalOperationText;

    private void Start()
    {
        RefreshOperationText();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.TryGetComponent(out SoldiersController controller))
            return;
        int result = MathematicalOperationCalculator.FindResult(mathematicalOperationText.text);
        Debug.Log(result);
        PointManager.Instance.SetPoint(PersonType.soldier, result);
        SoldierCreator.Instance.CreateSoldiers();
        PlatformManager.Instance.CreatePlatform();
        RefreshOperationText();
    }

    private void RefreshOperationText()
    {
        MathematicalOperations operations = new MathematicalOperations();
        mathematicalOperationText.text = operations.operationsList[Random.Range(0, operations.operationsList.Count)];
    }
}
