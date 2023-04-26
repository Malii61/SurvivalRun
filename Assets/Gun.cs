using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform bullet;
    [SerializeField] ParticleSystem muzzleEffect;
    private Vector3 bulletStartPos;
    private Vector3 bulletTargetPos;
    private void Start()
    {
        bulletStartPos = bullet.position;
        bulletTargetPos = bulletStartPos;
    }
    public void Fire(Vector3 targetPos)
    {
        bulletTargetPos = targetPos;
        muzzleEffect.Play();
    }
    private void Update()
    {
        Vector3.Lerp(bulletStartPos, bulletTargetPos, 0.1f);
    }
}
