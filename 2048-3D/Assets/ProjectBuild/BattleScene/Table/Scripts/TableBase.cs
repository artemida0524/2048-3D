using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class TableBase : MonoBehaviour, ITable
{
    public List<WallBase> Walls => walls;
    public ThrowingPlatformBase Platform => platform;

    [SerializeField] protected List<WallBase> walls;
    [SerializeField] protected ThrowingPlatformBase platform;
}