using System;
using UnityEngine;

public interface IWall
{
    event Action OnDetect;

    BoxCollider BoxCollider { get; }
    MeshRenderer MeshRenderer { get; }
}
