using System;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCreator : MonoBehaviour
{
    public static SoldierCreator Instance { get; private set; }
    public event EventHandler OnCreatedNewSoldiers;
    [SerializeField] private List<SoldierSO> soldiers;
    [SerializeField] private Transform creatingSoldierTransform;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CreateSoldiers();
    }
    public void CreateSoldiers()
    {
        OnCreatedNewSoldiers?.Invoke(this, EventArgs.Empty);
        DestroyPreviousSoldiers();
        int soldiersPoint = PointManager.Instance.GetPoint(PersonType.soldier);
        for (int i = soldiers.Count - 1; i >= 0; i--)
        {
            if (soldiersPoint < soldiers[i].soldierPointRequired)
                continue;
            int count = soldiersPoint / soldiers[i].soldierPointRequired;
            CreateSoldier(soldiers[i], count);
            soldiersPoint -= soldiers[i].soldierPointRequired * count;
        }
    }
    private void DestroyPreviousSoldiers()
    {
        int childCount = creatingSoldierTransform.childCount;
        for (int i = 1; i < childCount; i++)
        {
            Destroy(creatingSoldierTransform.GetChild(i).gameObject);
        }
    }

    private void CreateSoldier(SoldierSO soldierSO, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform soldier = Instantiate(soldierSO.soldierPrefab, creatingSoldierTransform);
            soldier.Translate(new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-1.5f, 1.5f)));
            soldier.GetComponent<Soldier>().InitializeSoldier(soldierSO);
        }
    }
}
