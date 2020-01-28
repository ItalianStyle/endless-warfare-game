using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks;

public class RotatePlatform : MonoBehaviour
{
    public float turnSpeed = 10f;
    private float angle;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * turnSpeed;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        player.rotation = Quaternion.Euler(0, angle, 0);
        
    }
}
