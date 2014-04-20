using UnityEngine;
using System.Collections;

//makes objects click and draggable across 2D orthographic camera
public class ClickAndDrag : MonoBehaviour
{
    private float distance;
    public bool isDraggable = true;
    private Vector3 initPos;

    void Start()
    {
        initPos = this.transform.position;
        isDraggable = true;
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
        if (this.transform.position == initPos && Input.GetMouseButtonUp(0))
        {
            isDraggable = true;
        }
    }

    void OnMouseDown()
    {
        if(isDraggable)
        {
            distance = (this.transform.position - Camera.main.transform.position).magnitude;
        }
    }

    void OnMouseDrag()
    {
        if (isDraggable)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            this.transform.position = ray.GetPoint(distance);
        }
    }

    void OnMouseUp()
    {
        if (isDraggable)
        {
            this.transform.position = initPos;
        }
    }

    void Return()
    {
        isDraggable = false;
        this.transform.position = initPos;
    }

    void Respawn()
    {
        GameObject newObj = (GameObject)Instantiate(this.gameObject, initPos, Quaternion.identity);
        this.gameObject.tag = "ToClean";
    }
}
