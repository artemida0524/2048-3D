using UnityEngine;

public class InputThrowigPlatformDefault : IInputThrowigPlatform
{
    private readonly IThrowingPlatform platform;
    public InputThrowigPlatformDefault(IThrowingPlatform platform)
    {
        this.platform = platform;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            platform.BeginDrag();
        }

        if (Input.GetMouseButton(0))
        {
            platform.Drag();
        }


        if (Input.GetMouseButtonUp(0))
        {
            platform.EndDrag();
        }



    }
}