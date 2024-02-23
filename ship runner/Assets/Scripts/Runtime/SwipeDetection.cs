using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool detectSwipeOnlyAfterRelease = false;

    public bool SwipeRight { get; private set; }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
                CheckSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsHorizontalSwipe() && FingerMovementDirection() == Vector2.right)
            {
                SwipeRight = true;
            }
            else
            {
                SwipeRight = false;
            }

            fingerDownPosition = fingerUpPosition;
        }
    }

    private bool IsHorizontalSwipe()
    {
        return Mathf.Abs(fingerUpPosition.y - fingerDownPosition.y) < Mathf.Abs(fingerUpPosition.x - fingerDownPosition.x);
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > 20 && HorizontalMovementDistance() > 20;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private Vector2 FingerMovementDirection()
    {
        return fingerUpPosition - fingerDownPosition;
    }
}