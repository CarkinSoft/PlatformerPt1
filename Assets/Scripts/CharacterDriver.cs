using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDriver : MonoBehaviour
{
    [Header("Movement Config")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float groundAcceleration = 15f;
    [SerializeField] private float airAcceleration = 10f;

    [Header("Jump Config")]
    public float apexHeight = 4.5f;
    public float apexTime = 0.5f;
    [SerializeField] private float maxFallSpeed = 25f;

    CharacterController _controller;
    Animator _animator;
    private float _xVelocity;
    private float _yVelocity;

    Quaternion _facingRight;
    Quaternion _facingLeft;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        // Face +X (right) and -X (left). Unity forward is +Z, so we yaw 90 degrees.
        _facingRight = Quaternion.Euler(0f, 90f, 0f);
        _facingLeft  = Quaternion.Euler(0f, -90f, 0f);
    }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float direction = 0f;
        if (Keyboard.current.dKey.isPressed) direction += 1f;
        if (Keyboard.current.aKey.isPressed) direction -= 1f;

        bool runHeld = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;

        bool jumpPressedThisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
        bool jumpHeld = Keyboard.current.spaceKey.isPressed;

        float gravityModifier = 1f;

        float targetMaxXSpeed = runHeld ? runSpeed : walkSpeed;
        float accel = _controller.isGrounded ? groundAcceleration : airAcceleration;

        if (direction != 0f)
        {
            if (_controller.isGrounded && Mathf.Sign(direction) != Mathf.Sign(_xVelocity))
                _xVelocity = 0f;

            _xVelocity += direction * accel * Time.deltaTime;
            _xVelocity = Mathf.Clamp(_xVelocity, -targetMaxXSpeed, targetMaxXSpeed);

            transform.rotation = (direction > 0f) ? _facingRight : _facingLeft;
        }
        else
        {
            if (_controller.isGrounded)
                _xVelocity = Mathf.MoveTowards(_xVelocity, 0f, groundAcceleration * Time.deltaTime);
        }

        if (_controller.isGrounded)
        {
            if (jumpPressedThisFrame)
                _yVelocity = 2f * apexHeight / apexTime;
        }
        else
        {
            if (!jumpHeld)
                gravityModifier = 2f;
        }

        float gravity = 2f * apexHeight / (apexTime * apexTime);
        _yVelocity -= gravity * gravityModifier * Time.deltaTime;
        _yVelocity = Mathf.Max(_yVelocity, -maxFallSpeed);

        float deltaX = _xVelocity * Time.deltaTime;
        float deltaY = _yVelocity * Time.deltaTime;

        Vector3 deltaPosition = new(deltaX, deltaY, 0f);
        CollisionFlags collisions = _controller.Move(deltaPosition);

        if ((collisions & CollisionFlags.CollidedAbove) != 0)
            _yVelocity = -1f;
        if ((collisions & CollisionFlags.CollidedSides) != 0)
            _xVelocity = 0f;

        _animator.SetFloat("Speed", Mathf.Abs(_xVelocity));
        _animator.SetBool("Grounded", _controller.isGrounded);
        _animator.SetBool("Running", runHeld);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        bool hitFromBelow = hit.moveDirection.y > 0.1f && hit.normal.y < -0.5f;
        if (!hitFromBelow) return;

        var brick = hit.collider.GetComponentInParent<BrickBlock>();
        if (brick != null)
        {
            brick.Break();
            return;
        }

        var question = hit.collider.GetComponentInParent<QuestionBlock>();
        if (question != null)
        {
            question.GiveCoin();
        }
    }
}