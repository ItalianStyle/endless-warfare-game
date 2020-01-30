
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    #region Variables
    public Transform target;//the target object
    
    public Vector3 initialPosition = new Vector3(0f, 7.33f, -15.44f);
    [Header("Limits")]
    // angles in degress starting from coord (X=radius, Y=0)
    public float minAngle = 2;
    public float maxAngle = 11; //These are arbitrary values, you can set them according to preference
    public float zoomSpeed = 10.0f;
    public float rotationSpeed = 45.0f;//a speed modifier

    [Header("Zoom parameters")]
    public float maxRadius;
    public float minRadius;
    [SerializeField] private float currDistance;
    [SerializeField] private float radius;

    [Header("Booleans")]
    [SerializeField] private bool up = false;  //can the camera go up?
    [SerializeField] private bool down = false;  //can the camera go down?
    [SerializeField] private bool zoomMode = false;  //is the camera in zoomMode?;

    private Vector3 point;//the coord to the point where the camera looks at
    #endregion

    #region Built-in Methods
    void Start()
    {
        //Set up things on the start method
        SetCamera();

        //take the radius
        radius = Mathf.Abs(Vector3.Distance(target.position, transform.position));

        //Check the camera position
        CheckPosition();
    }

    void SetCamera()
    {
        //set initial position
        transform.position = initialPosition;
        //get target's coords
        point = target.position;
        //makes the camera look to it 
        transform.LookAt(point);     
    }
    void FixedUpdate()
    {
        //makes the camera rotate around "point" coords, rotating around its X axis, 20 degrees per second times the speed modifier

        //checks the capabilities of the camera
        CheckPosition();
        if (Input.GetKey(KeyCode.E))
        {
            // if camera can rotate up and it's not in zoomMode
            if (up is true && zoomMode is false)
                //then the camera have free rotation
                transform.RotateAround(target.transform.position, target.right, rotationSpeed * Time.deltaTime);

            // else if the camera is in zoom mode
            else if (zoomMode is true)
                // then the camera is locked in higher or lower angle
                ZoomOut();
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            //if camera can rotate down and it's not in zoomMode
            if (down is true && zoomMode is false)
                //then the camera have free rotation
                transform.RotateAround(target.transform.position, target.right, -rotationSpeed * Time.deltaTime);

            //else if the camera is in zoomMode
            else if (zoomMode is true)
                // then the camera is locked in higher or lower angle
                ZoomIn();
        }

        if (GameManager.instance.GetMouseControls() == true)
            //rotate the camera along the y axes
            transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime, 0, Space.Self);
 

    }
    #endregion

    #region Custom Methods
    void CheckPosition()
    {
        //Get current rotation
        Vector3 currentRotation = transform.eulerAngles;

        // if the current rotation is over the limits, activate zoomMode
        zoomMode = (minAngle > currentRotation.x || currentRotation.x > maxAngle) ? true : false;
    }

#region Zoom handlers
    void ZoomIn()
    {
        currDistance = Mathf.Abs(Vector3.Distance(target.position, transform.position));
        if (currDistance <= minRadius)
            return;
        else
            transform.Translate(transform.forward * zoomSpeed * Time.deltaTime, Space.World);
    }

    void ZoomOut()
    {
        currDistance = Mathf.Abs(Vector3.Distance(target.position, transform.position));
        if (currDistance >= maxRadius)
            return;
        else
            transform.Translate(-transform.forward * zoomSpeed * Time.deltaTime, Space.World);
    }
#endregion
#endregion
}
