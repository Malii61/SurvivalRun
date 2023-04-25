using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform bullet;
    private Vector3 bulletStartPos;
    private Vector3 bulletTargetPos;
    private void Start()
    {
        bulletStartPos = bullet.position;
        bulletTargetPos = bulletStartPos;
    }
    public void FireBullet(Vector3 targetPos)
    {
        bulletTargetPos = targetPos;
    }
    private void Update()
    {
        Vector3.Lerp(bulletStartPos, bulletTargetPos, 0.1f);
    }
}
