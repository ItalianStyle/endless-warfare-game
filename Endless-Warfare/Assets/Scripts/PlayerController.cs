
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private Turret turret;
    [SerializeField] private Hull hull;
    //control of the tank mode: true for mouse, false for keyboard
    [SerializeField] private bool controlMode;
    private Rigidbody m_PlayerRigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;

    

    #endregion

    #region Built-in Methods

    private void Awake()
    {
        
        turret = GetComponentInChildren<Turret>();
        hull = GetComponentInChildren<Hull>();

        //Set the cursor state
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
        controlMode = GameManager.instance.GetMouseControls();
    }

    private void Update()
    {
        if (GameManager.instance.paused == false)
        {
            //the game is not paused

            // get hull movements input values
            m_MovementInputValue = Input.GetAxis("Vertical");
            m_TurnInputValue = Input.GetAxis("Horizontal");

            //get turret shots
            if (controlMode == true)
            {
                //mouse controls
                if (Input.GetMouseButton(0))
                {
                    if (turret.CanShoot())
                        turret.Shoot();
                }
            }
            else
            {
                //keyboard controls
                if (Input.GetKey(KeyCode.Space))
                {
                    if (turret.CanShoot())
                        turret.Shoot();
                }
            }
        }      
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.paused == false)
        {
            //move the hull
            hull.Move(m_MovementInputValue);
            hull.Turn(m_TurnInputValue);

            //move the turret
            if (controlMode == false)
            {
                // stop any eventual rotation
                turret.transform.Rotate(Vector3.up, 0f);

                // every fixed frame check if the button to rotate turret is down, if so, keep call the RotateTurret() 
                if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Comma))
                {
                    turret.transform.Rotate(Vector3.up, -turret.rotateSpeed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Period))
                {

                    turret.transform.Rotate(Vector3.up, turret.rotateSpeed * Time.deltaTime);
                }
            }
        }
        else
            turret.FollowCamera();
        
    }

#endregion
}
