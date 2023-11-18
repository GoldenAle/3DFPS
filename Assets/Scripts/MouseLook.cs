using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSens = 100f;
    [SerializeField] Transform playerBody;
    [SerializeField] float zoomFOV = 60f;
    [SerializeField] float normalFOV = 90f;

    float xRotation = 0f;

    private Vector3 offset;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - playerBody.position;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetMouseButton(1)) //Hold RightClick to zoom
        {
            //Change FOV to zoom in
            Camera.main.fieldOfView = zoomFOV;
        }
        else
        {
            //Change FOV back to normal
            Camera.main.fieldOfView = normalFOV;
        }
    }
}