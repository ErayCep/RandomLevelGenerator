using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHook : MonoBehaviour
{
    Rigidbody2D projectileRB;
    PlayerMovement playerMovement;

    public float bulletSpeed = 10f;

    private void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        projectileRB.velocity = Vector2.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
