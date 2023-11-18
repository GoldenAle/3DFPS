using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isJumping = false;
    Rigidbody playerRigidbody;
    Transform camTransform;

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

        playerRigidbody.velocity = new Vector3(direction.x * moveSpeed, playerRigidbody.velocity.y, direction.z * moveSpeed);

        //Jumping
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