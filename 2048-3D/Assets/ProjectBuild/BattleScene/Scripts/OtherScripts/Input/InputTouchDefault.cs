using UnityEngine;

public class InputTouchDefault : IInput
{
    private readonly Camera cameraMain;
    private readonly Transform target;


    public InputTouchDefault(Camera cameraMain, Transform target)
    {
        this.cameraMain = cameraMain;
        this.target = target;
    }

    public Vector3 GetPosition()
    {

        Vector3 position = Input.mousePosition;

        //position.z -= cameraMain.gameObject.transform.position.z;
        position.z -= cameraMain.gameObject.transform.position.z + target.position.z;



        return cameraMain.ScreenToWorldPoint(position);

    }
}
