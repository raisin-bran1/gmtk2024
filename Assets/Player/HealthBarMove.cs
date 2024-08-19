using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarMove : MonoBehaviour
{

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)) + new Vector3(2, 2, 0);
    }
}
