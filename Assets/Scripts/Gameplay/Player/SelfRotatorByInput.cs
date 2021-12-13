using System;
using UnityEngine;
using Zenject;


public class SelfRotatorByInput : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1f)]
    public float intensity = 1f;

    [SerializeField]
    private AnimationCurve dynamicIntensityCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    
    [SerializeField]
    private Vector2 speedRotation = Vector2.one;

    
    [SerializeField]
    private bool inverse = true;

    
    [SerializeField]
    private bool useLimits = false;
    [SerializeField]
    private Vector2 limitMin = Vector2.zero;
    [SerializeField]
    private Vector2 limitMax = Vector2.zero;

    
    [Space]
    [SerializeField]
    private Transform rotateTarget = null;

    
    private Vector3 startRotation = Vector3.zero;

    private float horizontalViewAngle = 0f;
    private float verticalViewAngle = 0f;

    
    private IAxis2DInputWriter inputWriter = null;
    

    [Inject]
    private void Construct(IAxis2DInputWriter axisWriter)
    {
        inputWriter = axisWriter;
    }
    
    
    private void Awake()
    {
        startRotation = rotateTarget.eulerAngles;
        horizontalViewAngle = startRotation.y;
        verticalViewAngle = startRotation.x;
    }


    private void OnEnable()
    {
        inputWriter.OnAxis2DChanged += Rotate;
    }


    private void Rotate(Vector2 normalizedDelta)
    {
        Vector2 dynamicIntensity = GetDynamicIntensity(normalizedDelta);

        horizontalViewAngle += (normalizedDelta.x * speedRotation.x * Time.deltaTime) * dynamicIntensity.x;

        verticalViewAngle +=
                ((inverse ? normalizedDelta.y : normalizedDelta.y * -1f) * speedRotation.y * Time.deltaTime) *
                dynamicIntensity.y;


        if (useLimits)
        {
            ClampToLimits();
        }

        rotateTarget.rotation =
                Quaternion.Euler(verticalViewAngle, horizontalViewAngle,
                        0f);
    }


    private Vector2 GetDynamicIntensity(Vector2 normalizedDelta)
    {
        float x = dynamicIntensityCurve.Evaluate(Mathf.Abs(normalizedDelta.x)) * intensity;
        float y = dynamicIntensityCurve.Evaluate(Mathf.Abs(normalizedDelta.y)) * intensity;
        
        return new Vector2(x, y);
    }


    private void ClampToLimits()
    {
        verticalViewAngle = Mathf.Clamp(verticalViewAngle, limitMin.y, limitMax.y);
        horizontalViewAngle = Mathf.Clamp(horizontalViewAngle, limitMin.x, limitMax.x);
    }


    private void OnDisable()
    {
        inputWriter.OnAxis2DChanged -= Rotate;
    }


    private void OnValidate()
    {
        if (rotateTarget == null)
        {
            rotateTarget = gameObject.transform;
        }
    }
}
