using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class AutoWeapon : BaseWeapon, IDelay
{
    [SerializeField]
    private float maxDelay = 1f;
    [SerializeField]
    private float delaySpeed = 1f;
    
    
    private BaseTarget currentTarget = null;
    

    private bool IsHaveTarget => currentTarget != null;
    
    private bool luckyStrikeWasMade = false;


    private float currentDelay = 0f;
    private float targetDelay = 0f;


    public event Action OnDelayBegin;

    public event Action<float> OnDelayUpdated;

    public event Action OnDelayCompleted;
    
    public event Action OnDelayCanceled;


        
    void Update()
    {
        if (DetectedTargets)
        {
            FindTarget();
            
            if (IsHaveTarget)
            {
                UpdateDelay();
            }
            else if (currentDelay > 0f)
            {
                ClearDelay();
            }
        }
        else
        {
            if (IsHaveTarget)
            {
                ClearTarget();
            }
        }
    }


    private void FindTarget()
    {
        Collider targetCollider = GetMainTarget();

        if (targetCollider != null)
        {
            if (IsHaveTarget)
            {
                if (!targetCollider.name.Equals(currentTarget.Id))
                {
                    if (IsTargetEnabled(targetCollider.name))
                    {
                        ReplaceTarget(targetCollider.name);
                    }
                }
            }
            else
            {
                if (IsTargetEnabled(targetCollider.name))
                {
                    SetTarget(targetCollider.name);
                }
            }
        }
    }


    private void ReplaceTarget(string id)
    {
        Debug.Log($"Replace target: {id}");
        
        ClearTarget();
        SetTarget(id);
    }


    private void SetTarget(string id)
    {
        Debug.Log($"Set target: {id}");
        
        currentTarget = shootTargets.GetTarget(id);

        targetDelay = maxDelay * currentTarget.GetHealthPercent();

        OnDelayBegin?.Invoke();
    }


    private void UpdateDelay()
    {
        Debug.Log("Update delay");
        
        currentDelay += delaySpeed * Time.deltaTime;

        float percent = currentDelay / targetDelay;
            
        OnDelayUpdated?.Invoke(percent);

        if (!luckyStrikeWasMade)
        {
            if (currentTarget.IsALatestChance())
            {
                LuckyStrike();
            }
        }

        if (currentDelay > targetDelay)
        {
            Shoot();
        }
    }


    private void LuckyStrike()
    {
        float delay = currentDelay - Mathf.Floor(currentDelay);
        
        Debug.Log($"Lucky Strike chance: {delay}");

        if (delay > Random.Range(0f, 1f))
        {
            Shoot();
        }

        luckyStrikeWasMade = true;
    }


    protected override void Shoot()
    {
        if (currentTarget != null)
        {
            currentTarget.Kill();
            ResetDelay();
                
            OnDelayCompleted?.Invoke();
            
            ClearTarget();
        }
        
        Debug.Log("Shoot");
    }


    private void ClearTarget()
    {
        currentTarget = null;
        
        luckyStrikeWasMade = false;
        
        ClearDelay();
        
        OnDelayCanceled?.Invoke();
        
        Debug.Log("Clear Target");
    }


    private bool IsTargetEnabled(string id)
    {
        return shootTargets.GetTarget(id).IsEnabled();
    }


    private void ResetDelay()
    {
        ClearDelay();
    }


    private void ClearDelay()
    {
        currentDelay = 0f;
    }
}