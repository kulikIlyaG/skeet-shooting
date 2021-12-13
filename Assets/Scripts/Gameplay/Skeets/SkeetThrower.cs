using System;
using UnityEngine;


public class SkeetThrower : MonoBehaviour
{
    [SerializeField]
    private Skeet skeet = null;
    
    
    [SerializeField]
    private Transform throwPoint = null;
    

    private Action onThrowCompleted = null;
    
    
    
    private void OnEnable()
    {
        skeet.OnCrashed += OnSkeetCrashed;
        skeet.OnKilled += OnSkeetKilled;
    }


    public void Throw(Action onThrowCompleted)
    {
        if (!skeet.IsCleared)
            skeet.Clear();

        this.onThrowCompleted = onThrowCompleted;

        Vector3 throwDirection = GetThrowDirection();
        
        skeet.transform.position = throwPoint.position;
        skeet.transform.rotation = Quaternion.Euler(new Vector3(throwDirection.x, throwDirection.y, transform.position.x < 0f ? -23f : 23f));
        
        skeet.gameObject.SetActive(true);

        Vector3 direction = throwDirection;

        skeet.Throw(direction);
        
        Debug.Log($"Throw");
    }




    private Vector3 GetThrowDirection()
    {
        return throwPoint.forward;
    }
    

    private void OnSkeetKilled()
    {
        skeet.gameObject.SetActive(false);

        OnThrowCompleted();
        
        Debug.Log("OnSkeetKilled");
    }


    private void OnSkeetCrashed()
    {
        skeet.gameObject.SetActive(false);
        
        OnThrowCompleted();
        
        Debug.Log("OnSkeetCrashed");
    }


    private void OnThrowCompleted()
    {
        onThrowCompleted?.Invoke();
        onThrowCompleted = null;
    }
    
    
    private void OnDisable()
    {
        skeet.OnCrashed -= OnSkeetCrashed;
        skeet.OnKilled -= OnSkeetKilled;
    }


    private void OnDrawGizmos()
    {
        if (throwPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(throwPoint.transform.position, 0.1f);

            Gizmos.color = Color.red;

            Gizmos.DrawLine(throwPoint.transform.position,
                    throwPoint.transform.position + throwPoint.transform.forward * 25f);
        }
    }
}