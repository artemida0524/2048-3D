using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] CanvasCounterTimeView view;
    [SerializeField] private float time;

    private bool isActive = false;

    private void Update()
    {
        if (isActive)
        {
            time -= Time.deltaTime;

            view.View(time); 
        }
    }

    public float GetTime()
    {
        return time;
    }

    public void EnableTime(bool enable)
    {
        isActive = enable;
    }

    public void ResetTime()
    {
        view.View(0);
    }

    public void StartCount()
    {
        isActive = true;
    }

}