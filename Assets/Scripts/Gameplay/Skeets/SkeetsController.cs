using System;
using System.Collections.Generic;
using UnityEngine;


public class SkeetsController : MonoBehaviour, IShootTargets
{
    [SerializeField]
    private Skeet[] allSkeets = null;

    private Dictionary<string, Skeet> skeets = null;

    private List<Skeet> inactiveSkeets = null;
    private List<Skeet> activeSkeets = null;

    public bool IsHaveShootTarget => activeSkeets.Count > 0;
    

    public event Action<string> OnShootTargetActivated;

    public event Action<string> OnShootTargetDeactivated;
    
    
    void Awake()
    {
        activeSkeets = new List<Skeet>(allSkeets.Length);
        inactiveSkeets = new List<Skeet>(allSkeets.Length);
        
        skeets = new Dictionary<string, Skeet>(allSkeets.Length);

        foreach (Skeet skeet in allSkeets)
        {
            skeets.Add(skeet.Id, skeet);
            inactiveSkeets.Add(skeet);
        }
    }


    public BaseTarget GetTarget(string id)
    {
        return GetSkeet(id);
    }
    

    private Skeet GetSkeet(string id)
    {
        if(skeets.ContainsKey(id))
            return skeets[id];

        return null;
    }

    public void SetShootTargetActive(string id)
    {
        Skeet skeet = GetSkeet(id);
        
        if (inactiveSkeets.Contains(skeet))
        {
            inactiveSkeets.Remove(skeet);
        }

        if (!activeSkeets.Contains(skeet))
        {
            activeSkeets.Add(skeet);
        }
        
        OnShootTargetActivated?.Invoke(id);
    }


    public void SetShootTargetInactive(string id)
    {
        Skeet skeet = GetSkeet(id);
        
        if (activeSkeets.Contains(skeet))
        {
            activeSkeets.Remove(skeet);
        }
        
        if (!inactiveSkeets.Contains(skeet))
        {
            inactiveSkeets.Add(skeet);
        }
        
        OnShootTargetDeactivated?.Invoke(id);
    }
}