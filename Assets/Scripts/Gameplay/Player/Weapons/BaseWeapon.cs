using UnityEngine;
using Zenject;


public abstract class BaseWeapon : TargetDetector
{
    [SerializeField]
    private Transform view = null;

    
    protected IShootTargets shootTargets = null;


    
    [Inject]
    private void Construct(IShootTargets shootTargets)
    {
        this.shootTargets = shootTargets;
    }

    
    private void OnEnable()
    {
        shootTargets.OnShootTargetActivated += OnAnyTargetActivated;
        shootTargets.OnShootTargetDeactivated += OnAnyTargetDeactivated;
        
        if(shootTargets.IsHaveShootTarget)
            SetDetectEnable(true);
    }


    protected Collider GetMainTarget()
    {
        //considering that there can be only 1 element, I will not process the array now

        return Targets[0];
    }


    [ContextMenu("RotateViewToAim")]
    private void RotateViewToAim()
    {
        view.LookAt(PotentialSightingPoint);
    }
    
    
    protected virtual void OnAnyTargetActivated(string id)
    {
        if (!IsDetectEnabled)
        {
            SetDetectEnable(true);
        }
    }


    protected virtual  void OnAnyTargetDeactivated(string id)
    {
        if (IsDetectEnabled)
        {
            if (!shootTargets.IsHaveShootTarget)
            {
                SetDetectEnable(false);
            }
        }
    }


    protected abstract void Shoot();

    
    private void OnDisable()
    {
        shootTargets.OnShootTargetActivated -= OnAnyTargetActivated;
        shootTargets.OnShootTargetDeactivated -= OnAnyTargetDeactivated;
    }


    protected override Color GetMainGizmosColor()
    {
        return IsDetectEnabled ? Color.green : Color.red;
    }
}