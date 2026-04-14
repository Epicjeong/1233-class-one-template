using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    //Gets the character controller component for future use
    [SerializeField] private CharacterController _characterControl;
    //Gets the players input and direction
    private Vector2 _input;
    [SerializeField] private Vector3 _direction;

    //Variables that smooth turning
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    //Variables for speed
    [SerializeField] private float _speed;

    //Variables for gravity
    private float _gravity = -9.81f;
    [SerializeField] private float _gravMult = 3f;
    private float _velocity;

    //Variables for jumping and double jumping
    [SerializeField] private float jumpForce;
    [SerializeField] private int _maxJumps = 2;
    private int _numberOfJumps;
    private bool IsGrounded() => _characterControl.isGrounded;
    
    [SerializeField] private ProjectileWeapon _weapon;
    [SerializeField] private GameObject _target;

    [SerializeField] private Health _health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        if (_health == null) GetComponent<Health>();
    }

    public void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDied += HandleDied;
        }
    }

    public void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDied -= HandleDied;
        }
    }

    private void HandleDamaged(DamageInfo info)
    {
        if (_health != null)
        {

        }
    }
    private void HandleDied()
    {
        Debug.Log("You are dead, not big suprise");
        GameMgr.Instance.GameOver();
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
        //Animates player
        AnimParameters();
    }

    //Makes player victim to Issac Newton (become affected by gravity)
    private void ApplyGravity()
    {
        //Makes sure gravity does not build up while grounded
        if (_characterControl.isGrounded && _velocity < 0f)
        {
            _velocity = -1f;
        }
        else
        {
            _velocity += _gravity * _gravMult * Time.deltaTime;
        }
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

    //When the jump key is pressed
    public void Jump(InputAction.CallbackContext context)
    {
        //Makes sure the player cant jump when they either have no jumps left or is midair
        if (!context.started) return;
        if (!IsGrounded() && _numberOfJumps >= _maxJumps) return;
        if (_numberOfJumps == 0)
        {
            StartCoroutine(WaitForLanding());
        }

        _numberOfJumps++;
        _velocity = jumpForce;
    }

    public void Aim(InputAction.CallbackContext context)
    {   
        _target.SetActive(true);
        if (context.canceled)
        {
            _target.SetActive(false);
            if (_weapon.CanFire)
            {
                _weapon.Fire(_target.transform.position);
            }
        }
        
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);
        _numberOfJumps = 0;
    }

    [SerializeField] private Animator _animator;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    private void AnimParameters()
    {
        _animator.SetFloat(Speed, _input.sqrMagnitude);
        _animator.SetFloat(Velocity, _velocity);
    }
}
