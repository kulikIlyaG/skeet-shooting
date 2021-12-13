using UnityEngine;
using Zenject;


public class AimView : ProgressView
{
    [SerializeField]
    private Transform timerProgress = null;
    
    private Vector3 originalScale = Vector3.one;

    private IDelay delay = null;


    [Inject]
    private void Construct(IDelay delay)
    {
        this.delay = delay;
    }
    
    
    private void Awake()
    {
        originalScale = timerProgress.localScale;
    }


    private void OnEnable()
    {
        delay.OnDelayBegin += ShowTimer;
        delay.OnDelayUpdated += SetValue;
        delay.OnDelayCompleted += HideTimer;
        delay.OnDelayCanceled += HideTimer;
    }


    private void ShowTimer()
    {
        timerProgress.gameObject.SetActive(true);
    }


    private void HideTimer()
    {
        timerProgress.gameObject.SetActive(false);
        SetValue(0f);
    }
    
    
    public override void SetValue(float value)
    {
        timerProgress.localScale = originalScale * value;
    }
    
    
    private void OnDisable()
    {
        delay.OnDelayBegin -= ShowTimer;
        delay.OnDelayUpdated -= SetValue;
        delay.OnDelayCompleted -= HideTimer;
        delay.OnDelayCanceled -= HideTimer;
    }
}