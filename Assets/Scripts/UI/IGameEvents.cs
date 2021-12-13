using System;


public interface IGameEvents
{
    event Action OnInvokeThrowSkeet;

    void RefreshThrowers();
}