using UnityEngine;
using TMPro;

public class SoldiersController : MonoBehaviour
{
    public static SoldiersController Instance { get; private set; }

    public static float forwardSpeedOnCombat = 5;
    public static float forwardSpeedOnNotCombat = 14;

    [SerializeField] private float leftAndRightSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private TextMeshProUGUI soldiersPointUIText;
    private Rigidbody rb;
    private float targetSpeed;
    Touch _touch;
    private bool _dragStarted;
    private float x = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Instance = this;
        targetSpeed = forwardSpeed;
    }
    private void LateUpdate()
    {
#if UNITY_ANDROID&& !UNITY_EDITOR

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
            }
        }

        if (_dragStarted)
        {
            x = _touch.deltaPosition.x * 0.3f;
            Move(x);
        }
        else
            Move(0f);
#else

        float horizontalMove = Input.GetAxis("Horizontal") * leftAndRightSpeed;
        Move(horizontalMove);
#endif 
        SetText();
        forwardSpeed = Mathf.Lerp(forwardSpeed, targetSpeed, 0.2f);
    }

    private void SetText()
    {
        int soldierPoint = PointManager.Instance.GetPoint(PersonType.soldier);
        if (soldierPoint == PointManager.maxSoldierPoint)
            soldiersPointUIText.text = "MAX";
        else
            soldiersPointUIText.text = soldierPoint.ToString();
    }

    private float GetHorizontalAxis()
    {
#if UNITY_ANDROID&& !UNITY_EDITOR
        return x;
#else
        return Input.GetAxis("Horizontal");
#endif
    }
    private void Move(float moveValue)
    {
        float horizontalAxisValue = GetHorizontalAxis();
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Animator anim))
            {
                anim.SetFloat("Horizontal", horizontalAxisValue);
            }
        }
        rb.velocity = new Vector3(0, 0, -moveValue);
        rb.MovePosition(transform.position + new Vector3(forwardSpeed * Time.deltaTime, 0, 0));
    }
    public void SetForwardSpeed(float speed)
    {
        targetSpeed = speed;
    }
}
