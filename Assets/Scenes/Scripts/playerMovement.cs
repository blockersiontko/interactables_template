using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    [SerializeField] private Rigidbody rb;
    [Range(1f, 50f)] public float moveSpeed = 5f;

    void Start()
    {
        // Fizyka
        rb = gameObject.GetComponent<Rigidbody>();
        // Wy��czenie grawitacji
        rb.useGravity = false;
        // Zatrzymanie rotacji
        rb.freezeRotation = true;
    }

    void Update()
    {
        // A, D
        float horizontal = Input.GetAxisRaw("Horizontal");
        // W, S
        float vertical = Input.GetAxisRaw("Vertical");

        // Kierunku względem kamery
        Vector3 forwardDirection = transform.forward;
        Vector3 rightDirection = transform.right;

        moveDirection = (rightDirection * horizontal + forwardDirection * vertical).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.z * moveSpeed);
    }
}