using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    //Gets the character controller component for future use
    [SerializeField] private CharacterController _characterControl;
    //Gets the players input and direction
    private Vector2 _input;
    private Vector3 _direction;

    //Variables that smooth turning
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    //Variables for speed
    [SerializeField] private float _speed;

    //Variables for gravity
    private float _gravity = -9.81f;
    [SerializeField] private float _gravMult = 3f;
    private float _velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Applies gravity to the player
        ApplyGravity();
        //Rotates player
        ApplyRotation();
        //Moves player
        Movement();
    }

    //Makes player victim to Issac Newton (become affected by gravity)
    private void ApplyGravity()
    {
        _velocity += _gravity * _gravMult * Time.deltaTime;
        _direction.y = _velocity;
    }

    //Faces player to direction being moved
    private void ApplyRotation()
    {
        //Prevents player from facing north whenever nothing is pressed
        if (_input.sqrMagnitude == 0f)
        {
            return;
        }
        //Sets the rotation of player
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    //Moves the player
    private void Movement()
    {
        //Moves the player
        _characterControl.Move(_direction * _speed * Time.deltaTime);
    }
    //When a movement key is pressed
    public void Move(InputAction.CallbackContext context)
    {
        //Checks the movement key pressed and moves in the direction the key was assigned to
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0, _input.y);
    }
}
