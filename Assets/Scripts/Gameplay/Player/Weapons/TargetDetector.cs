using UnityEngine;


public class TargetDetector : MonoBehaviour
{
    [SerializeField]
    private float aimRadius = 1f;

    [SerializeField]
    private float aimRange = 100f;
    
    [SerializeField]
    private LayerMask layerMask = new LayerMask();
    
    
    private bool isDetectEnabled = false;
    
    private Collider[] targets = null;


    public bool IsDetectEnabled => isDetectEnabled;
    public bool DetectedTargets => targets != null && targets.Length > 0;
    
    private Vector3 AimPoint => transform.position;

    private Vector3 AimDirection => transform.forward;

    protected Vector3 PotentialSightingPoint => AimPoint + AimDirection * aimRange;
    
    protected Collider[] Targets => targets;

    

    public void SetDetectEnable(bool enable)
    {
        isDetectEnabled = enable;
    }


    private void FixedUpdate()
    {
        if (isDetectEnabled)
        {
            DetectTarget();
        }
    }


    private bool DetectTarget()
    {
        targets = Physics.OverlapCapsule(AimPoint, PotentialSightingPoint, aimRadius, layerMask);

        return DetectedTargets;
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AimPoint, aimRadius);
        Gizmos.DrawWireSphere(PotentialSightingPoint, aimRadius);
        
#region DRAW LINES FOR CAPSULE
        
        //right
        Vector3 outsideLineA = AimPoint + Vector3.right * aimRadius;
        Gizmos.DrawLine(outsideLineA, outsideLineA + AimDirection * aimRange);
        
        //left
        outsideLineA = AimPoint - Vector3.right * aimRadius;
        Gizmos.DrawLine(outsideLineA, outsideLineA + AimDirection * aimRange);
        
        //top
        outsideLineA = AimPoint + Vector3.up * aimRadius;
        Gizmos.DrawLine(outsideLineA, outsideLineA + AimDirection * aimRange);
        
        //bottom
        outsideLineA = AimPoint - Vector3.up * aimRadius;
        Gizmos.DrawLine(outsideLineA, outsideLineA + AimDirection * aimRange);
        
        //center line
        Gizmos.color = GetMainGizmosColor();
        Gizmos.DrawLine(AimPoint, AimPoint + AimDirection * aimRange);
        
#endregion
    }


    protected virtual Color GetMainGizmosColor()
    {
        return Color.red;
    }
}