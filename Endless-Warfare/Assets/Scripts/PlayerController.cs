#define TESTING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private Turret turret;
    [SerializeField] private Hull hull;
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
    }

    private void Update()
    {
        // get hull movements values
        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");

        //turret shots
#if TESTING
        if (GameUI.instance.localMouseControls == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (turret.CanShoot())
                    turret.Shoot();
            }
        }
#else
        if (GameManager.instance.GetMouseControls() == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (turret.CanShoot())
                    turret.Shoot();
            }
        }
#endif
        else if (Input.GetMouseButton(0) && GameManager.instance.paused == false)
        {
            if (turret.CanShoot())
                turret.Shoot();
        }
        
    }

    private void FixedUpdate()
    {
        //move the hull
        hull.Move(m_MovementInputValue);
        hull.Turn(m_TurnInputValue);
        
        //every fixed frame check: if the button to rotate turret is down, if so, keep call the rotateturret function, else stop it
        turret.RotateTurret();
    }

#endregion
}
