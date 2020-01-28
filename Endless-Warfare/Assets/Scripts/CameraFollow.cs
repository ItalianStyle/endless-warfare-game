using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    #region Variables
   
    public Transform target;
    public float rotationSpeed = 45.0f;
    [Header("Limits")]
    public float maxRotation = 85.0f;
    public float initRotation = 10.0f;
    public float minRotation = 5.0f;
    public float offset = 5.0f;
    [Header("Positions")]
    public Vector3 currentRotation = Vector3.zero;
    public Vector3 maxHeight = new Vector3(0, 20, 0);
    public Vector3 initPosition;
    public Vector3 endPos = new Vector3(0, 20, 0);

    private float angle;
    private bool up = false;
    private bool down = false;
    
    #endregion

    #region Built-in Methods
    private void Start()
    {
        transform.localEulerAngles = currentRotation;    
    }
    private void Update()
    {
        if (target != null)
            transform.LookAt(target);
    }

    private void FixedUpdate()
    {
        RotateCamera();
        if (up is true || down is true)
        {
            CapAngle();
        }
    }
    #endregion

    #region Custom Methods

    void CapAngle()
    {
        //Get the camera Y axis position in the turret's local space
        float yPositionInTurretSpace = target.InverseTransformPoint(transform.position).y;

        float minY = 2;
        float maxY = 11; //These are arbitrary values, you can set them according to preference

        bool canMoveUp;
        bool canMoveDown;


        canMoveDown = (yPositionInTurretSpace < minY) ? false : true;

        canMoveUp = (yPositionInTurretSpace > maxY) ? false : true;

        if (up && canMoveUp)
            transform.Translate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (down && canMoveDown)
            transform.Translate(Vector3.down * rotationSpeed * Time.deltaTime);
    }

    public void RotateUp()
    {
        up = true;
    }

    public void RotateDown()
    {
        down = true;
    }

    public void RotateStop()
    {
        up = false;
        down = false;
    }

    public void RotateCamera()
    {
        RotateStop();

        if (Input.GetKey(KeyCode.Q))
            RotateDown();
        
        else if (Input.GetKey(KeyCode.E))
            RotateUp();  
    }
    #endregion
}
