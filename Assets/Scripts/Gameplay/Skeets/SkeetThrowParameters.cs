using UnityEngine;


[CreateAssetMenu(fileName = "SkeetThrowParameters", menuName = "SkeetShooting/SkeetThrowParameters")]
public class SkeetThrowParameters : ScriptableObject
{
    [SerializeField]
    private float minDistance = 10f;
    [SerializeField]
    private float maxDistance = 20f;
    
    [SerializeField]
    private float minHeight = 5f;
    [SerializeField]
    private float maxHeight = 10f;
    
    [SerializeField]
    private float timeForFly = 5f;

    
    [Space]
    [SerializeField]
    private float latestChanceToShootPercent = 0.9f;
    
    
    [Space]
    [SerializeField]
    private AnimationCurve minCurve = AnimationCurve.Linear(0f,0f,1f, 1f);
    [SerializeField]
    private AnimationCurve maxCurve = AnimationCurve.Linear(0f,0f,1f, 1f);
    
    [SerializeField]
    private AnimationCurve healthCurveByTimeInAir = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public float TimeForFly => timeForFly;
    
    public float LatestChanceToShootPercent => latestChanceToShootPercent;


    public float GetPositionCurve(float lerpValue, float percent)
    {
        return Mathf.Lerp(minCurve.Evaluate(percent), maxCurve.Evaluate(percent), lerpValue);
    }
    
    
    public float GetDistance(float lerpValue)
    {
        return Mathf.Lerp(minDistance, maxDistance, lerpValue);
    }
    
    
    public float GetHeight(float lerpValue)
    {
        return Mathf.Lerp(minHeight, maxHeight, lerpValue);
    }
    
    
    public float GetHealthPercent(float percent)
    {
        return healthCurveByTimeInAir.Evaluate(percent);
    }
}