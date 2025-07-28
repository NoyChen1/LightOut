using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private int gridSize = 5;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameTimer _gameTimer;


    public override void InstallBindings()
    {
        Container.Bind<TileFactory>()
            .FromMethod(ctx => new TileFactory(gridSize, _parent))
            .AsSingle()
            .NonLazy();

        Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle();
    }
}
