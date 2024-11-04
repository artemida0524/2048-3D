using UnityEngine;
using Zenject;

public class TableInstaller : MonoInstaller
{
    [SerializeField] private TableBase table;

    public override void InstallBindings()
    {
        BindTable(table);

    }

    private void BindTable(TableBase table)
    {
        Container
            .Bind<ITable>()
            .FromComponentInNewPrefab(table)
            .AsSingle()
            .NonLazy()
            ;
    }
}