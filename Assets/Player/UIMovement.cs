using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{

    public float localX, localY, offsetX, offsetY;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = cam.ViewportToWorldPoint(new Vector3(localX, localY, cam.nearClipPlane)) + new Vector3(offsetX, offsetY, 0);
    }
}
