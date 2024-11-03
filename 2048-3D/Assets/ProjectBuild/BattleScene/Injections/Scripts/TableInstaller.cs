using UnityEngine;
using Zenject;

public class TableInstaller : MonoInstaller
{
    [SerializeField] private TableBase table;
    // 


    public override void InstallBindings()
    {
        // тут можна добавляти кондішіни яка іммено буде біндитись дошка

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