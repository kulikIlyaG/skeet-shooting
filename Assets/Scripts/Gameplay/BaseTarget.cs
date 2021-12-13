using UnityEngine;


public abstract class BaseTarget : MonoBehaviour
{
    protected bool isCleared = true;
    public bool IsCleared => isCleared;
    public string Id => gameObject.name;
        
        
        
    public virtual void Kill()
    {
    }
        
        
    public virtual void Clear()
    {
        isCleared = true;
    }


    public abstract bool IsEnabled();

    public abstract float GetHealthPercent();

    public abstract bool IsALatestChance();
    
    public override bool Equals(object obj)
    {
        var item = obj as BaseTarget;

        if (item == null)
        {
            return false;
        }

        return Id.Equals(item.Id);
    }
}