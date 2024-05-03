using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_movement : MonoBehaviour
{
    Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vec = transform.position;
        vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * -5;
        vec.z += Input.GetAxis("Vertical") * Time.deltaTime * -5;
        transform.position = vec;
    }
}
