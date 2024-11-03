using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private TimerHandler timer;
    [SerializeField] private CanvasResultScreen canvasResultScreen;

    [SerializeField] private PointHandle pointHandler;

    [SerializeField] private int conditionCount;

    private IThrowingPlatform platform;

    private bool isSetScreen = true;

    [Inject]
    private void Construct(ITable table)
    {
        platform = table.Platform;
    }

    private void Start()
    {
        platform.OnTouch += OnTouchPlatform;
    }

    private void OnTouchPlatform()
    {
        timer.StartCount();

        platform.OnTouch -= OnTouchPlatform;
    }

    private void Update()
    {
        if (isSetScreen)
        {
            if (pointHandler.GetCount() >= conditionCount)
            {

                EnableWinScreen(true);

                Set();

            }

            if (timer.GetTime() < 0 && pointHandler.GetCount() <= conditionCount)
            {

                EnableLoseScreen(true);

                Set();

            }
        }
    }

    private void Set()
    {
        timer.EnableTime(false);
        platform.CanDrag = false;
        isSetScreen = false;
    }

    public void EnableLoseScreen(bool enable)
    {
        canvasResultScreen.EnableLoseScreen(enable);
    }

    public void EnableWinScreen(bool enable)
    {
        canvasResultScreen.EnableWinScreen(enable);
    }


}
