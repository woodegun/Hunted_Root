using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Состояние
    private PlayerState _playerState = PlayerState.OnTheGround;

    //Движение
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private const float PlayerSpeed = 5.0f;
    private const float GravityValue = -9.81f;

    //Ускорение
    private float _accelerationSpeed = 0;
    private const float AccelerationMaxSpeed = 4;
    private const float AccelerationMaxStamina = 5; //запас ускорения
    private float _accelerationStamina = 5;
    [SerializeField] private Slider accelerationMaxStaminaSlider;

    //Закапывание
    [SerializeField] private Slider digInKdSlider;
    private const float DigInKdMax = 20; //кд для закапывания
    private float _currentDigInKd = 0;

    private void Start()
    {
        _controller = gameObject.AddComponent<CharacterController>();
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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * (PlayerSpeed + _accelerationSpeed));

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        _playerVelocity.y += GravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}