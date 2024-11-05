using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class CanvasResultScreen : MonoBehaviour
{
    [SerializeField] private TimerHandler timerHandler;

    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;

    private IThrowingPlatform platform;

    [Inject]
    private void Construct(ITable table)
    {
        platform = table.Platform;
    }

    public void EnableLoseScreen(bool active)
    {
        loseScreen.SetActive(active);
    }

    public void EnableWinScreen(bool active)
    {
        winScreen.SetActive(active);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }


    public void Continue()
    {
        platform.CanDrag = true;
        timerHandler.ResetTime();
    }
}
