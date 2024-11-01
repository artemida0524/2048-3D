using UnityEngine;
using Zenject;

public class TableInstaller : MonoInstaller
{
    [SerializeField] private TableBase table;

    public override void InstallBindings()
    {
        Container
            .Bind<ITable>()
            .FromComponentInNewPrefab(table)
            .AsSingle()
            .NonLazy()
            ;


        Container
            .Bind<ThrowingPlatformBase>()
            .FromInstance(table.Platform)
            .AsSingle()
            ;

    }
}