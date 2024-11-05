using System.Collections;
using UnityEngine;
using Zenject;

public class PointHandle : MonoBehaviour
{
    [SerializeField] private PointView view;

    private PointData data;

    private IThrowingPlatform platform;

    [Inject]
    private void Construct(ITable table)
    {
        this.platform = table.Platform;
    }

    private void Start()
    {
        data = new PointData();

        platform.OnDetectSameItem += OnDetectSameItemHandler;

    }

    private void OnDetectSameItemHandler(int number)
    {

        data.number += number / 2;

        view.View(data.number);

    }

    public int GetCount()
    {
        return data.number;
    }

    public void ViewCondition(int count)
    {
        view.ViewCondition(count);
    }

    public void DisableViewCondition()
    {
        view.DisableViewCondition();
    }
}
