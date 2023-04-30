using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mathematicalOperationText;
    [SerializeField] Collider otherPanelCollider;
    private bool triggered;
    private bool isPositiveOperation;
    [SerializeField] Image panelBackground;
    private Color firstPanelColor;
    private BoxCollider boxCollider;
    private Vector3 aheadColliderPos = new Vector3(-0.3f, 0, -1.5f);
    private Vector3 behindColliderPos = new Vector3(-0.3f, 0, -2.0f);
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        firstPanelColor = panelBackground.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.transform.TryGetComponent(out SoldiersController controller))
            return;
        // destroy the other collider in case player triggers it
        otherPanelCollider.enabled = false;
        triggered = true;

        // set soldier point
        int soldierPoint = MathematicalOperationCalculator.FindResult(mathematicalOperationText.text);
        if (soldierPoint > PointManager.maxSoldierPoint)
            soldierPoint = PointManager.maxSoldierPoint;
        else if (soldierPoint <= 0)
            SurvivalRunManager.Instance.OnGameFinished();

        PointManager.Instance.SetPoint(PersonType.soldier, soldierPoint);
        if (isPositiveOperation)
            panelBackground.color = Color.green;
        else
            panelBackground.color = Color.red;
        //dosth
        // create soldiers according to soldier point
        SoldierCreator.Instance.CreateSoldiers();

        // create next platform
        PlatformManager.Instance.CreatePlatform();

        // decrease forward speed in combat
        SoldiersController.Instance.SetForwardSpeed(SoldiersController.forwardSpeedOnCombat);
    }
    public void SetOperationText(bool isPositive)
    {
        MathematicalOperations operations = new MathematicalOperations();
        isPositiveOperation = isPositive;
        List<string> operationList = new List<string>();
        if (isPositiveOperation)
            operationList = operations.positiveOperationsList;
        else
            operationList = operations.negativeOperationsList;
        //choose random operation from list
        mathematicalOperationText.text = operationList[Random.Range(0, operationList.Count)];
    }
    public void ResetPanel(bool isAhead)
    {
        if (isAhead)
            boxCollider.center = aheadColliderPos;
        else
            boxCollider.center = behindColliderPos;
        panelBackground.color = firstPanelColor;
        otherPanelCollider.enabled = true;
        triggered = false;
        mathematicalOperationText.enabled = true;
    }
}
