using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensetivity;

    [SerializeField] private Transform _player;

    private float _xRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensetivity * Time.deltaTime;

        MouseXRotation(mouseX);
        MouseyRotation(mouseY);
    }
    private void MouseXRotation(float mouseX)
    {
        _player.Rotate(Vector3.up * mouseX);
    }
    private void MouseyRotation(float mouseY)
    {
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
