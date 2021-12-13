using UnityEngine;
using UnityEngine.UI;


public class SliderToIntensity : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private SelfRotatorByInput intens;

    private void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderUpdated);
        OnSliderUpdated(slider.value);
    }


    private void OnSliderUpdated(float arg0)
    {
        intens.intensity = arg0;
    }
}