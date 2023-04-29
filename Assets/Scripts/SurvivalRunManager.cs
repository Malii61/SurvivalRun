using System;
using UnityEngine;

public class SurvivalRunManager : MonoBehaviour
{
    public static SurvivalRunManager Instance { get; private set; }
    public event EventHandler OnGameFinishedEvent;
    private void Awake()
    {
        Instance = this;
    }
    public void OnGameFinished()
    {
        OnGameFinishedEvent?.Invoke(this, EventArgs.Empty);
    }

}
