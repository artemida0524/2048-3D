
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ThrowableItemDefault : ThrowableItemBase
{
    [SerializeField] private TrailRenderer trailRenderer;


    protected override void EnabledComponents(bool active)
    {
        base.EnabledComponents(active);

        trailRenderer.enabled = active;

    }
}