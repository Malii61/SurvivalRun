using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SoldierAnimationController : MonoBehaviour
{
    [SerializeField] MultiAimConstraint multiAimConstraint;
    [SerializeField] Transform target;
    private float targetWeight;
    public void SetTargetPosition(Vector3 _position)
    {
        if (_position == Vector3.zero)
            targetWeight = 0;
        else
            targetWeight = 1;
        target.position = _position;
    }
    private void Update()
    {
        multiAimConstraint.weight = Mathf.Lerp(multiAimConstraint.weight, targetWeight, Time.deltaTime * 20);
    }
}
