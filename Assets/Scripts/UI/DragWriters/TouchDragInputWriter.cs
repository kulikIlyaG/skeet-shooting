using System;
using UnityEngine;


public class TouchDragInputWriter : MonoBehaviour, IAxis2DInputWriter
{
    public event Action<Vector2> OnAxis2DChanged;

    private Vector2 position = Vector2.zero;



    private void Update()
    {
#if UNITY_EDITOR
        MouseInput();
#else
        TouchInput();
#endif
    }


#if UNITY_EDITOR
    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            position = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePosition = Input.mousePosition;

            Vector2 delta = (currentMousePosition - position).normalized;
            
            OnAxis2DChanged?.Invoke(delta);
        }
    }
    
#else
    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                position = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                OnAxis2DChanged?.Invoke(touch.deltaPosition);
            }
        }
    }
#endif
}