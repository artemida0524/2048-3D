using TMPro;
using UnityEngine;

public class CanvasCounterTimeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    public void View(float time)
    {
        string timeString = ConvertTime(time);

        timeText.text = timeString;
    }

    private string ConvertTime(float time)
    {
        return $"{(int)time / 60} : {(int)time % 60}";
    }
}
