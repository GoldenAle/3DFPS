using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isJumping = false;
    [SerializeField] float friction = 0.3f;
    Rigidbody playerRigidbody;
    Transform camTransform;

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.1f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = (camTransform.forward * moveVertical + camTransform.right * moveHorizontal).normalized;
        direction.y = 0f;

        // Sprinting
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        Vector3 targetVelocity = new Vector3(direction.x * speed, playerRigidbody.velocity.y, direction.z * speed);
        playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, targetVelocity, smoothTime);

        // Friction
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x * (1 - friction),
                playerRigidbody.velocity.y,
                playerRigidbody.velocity.z * (1 - friction)
            );
        }

        // Jumping
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            playerRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}