using UnityEngine;
using Zenject;


public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private TouchDragInputWriter touchDragInputWriter = null;

    [SerializeField]
    private MainWindow mainWindow = null;

    [SerializeField]
    private SkeetsController skeetsController = null;

    [SerializeField]
    private AutoWeapon weapon = null;
        
    public override void InstallBindings()
    {
        BindGameUIEvents();
        BindAxis2DInputWriter();
        BindSkeetsController();
        BindDelayWeapon();
    }


    private void BindGameUIEvents()
    {
        Container.Bind<IGameEvents>().To<MainWindow>().FromInstance(mainWindow).AsSingle();
    }


    private void BindAxis2DInputWriter()
    {
        Container.Bind<IAxis2DInputWriter>().To<TouchDragInputWriter>().FromInstance(touchDragInputWriter).AsSingle();
    }


    private void BindSkeetsController()
    {
        Container.Bind<IShootTargets>().To<SkeetsController>().FromInstance(skeetsController).AsSingle();
    }


    private void BindDelayWeapon()
    {
        Container.Bind<IDelay>().To<AutoWeapon>().FromInstance(weapon).AsSingle();
    }
}