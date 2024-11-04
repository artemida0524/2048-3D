using TMPro;
using UnityEngine;

public class PointView : MonoBehaviour
{
    private const string ADD_PATH = "Add";

    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private TextMeshProUGUI conditionText;

    [SerializeField] private Animator animator;

    public void View(int number)
    {
        animator.SetTrigger(ADD_PATH);

        count.text = number.ToString();
    }

    public void SetColorYellow()
    {
        count.color = Color.yellow;
    }

    public void SetColorDefault()
    {
        count.color = Color.white;
    }

    public void ViewCondition(int count)
    {
        conditionText.text = $"Goal: {count}";
    }

    public void DisableViewCondition()
    {
        conditionText.text = string.Empty;
    }

}
