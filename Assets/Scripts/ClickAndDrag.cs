using UnityEngine;
using System.Collections;

public class ClickAndDrag : MonoBehaviour
{
    private float distance;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnMouseDown()
    {
        distance = (this.transform.position - Camera.main.transform.position).magnitude;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 15);
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        this.transform.position = ray.GetPoint(distance);
    }
}
