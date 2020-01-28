using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float turnSpeed;
    public float turretRotation;

    [Header("Camera")]
    public float lookSensitivity;     // mouse look sensitivity
    public float maxLookX;            // lowest down we can look
    public float minLookX;            // highest up we can look
    private float rotY;               // current y rotation of the camera
    
    private Camera cam;
    private Rigidbody rig;

    private void Awake()
    {
        cam = Camera.main;
        rig = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CamLook(GameManager.instance.GetMouseControls());

    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * turnSpeed;
        float z = Input.GetAxis("Vertical")* moveSpeed;
        Vector3 dir = transform.forward * x;
        Quaternion turnHull = Quaternion.Euler(0, x, 0);
        rig.MoveRotation(turnHull);
        rig.velocity = dir;
        
    }

    void CamLook(bool mouseControl)
    {
        if(mouseControl is true)
        {
            
            rotY += Input.GetAxis("Mouse X") * lookSensitivity;

            cam.transform.localRotation = Quaternion.Euler(0, rotY, 0);
        }
        else
        {

        }
    }
}
