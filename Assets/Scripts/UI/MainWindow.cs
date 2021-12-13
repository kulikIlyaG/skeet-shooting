using System;
using UnityEngine;


public class MainWindow : MonoBehaviour, IGameEvents
{
    [SerializeField]
    private DelayButton button;

    public event Action OnInvokeThrowSkeet;



    private void OnEnable()
    {
        button.AddCallback(OnClickedThrowSkeet);
    }


    private void OnClickedThrowSkeet()
    {
        OnInvokeThrowSkeet?.Invoke();
    }

    
    public void RefreshThrowers()
    {
        button.ResetButton();
    }
    

    private void OnDisable()
    {
        button.RemoveCallback(OnClickedThrowSkeet);
    }
}