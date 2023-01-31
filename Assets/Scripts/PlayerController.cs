using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Состояние
    private PlayerState _playerState = PlayerState.OnTheGround;

    //Движение
    private CharacterController _controller;
    private const float PlayerSpeed = 5.0f;
    private const float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    //Ускорение
    private float _accelerationSpeed;
    private const float AccelerationMaxSpeed = 4;
    private const float AccelerationMaxStamina = 5; //запас ускорения
    private float _accelerationStamina = 5;
    [SerializeField] private Slider accelerationMaxStaminaSlider;

    //Закапывание
    [SerializeField] private Slider digInKdSlider;
    private const float DigInKdMax = 20; //кд для закапывания
    private float _currentDigInKd;

    //Камера
    private Transform _camera;
    
    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _camera = FindObjectOfType<Camera>().transform;
    }

    void Update()
    {
        switch (_playerState)
        {
            case PlayerState.OnTheGround:
                Acceleration();
                Movement();
                DigIn();
                break;
            case PlayerState.InTheGround:
                DigOut();
                break;
            case PlayerState.Dead:
                break;
        }
    }

    private void DigIn()
    {
        if (_currentDigInKd > 0)
        {
            _currentDigInKd -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _currentDigInKd <= 0)
        {
            _playerState = PlayerState.InTheGround;
            _currentDigInKd = DigInKdMax;
        }

        digInKdSlider.value = _currentDigInKd / DigInKdMax;
    }

    private void DigOut()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerState = PlayerState.OnTheGround;
            _currentDigInKd = DigInKdMax;
        }
    }

    private void Acceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_accelerationStamina > 0)
            {
                _accelerationSpeed = AccelerationMaxSpeed;
                _accelerationStamina -= Time.deltaTime;
            }
            else
            {
                _accelerationSpeed = 0;
                _accelerationStamina = 0;
            }
        }
        else
        {
            _accelerationSpeed = 0;
            if (_accelerationStamina < AccelerationMaxStamina)
            {
                _accelerationStamina += Time.deltaTime;
            }
            else
            {
                _accelerationStamina = AccelerationMaxStamina;
            }
        }

        accelerationMaxStaminaSlider.value = _accelerationStamina / AccelerationMaxStamina;
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angel, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * Time.deltaTime * (PlayerSpeed + _accelerationSpeed));
        }
    }

}