using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class DelayButton : MonoBehaviour
{
    [SerializeField]
    private Button button = null;
    
    [SerializeField]
    private ProgressView progressView = null;

    [SerializeField]
    private float delay = 1f;

    [SerializeField]
    private bool setDisableAfterClick = true;


    private float durationResetAnimation = 0.7f;

    private event Action onClick = null;


    
    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }


    public void SetInteractable(bool isInteractable)
    {
        button.interactable = isInteractable;
    }


    public void ResetButton()
    {
        float value = 0f;

        DOTween.To(() => value, x =>
                {
                    value = x;
                    progressView.SetValue(value);
                },
                1f, durationResetAnimation).SetEase(Ease.InOutCirc).SetAutoKill()
            .OnComplete(()=> SetInteractable(true));
    }
    

    public void AddCallback(Action callback)
    {
        onClick += callback;
    }


    public void RemoveCallback(Action callback)
    {
        onClick -= callback;
    }
    
    
    private void OnClick()
    {
        float value = 1f;

        if (setDisableAfterClick)
        {
            SetInteractable(false);
        }
        
        DOTween.To(()=> value, x=>
        {
            value = x;
            progressView.SetValue(value);
        },
                0f, delay).OnComplete(()=> onClick?.Invoke());
    }
}