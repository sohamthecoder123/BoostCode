using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensX, sensY;

    public Transform cam, orientation;

    float mouseX, mouseY;

    float multiplier = 0.01f;

    float xRot, yRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MyInput();

        cam.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRot, 0f);
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * multiplier;
        xRot -= mouseY * sensY * multiplier;

        xRot = Mathf.Clamp(xRot, -90, 90);
    }
}
