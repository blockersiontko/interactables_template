using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] [Range(10f, 300f)]
    private float Sensitivity = 300f;

    [SerializeField] private Transform playerBody;

    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // transform.localRotation ustawia lokaln¹ rotacjê obiektu
        // Quaternion.Euler(x, y, z) - tworzy rotacjê k¹tów Eulera, oœ XYZ
        // _xRotation ruch myszy w osi pionowej
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}