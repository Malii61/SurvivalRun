using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersController : MonoBehaviour
{
    public enum PositionClamp
    {
        left,
        right,
        none
    }
    private PositionClamp positionClamp = PositionClamp.none;
    [SerializeField] private float leftAndRightSpeed;
    [SerializeField] private float forwardSpeed;
    private void LateUpdate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        	
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
            }
        }
        if (_dragStarted && !PauseMenuUI.isPauseMenuActive)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                float x = _touch.deltaPosition.x * senstivityMultiplier;
                Move(x);
            }
            else
            {
                Move(0f);
            }
        }
#else

        float horizontalMove = Input.GetAxis("Horizontal") * leftAndRightSpeed * Time.deltaTime;
        Move(horizontalMove);
#endif 
    }
    private void Move(float moveValue)
    {
        float adjustedHorizontalMove = CheckPositionClamper(moveValue);
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(i).Translate(-forwardSpeed * Time.deltaTime, 0, adjustedHorizontalMove);
        //}
        transform.Translate(forwardSpeed * Time.deltaTime, 0, -adjustedHorizontalMove);
    }
    private float CheckPositionClamper(float horizontalMove)
    {
        bool isGreaterThanZero = horizontalMove > 0;
        if ((positionClamp == PositionClamp.right && isGreaterThanZero) || (positionClamp == PositionClamp.left && !isGreaterThanZero))
            return 0f;
        return horizontalMove;
    }
}
