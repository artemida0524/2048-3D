using System.Collections;
using UnityEngine;

public class ColorLerpHandler
{
    public readonly float speed;

    public MeshRenderer MeshRenderer { get; private set; }

    private float time = 0f;
    private const float timeOut = 1f;

    private UnityEngine.Coroutine oldCoroutine;

    private readonly Color currentColor;

    public ColorLerpHandler(MeshRenderer meshRenderer, float speed)
    {
        this.MeshRenderer = meshRenderer;
        this.speed = speed;

        currentColor = MeshRenderer.material.color;
    }


    public void ChangeColor(Color beginColor)
    {
        if (oldCoroutine != null)
        {
            Coroutine.Instance.StopCoroutine(oldCoroutine);
            MeshRenderer.material.color = currentColor;
        }

        time = 0f;
        oldCoroutine = Coroutine.Instance.StartCoroutine(ChangeColorEnumerator(beginColor));
    }


    private IEnumerator ChangeColorEnumerator(Color beginColor)
    {
        while (timeOut > time)
        {
            time += Time.deltaTime * speed;

            Color newColor = Color.Lerp(beginColor, currentColor, time);

            MeshRenderer.material.color = newColor;

            yield return null;
        }
        MeshRenderer.material.color = currentColor;
        yield return null;
    }
}