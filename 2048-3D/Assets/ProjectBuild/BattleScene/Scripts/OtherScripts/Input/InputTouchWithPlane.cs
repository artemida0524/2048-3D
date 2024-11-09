using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchWithPlane : IInput
{
    private readonly Camera cameraMain;
    private readonly Transform target;

    public InputTouchWithPlane(Camera cameraMain, Transform target)
    {
        this.cameraMain = cameraMain;
        this.target = target;
    }

    public Vector3 GetPosition()
    {

        Plane plane = new Plane(cameraMain.transform.forward, target.transform.position);

        Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 endPosition = ray.GetPoint(distance);
            return new Vector3(endPosition.x, endPosition.y, target.transform.position.z);
        }

        throw new System.Exception();
    }
}
