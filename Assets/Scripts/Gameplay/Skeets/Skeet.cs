using System;
using UnityEngine;
using Zenject;


public class Skeet : BaseTarget
{
    [SerializeField]
    private SkeetThrowParameters parameters = null;
    
    [Space]
    [SerializeField]
    private ParticleSystem particlesCrash = null;
    [SerializeField]
    private ParticleSystem particlesKill = null;
    
    
    private bool isFlies = false;

    
    private IShootTargets shootTargets = null;


    private Vector3 startPoint = Vector3.zero;
    private Vector3 endPoint = Vector3.zero;
    
    
    private float lerpValue = 0f;
    private float percentOfWay = 0f;

    
    private float startFlyTime = 0f;


    public event Action OnKilled = null;
    public event Action OnCrashed = null;



    [Inject]
    private void Construct(IShootTargets shootTargets)
    {
        this.shootTargets = shootTargets;
    }
    
    
    private void Awake()
    {
        startPoint = transform.position;
    }

    
    public void Throw(Vector3 direction)
    {
        percentOfWay = 0f;
        lerpValue = 1f;
        
        endPoint = startPoint + (GetDistance() * direction);
        
        startFlyTime = Time.time;
        
        isCleared = false;
        
        isFlies = true;
        
        shootTargets.SetShootTargetActive(Id);
    }


    private void Update()
    {
        if (isFlies)
        {
            float pastTime = Time.time - startFlyTime;
            
            percentOfWay = pastTime / parameters.TimeForFly;

            float yPosition = GetPositionCurve(lerpValue, percentOfWay) * GetHeight();

            Vector3 position = Vector3.Lerp(startPoint, endPoint, percentOfWay);
            
            position = new Vector3(position.x, startPoint.y + yPosition, position.z);

            transform.position = position;

            if (percentOfWay >= 1f)
            {
                Crash();
            }
        }
    }


    private float GetPositionCurve(float lerpValue, float percent)
    {
        return parameters.GetPositionCurve(lerpValue, percent);
    }


    private float GetDistance()
    {
        return parameters.GetDistance(lerpValue);
    }
    
    
    private float GetHeight()
    {
        return parameters.GetHeight(lerpValue);
    }

    
    private void Crash()
    {
        Instantiate(particlesCrash, transform.position, Quaternion.identity);
        
        OnCrashed?.Invoke();
        
        Clear();
    }


    public override void Kill()
    {
        Instantiate(particlesKill, transform.position, Quaternion.identity);

        OnKilled?.Invoke();
        
        base.Kill();
        
        Clear();
    }


    public override void Clear()
    {
        base.Clear();
        isFlies = false;
        
        shootTargets.SetShootTargetInactive(Id);
    }


    public override bool IsEnabled()
    {
        return isFlies;
    }


    public override float GetHealthPercent()
    {
        return parameters.GetHealthPercent(percentOfWay);
    }


    public override bool IsALatestChance()
    {
        return percentOfWay > parameters.LatestChanceToShootPercent;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other != null)
        {
            Crash();
        }
    }
}