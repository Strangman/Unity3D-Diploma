using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _playerRidgidBody;

    [SerializeField] private bool _canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Move(x, z);
    }
    private void Move(float x, float z)
    {

        Vector3 move = transform.right * x + transform.forward * z;
        _player.position += move * _moveSpeed * Time.deltaTime;
    }
    private void Jump()
    {
        if (_canJump == true)
        {
            _canJump = false;
            _playerRidgidBody.AddForce(new Vector3(0, _jumpForce, 0f));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }
}
