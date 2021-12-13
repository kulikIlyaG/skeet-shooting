using System;


public interface IShootTargets
{
    bool IsHaveShootTarget { get; }

    event Action<string> OnShootTargetActivated;

    event Action<string> OnShootTargetDeactivated;

    void SetShootTargetActive(string id);
    void SetShootTargetInactive(string id);
    BaseTarget GetTarget(string id);
}