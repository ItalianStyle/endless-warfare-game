using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float lifetime;
    private float shootTime;

    private void OnEnable()
    {
        shootTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - shootTime >= lifetime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // did we hit the player or enemy?
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            other.GetComponent<Tank>().TakeDamage(damage);
        }

        //disable the bullet
        gameObject.SetActive(false);
    }
}
