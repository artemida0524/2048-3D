using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class TableBase : MonoBehaviour, ITable
{
    // на щот ініціалізації ліста стін думав краще заюзати GetComponentsInChildren<> но вибрав вручну через інспектор
    [SerializeField] protected List<WallBase> walls;
    [SerializeField] protected ThrowingPlatformBase platform;
    
    public List<WallBase> Walls => walls;
    public ThrowingPlatformBase Platform => platform;

}