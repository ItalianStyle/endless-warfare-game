using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks;


public class Hull : Tank
{
    #region Variables

    private Rigidbody rig;
    [Header("Stats")]
    public int currHp;
    public int maxHp;
    [Header("Movement")]
    public float MaxSpeed = 44.08f;
    public float acceleration = 1000f;
    public float m_TurnSpeed = 120f;
    public float currentSpeed;
    public float torque = 120f;



    #endregion

    #region Built-in Methods

    private void Awake()
    {
        rig = GetComponentInParent<Rigidbody>();
    }

    #endregion

    #region Custom Methods
    public void Move(float moveRatio)
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * moveRatio * acceleration * Time.deltaTime;
        //Updates speedometer
        currentSpeed = rig.velocity.magnitude * 3.6f;
        // Apply this movement to the rigidbody's position.
        rig.AddForce(movement, ForceMode.Acceleration);
        //Fissa un limite di velocità
        CapSpeed(rig);
    }

    public void Turn(float turnRatio)
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = turnRatio * m_TurnSpeed * Time.deltaTime;

         // Make this into a rotation in the y axis.
         Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

         // Apply this rotation to the hull rigidbody's rotation.
         rig.MoveRotation(rig.rotation * turnRotation);       
    }

    public void CapSpeed(Rigidbody rigid)
    {
        float speed = rigid.velocity.magnitude;
        speed *= 3.6f;
        if (speed > MaxSpeed)
            rigid.velocity = (MaxSpeed / 3.6f) * rigid.velocity.normalized;
    }

    
    #endregion
}
