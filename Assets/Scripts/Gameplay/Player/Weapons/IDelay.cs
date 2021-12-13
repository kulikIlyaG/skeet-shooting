using System;


public interface IDelay
{
    event Action OnDelayBegin;
    event Action<float> OnDelayUpdated;
    event Action OnDelayCompleted;
    event Action OnDelayCanceled;
}