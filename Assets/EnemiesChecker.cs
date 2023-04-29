using UnityEngine;

public class EnemiesChecker : MonoBehaviour
{
    [SerializeField] private ZombiePlatform zombiePlatform;
    private void OnTriggerEnter(Collider other)
    {
        if (zombiePlatform.GetZombiePoint() > 0)
            SoldiersController.Instance.SetForwardSpeed(0);
    }
}
