using UnityEngine;
using System.Collections;

//makes objects click and draggable across 2D orthographic camera
public class ClickAndDrag : MonoBehaviour
{
    private float distance;
    public bool isDraggable = true;
    private Vector3 initPos;
    public Rect canvasRegion = new Rect(0,0,0,0);
    private Texture2D wireTexture;

    void Start()
    {
        initPos = this.transform.position;
        isDraggable = true;

        wireTexture = new Texture2D(256, 256);
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                wireTexture.SetPixel(i, j, Color.clear);
                if (i == 0 || j == 0 || i == 255 || j == 255)
                {
                    wireTexture.SetPixel(i, j, Color.green);
                }
            }
        }
        wireTexture.Apply();
    }

    void Update()
    {
        canvasRegion = new Rect(0, 0, Screen.width*0.4f, Screen.height * 0.8f);

        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
        if (this.transform.position == initPos && Input.GetMouseButtonUp(0))
        {
            isDraggable = true;
        }
        //*
        //*/
        if (canvasRegion.Contains(Input.mousePosition))
        {
            Debug.Log("Position in canvas");
        }
        else
        {
            Debug.Log("No longer in canvas");
        }
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(
            Camera.main.ScreenToWorldPoint(new Vector3(canvasRegion.x + canvasRegion.width / 2.0f, canvasRegion.y + canvasRegion.height / 2.0f, 0)),
            Camera.main.ScreenToWorldPoint(new Vector3(2*canvasRegion.width, 2*canvasRegion.height, 1)));
    }//*/

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
            if (canvasRegion.Contains(Input.mousePosition))
            {
                this.transform.position = initPos;
            }

            Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
            if (pos.x < 0 || pos.x > Screen.width || pos.y < 0 || pos.y > Screen.height - canvasRegion.y - canvasRegion.height)//Screen.height)
            {
                this.transform.position = initPos;
            }
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(canvasRegion.x, Screen.height - canvasRegion.y - canvasRegion.height,canvasRegion.width,canvasRegion.height),wireTexture);
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
