using UnityEngine;
using TMPro;
public class OptionPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mathematicalOperationText;
    [SerializeField] Collider otherPanelCollider;
    private bool triggered;
    private void Start()
    {
        RefreshOperationText();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.transform.TryGetComponent(out SoldiersController controller))
            return;
        // destroy the other collider in case player triggers it
        otherPanelCollider.enabled = false;
        triggered = true;

        // set soldier point
        int result = MathematicalOperationCalculator.FindResult(mathematicalOperationText.text);
        PointManager.Instance.SetPoint(PersonType.soldier, result);

        // create soldiers according to soldier point
        SoldierCreator.Instance.CreateSoldiers();

        // create next platform
        PlatformManager.Instance.CreatePlatform();

        // set math operation of next panel
        RefreshOperationText();
    }
    private void RefreshOperationText()
    {
        MathematicalOperations operations = new MathematicalOperations();

        //choose random operation from list
        mathematicalOperationText.text = operations.operationsList[Random.Range(0, operations.operationsList.Count)];
    }
    public void ResetPanel()
    {
        otherPanelCollider.enabled = true;
        triggered = false;
        mathematicalOperationText.enabled = true;
    }
}
