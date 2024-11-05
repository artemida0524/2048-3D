using System.Collections;
using UnityEngine;

public class WallDefault : WallBase
{
    protected ColorLerpHandler colorChanger;

    protected void Start()
    {
        OnDetect += ChangeColorOnCollision;
        colorChanger = new ColorLerpHandler(MeshRenderer, 1f);
    }

    
    protected void ChangeColorOnCollision()
    {
        Color beginColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        colorChanger.ChangeColor(beginColor);
    }
}
