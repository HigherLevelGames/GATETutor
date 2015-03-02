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

			// http://answers.unity3d.com/questions/610440/on-touch-event-on-game-object-on-android-2d.html
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 wp = ray.GetPoint (distance);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);
			if(hit)
			{
				Debug.Log(hit.transform.parent.gameObject.name + hit.transform.gameObject.name);
				hit.transform.gameObject.SendMessage("TouchUpEventHandler",
				                                     0,
				                                     SendMessageOptions.DontRequireReceiver);
			}
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
