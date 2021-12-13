using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


public class SkeetThrowersController : MonoBehaviour
{
    [SerializeField]
    private SkeetThrower[] throwers = null;

    private bool isCanThrow = true;
    
    private int latestThrowerIndex = 0;

    private SkeetThrower currentThrower = null;

    private IGameEvents gameEvents = null;

    
    
    [Inject]
    private void Construct(IGameEvents gameEvents)
    {
        this.gameEvents = gameEvents;
    }


    private void OnEnable()
    {
        gameEvents.OnInvokeThrowSkeet += TryThrow;
    }


    [ContextMenu("Try throw")]
    public void TryThrow()
    {
        if (!isCanThrow)
        {
            return;
        }

        isCanThrow = false;
        
        int throwerIndex = GetThrowerIndex();

        latestThrowerIndex = throwerIndex;

        currentThrower = throwers[throwerIndex];

        currentThrower.Throw(OnSkeetFlyingCompleted);
    }


    private void OnSkeetFlyingCompleted()
    {
        isCanThrow = true;
        
        currentThrower = null;
        
        gameEvents.RefreshThrowers();
    }


    private int GetThrowerIndex()
    {
        int throwerIndex = latestThrowerIndex;
        
        throwerIndex++;

        if (throwerIndex >= throwers.Length)
        {
            throwerIndex = 0;
        }
        
        Debug.Log(throwerIndex);

        return throwerIndex;
    }


    private void OnDisable()
    {
        gameEvents.OnInvokeThrowSkeet -= TryThrow;
    }
}