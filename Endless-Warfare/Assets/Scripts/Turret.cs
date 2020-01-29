#define TESTING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks;


public class Turret : Tank
{
    #region Variables
    public ObjectPool bulletPool;
    public Transform muzzle;

    public int currAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float bulletSpeed;

    public float shootRate;

    private float lastShootTime;
    [SerializeField] private bool isPlayer;
    private float yaw = 0.0f;
    private bool left = false;
    private bool right = false;
    private Rigidbody rig;
    [Header("DPS charateristics")]
    public int damage;
    public float reloadSpeed;
    [Header("Movements")]
    [Tooltip("Value used to compute rotation speed with keyboard controls")] public float rotateSpeed;
    [Tooltip("Value used to compute rotation speed with mouse controls")] public float mouseTurretRotation;
    [Tooltip("Make the camera child of the turret and put it here")] public Camera camera;

    #endregion

    #region Built-In Methods

    private void Awake()
    {
        //are we attached to the player;
        if (GetComponentInParent<PlayerController>())
        {
            isPlayer = true;
            camera = GetComponentInChildren<Camera>();
        }
        
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isPlayer is true)
        {
            #if TESTING
            if(GameUI.instance.localMouseControls == true && (left || right))
                transform.Rotate(0, (Input.GetAxis("Mouse X") * mouseTurretRotation * Time.deltaTime), 0, Space.World);
            #else

            if (GameManager.instance.GetMouseControls() == true && (left || right))
            {
                transform.Rotate(0, (Input.GetAxis("Mouse X") * mouseTurretRotation * Time.deltaTime), 0, Space.World);
            }
            #endif
            else
            {
                if (left == true)
                    transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);

                else if (right == true)
                    transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

                else
                    transform.Rotate(Vector3.up, 0f);
            }
        }
    }

#endregion

#region Custom Methods
    //can we shoot a bullet?
    public bool CanShoot()
    {
        if (Time.time - lastShootTime >= shootRate)
        {
            if (currAmmo > 0 || infiniteAmmo == true)
                return true;
        }
        return false;
    }

    public void Shoot()
    {
        lastShootTime = Time.time;

        GameObject bullet = bulletPool.GetObject();

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        //Set the velocity
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
    }

    public void RotateLeft()
    {
        left = true;
    }

    public void RotateRight()
    {
        right = true;
    }

    public void RotateStop()
    {
        left = false;
        right = false;
    }

    public void RotateTurret()
    {
#if TESTING
        if(GameUI.instance.localMouseControls == false)
        {
            // Use keys to rotate
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Comma))
            {
                RotateStop();
                RotateLeft();
            }
            else if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Period))
            {
                RotateStop();
                RotateRight();
            }
            else
                RotateStop();
        }
#else
        if (GameManager.instance.GetMouseControls() == false)
        {
            // Use keys to rotate
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Comma))
            {
                RotateStop();
                RotateLeft();
            }
            else if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Period))
            {
                RotateStop();
                RotateRight();
            }
            else
                RotateStop();
        }
#endif
        else
        {
            //Follow the camera  
            transform.rotation = camera.transform.rotation;
            
        }
        
    }

#endregion
}
