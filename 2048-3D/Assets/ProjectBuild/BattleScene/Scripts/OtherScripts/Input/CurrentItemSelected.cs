using UnityEngine;

public class CurrentItemSelected : IInputCurrentSelected
{
    private Camera cameraMain;
    public CurrentItemSelected(Camera cameraMain)
    {
        this.cameraMain = cameraMain;
    }

    public bool IsCurrentItemSelected<T>(T currentItem) where T : MonoBehaviour
    {
        Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out T item))
            {
                if (item == currentItem)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
