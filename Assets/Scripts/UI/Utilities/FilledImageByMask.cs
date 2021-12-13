using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectMask2D))]
[ExecuteInEditMode]
public class FilledImageByMask : ProgressView
{
    [SerializeField]
    private RectTransform rectTransform = null;

    [SerializeField]
    private RectMask2D rectMask = null;

    [SerializeField]
    [Range(0f, 1f)]
    private float fill = 1f;

    
    #if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateMaskPaddings();
        }
    }
    #endif
    

    public override void SetValue(float value)
    {
        fill = value;
        UpdateMaskPaddings();
    }


    private void UpdateMaskPaddings()
    {
        Vector4 maskPaddings = GetMaskPaddings(fill);

        rectMask.padding = maskPaddings;
    }


    private Vector4 GetMaskPaddings(float fill)
    {
        Vector2 size = rectTransform.rect.size;

        fill = 1f - fill;

        return new Vector4(0f, 0f, 0f, size.y * fill);
    }

    private void OnValidate()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        
        if (rectMask == null)
        {
            rectMask = GetComponent<RectMask2D>();
        }
    }
}