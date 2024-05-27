using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovementParametrs _params;

    [SerializeField] Camera _camera;
    [SerializeField] InputActionReference _movementControl;
    [SerializeField] InputActionReference _jumpControl;
    [SerializeField] InputActionReference _sprintControl;
    [SerializeField] InputActionReference _escPressed;

    private Transform _cameraMainTransform;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public bool IsMoving { get; private set; }

    public UnityEvent OnEscapePressed = new UnityEvent();

    public PlayerState PlayerState { get; private set; }

    private void OnEnable()
    {
        _movementControl.action.Enable();
        _sprintControl.action.Enable();
        _jumpControl.action.Enable();
        _escPressed.action.Enable();
    }

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        _cameraMainTransform = _camera.transform;

        _escPressed.action.performed += (e) =>
        {
            OnEscapePressed.Invoke();
        };
    }

    private void Start()
    {
        PlayerState = PlayerState.Stay;
    }

    public void UpdatePlayerInput()
    {

    }

    public void UpdatePlayerMovement()
    {
        PlayerState = PlayerState.Stay;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = _movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        //update gravity
        playerVelocity.y += _params.GravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            move = _cameraMainTransform.forward * move.z + _cameraMainTransform.right * move.x;
            move.y = 0;

            if (_sprintControl.action.IsPressed())
            {
                controller.Move(move * Time.deltaTime * _params.RunSpeed);
                PlayerState = PlayerState.Run;
            }
            else
            {
                controller.Move(move * Time.deltaTime * _params.MoveSpeed);
                PlayerState = PlayerState.Walk;
            }

            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + _cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _params.RotationSpeed);
        }

        // Changes the height position of the player..
        if (_jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_params.JumpHeight * -3.0f * _params.GravityValue);
            PlayerState = PlayerState.Jump;
        }
    }

    private void OnDisable()
    {
        _movementControl.action.Disable();
        _sprintControl.action.Enable();
        _jumpControl.action.Disable();
        _escPressed.action.Disable();
    }
}
