using System;
using UnityEngine;

namespace SwipeDetector
{
    public class SwipeDetector : MonoBehaviour
    {
        public event Action<SwipeData> OnSwipe = delegate { };

        [SerializeField] private bool detectSwipeOnlyAfterRelease = false;

        [SerializeField] private float minDistanceForSwipe = 20f;

        private Vector2 _fingerDownPosition;
        private Vector2 _fingerUpPosition;

        private void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                TouchBegan(touch);
                TouchMoved(touch);
                TouchEnded(touch);
            }
            
        }

        private void TouchBegan(Touch touch)
        {
            if (touch.phase != TouchPhase.Began) return;
            _fingerUpPosition = touch.position;
            _fingerDownPosition = touch.position;
        }

        private void TouchMoved(Touch touch)
        {
            if (detectSwipeOnlyAfterRelease || touch.phase != TouchPhase.Moved) return;
            _fingerDownPosition = touch.position;
            DetectSwipe();
        }

        private void TouchEnded(Touch touch)
        {
            if (touch.phase != TouchPhase.Ended) return;
            _fingerDownPosition = touch.position;
            DetectSwipe();
        }
        

        private void DetectSwipe()
        {
            if (SwipeDistanceCheckMet())
            {
                if (IsVerticalSwipe())
                {
                    var direction = _fingerDownPosition.y - _fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                    SendSwipe(direction);
                }
                else
                {
                    var direction = _fingerDownPosition.x - _fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                    SendSwipe(direction);
                }
                _fingerUpPosition = _fingerDownPosition;
            }
        }

        private bool IsVerticalSwipe() => VerticalMovementDistance() > HorizontalMovementDistance();
        private bool SwipeDistanceCheckMet() => VerticalMovementDistance() >
            minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;

        private float VerticalMovementDistance() => Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);

        private float HorizontalMovementDistance() => Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);

        private void SendSwipe(SwipeDirection direction)
        {
            SwipeData swipeData = new SwipeData()
            {
                Direction = direction,
                StartPosition = _fingerDownPosition,
                EndPosition = _fingerUpPosition
            };
            OnSwipe(swipeData);
        }
    }

    
}