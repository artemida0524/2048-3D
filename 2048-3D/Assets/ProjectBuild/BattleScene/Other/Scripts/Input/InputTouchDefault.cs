using UnityEngine;

public class InputTouchDefault : IInput
{
    private Camera cameraMain;

    public InputTouchDefault(Camera cameraMain)
    {
        this.cameraMain = cameraMain;
    }

    public Vector3 GetPosition()
    {

        Vector3 position = Input.mousePosition;

        position.z -= cameraMain.gameObject.transform.position.z;

        return cameraMain.ScreenToWorldPoint(position);

    }
}
