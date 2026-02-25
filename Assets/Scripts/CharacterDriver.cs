using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDriver : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float groundAcceleration = 15f;

    Vector2 _velocity;
    CharacterController _controller;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
        _controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0f;
        if (Keyboard.current.dKey.isPressed) direction += 1f;
        if (Keyboard.current.aKey.isPressed) direction -= 1f;

        _velocity.x += groundAcceleration * Time.deltaTime;
        if (direction != 0f)
        {
            _velocity.x = direction * groundAcceleration * Time.deltaTime;
            _velocity.x = Mathf.Clamp(_velocity.x, -walkSpeed, walkSpeed);
        }
        else _velocity.x = 0f;

        float deltaX = direction * walkSpeed * Time.deltaTime;
        Vector3 deltaPosition = new(deltaX, 0f, 0f);
        _controller.Move(deltaPosition);
    }
}