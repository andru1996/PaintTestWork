using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float swipeOffset = 100f;

    public static System.Action<string> OnSwipe;

    private Vector2 _startPosition;
    private Vector2 _swipeDelta;

    private bool isSwiping;
    private bool isMobile;
    

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if(!isMobile)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                _startPosition = Input.mousePosition;
            }
            else
            {
                if(Input.GetMouseButtonUp(0))
                {
                    ResetSwipe();
                }
            }
        }
        else
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    _startPosition = Input.GetTouch(0).position;
                }
                else
                {
                    if(Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        ResetSwipe();
                    }
                }
            }
        }

        CheckSwipe();
    }

    private void CheckSwipe()
    {
        _swipeDelta = Vector2.zero;

        if(isSwiping)
        {
            if(!isMobile && Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startPosition;
            }
            else
            {
                if(Input.touchCount >0)
                {
                    _swipeDelta = Input.GetTouch(0).position - _startPosition;
                }
            }
        }

        if(_swipeDelta.magnitude >= swipeOffset)
        {
            if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
            {
                if(_swipeDelta.x > 0)
                {
                    OnSwipe?.Invoke("Right");
                }
                else
                {
                    OnSwipe?.Invoke("Left");
                }
            }
            else
            {
                if (_swipeDelta.y > 0)
                {
                    OnSwipe?.Invoke("Up");
                }
                else
                {
                    OnSwipe?.Invoke("Down");
                }
            }

            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        isSwiping = false;

        _startPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }
}
