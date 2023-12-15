using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FinalCam : MonoBehaviour
{
    Camera cam;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        cam.transform.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X")*speed*Time.deltaTime , 0f);
    }
}
