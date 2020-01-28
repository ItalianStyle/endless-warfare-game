using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    #region Variables
    public Transform target;//the target object
    public Vector3 CameraPosition; //this vector holds ONLY the Y coord of the camera
    [Header("Limits")]
    public float minY = 2;
    public float maxY = 11; //These are arbitrary values, you can set them according to preference
    public float zoomSpeed = 10.0f;
    public float rotationSpeed = 45.0f;//a speed modifier

    [Header("Zoom parameters")]
    public float maxDistance;
    public float minDistance;
    [SerializeField] private float currDistance;
    [SerializeField] private float offset;

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
        point = target.transform.position;//get target's coords
        transform.LookAt(point);//makes the camera look to it
        CheckPosition();
        offset = Mathf.Abs(Vector3.Distance(target.position, transform.position));
    }

    void FixedUpdate()
    {
        //makes the camera rotate around "point" coords, rotating around its X axis, 20 degrees per second times the speed modifier

        //checks the capabilities of the camera
        CheckPosition();
        if (Input.GetKey(KeyCode.E))
        {
            // if camera can rotate up and it's not in zoomMode)
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

        if(GameManager.instance.GetMouseControls() == true)
        {
            //rotate the camera along the y axes
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0, Space.Self);
        }
       
    }
    #endregion

    #region Custom Methods
    void CheckPosition()
    {
        //Get the camera Y axis position in the turret's local space
        CameraPosition.y = target.InverseTransformPoint(transform.position).y;
        
        //If CameraPosition.y is over the limits, deactivate up/down
        down = (CameraPosition.y < minY) ? false : true;
        up = (CameraPosition.y > maxY) ? false : true;

        //if up or down are deactivated, activate zoomMode
        if (up is false || down is false)
            zoomMode = true;
        //else if the camera can move up and down, deactivate zoomMode
        else if (up && down)
            zoomMode = false;
    }
    #region Zoom handlers
    void ZoomIn()
    {
        currDistance = Mathf.Abs(Vector3.Distance(target.position, transform.position));
        if (currDistance <= minDistance)
            return;
        else
            transform.Translate(transform.forward * zoomSpeed * Time.deltaTime, Space.World);
    }

    void ZoomOut()
    {
        currDistance = Mathf.Abs(Vector3.Distance(target.position, transform.position));
        if (currDistance >= maxDistance)
            return;
        else
            transform.Translate(-transform.forward * zoomSpeed * Time.deltaTime, Space.World);
    }
    #endregion
    #endregion
}
