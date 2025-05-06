using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector3 _camera;

    public CinemachineVirtualCamera cam;
    public float cameraSpeed = 5f;
    public Transform target;  
    private Vector3 offset = new Vector3(0,0,0);
    private float camera_x;
    private float camera_y;

    Vector3 cam_forward = new Vector3(0, 0, 0);
    Vector3 cam_right = new Vector3(0, 0, 0);

    private float xRotation;
    public Vector3 _came = new Vector3(0, 0, 0);

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stage"))
        {
            other.gameObject.SetActive(true);
        }
    }*/

    public bool isStairsend = false;


    void LateUpdate()
    {
        cam.transform.position = target.position + offset;
    }

    Rigidbody rb;
    
    void Update()
    {
       
        _came.x += (_camera.x * cameraSpeed) * -1;
        _came.y += _camera.y * cameraSpeed;

        cam_forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        cam_right = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;


        //����U�ۗ�
        //_came.x = Mathf.Clamp(_came.x, -90, 90);
        // _came.y = Mathf.Clamp(_came.y, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(_came);

       // target.Rotate(Vector3.up * camera_x);

        
    }

}

