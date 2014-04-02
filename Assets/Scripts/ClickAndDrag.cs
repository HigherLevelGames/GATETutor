using UnityEngine;
using System.Collections;

//makes objects click and draggable across 2D orthographic camera
public class ClickAndDrag : MonoBehaviour
{
    private float distance;

    void OnMouseDown()
    {
        distance = (this.transform.position - Camera.main.transform.position).magnitude;
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        this.transform.position = ray.GetPoint(distance);
    }
}
