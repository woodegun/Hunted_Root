using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Состояние
    public PlayerState _playerState = PlayerState.OnTheGround;

    //Движение
    private CharacterController _controller;
    private float PlayerSpeed;
    private const float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    //web
    private bool isSpeedDecreased;
    private float curDecreasedSpeedTime;
    private float maxDecreasedSpeedTime = 2;
    private float maxDecreasedSpeed = 2;
    private float curDecreasedSpeed = 0;

    //Ускорение
    private float _accelerationSpeed;
    private float AccelerationMaxSpeed;
    private float AccelerationMaxStamina; //запас ускорения
    private float _accelerationStamina = 5;
    [SerializeField] private Slider accelerationMaxStaminaSlider;

    //Закапывание
    [SerializeField] private GameObject baseModel;
    [SerializeField] private Slider digInKdSlider;
    private float DigInKdMax; //кд для закапывания
    private float _currentDigInKd;

    //Камера
    private Transform _camera;

    public GameObject DiePanel;
    public TextMeshProUGUI RestartBtnText;
    private Image diePanelImage;

    private void Start()
    {
        PlayerSpeed = GlobalSettings.INSTANSE.PlayerSpeed;
        AccelerationMaxSpeed = GlobalSettings.INSTANSE.AccelerationMaxSpeed;
        AccelerationMaxStamina = GlobalSettings.INSTANSE.AccelerationMaxStamina;
        DigInKdMax = GlobalSettings.INSTANSE.DigInKdMax;

        _controller = gameObject.GetComponent<CharacterController>();
        _camera = FindObjectOfType<Camera>().transform;
        if (DiePanel != null) diePanelImage = DiePanel.GetComponent<Image>();
    }

    void Update()
    {
        switch (_playerState)
        {
            case PlayerState.OnTheGround:
                SpeedUpdate();
                Acceleration();
                Movement();
                DigIn();
                break;
            case PlayerState.UnderTheGround:
                DigOut();
                break;
            case PlayerState.Dead:
                UpdateDiePanel();
                break;
            case PlayerState.Win:
                break;
        }
    }

    private void DigIn()
    {
        if (_currentDigInKd > 0)
            _currentDigInKd -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space) && _currentDigInKd <= 0)
        {
            _playerState = PlayerState.UnderTheGround;
            _currentDigInKd = DigInKdMax;
            baseModel.SetActive(false);
        }

        if (digInKdSlider != null)
            digInKdSlider.value = _currentDigInKd / DigInKdMax;
        else
            Debug.LogWarning("Setup digInKdSlider");
    }

    private void DigOut()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DoDigOut();
    }

    public void DoDigOut()
    {
        _playerState = PlayerState.OnTheGround;
        _currentDigInKd = DigInKdMax;
        baseModel.SetActive(true);
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
                _accelerationStamina += Time.deltaTime;
            else
                _accelerationStamina = AccelerationMaxStamina;
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
            _controller.Move(moveDir.normalized * Time.deltaTime *
                             (PlayerSpeed + _accelerationSpeed - curDecreasedSpeed));
        }

        _controller.Move(Vector3.down * Time.deltaTime * 3);
    }

    public void Die()
    {
        if (DiePanel != null)
            DiePanel.SetActive(true);
        else
            Debug.LogWarning("Setup DiePanel");

        _playerState = PlayerState.Dead;
    }

    private void UpdateDiePanel()
    {
        if (diePanelImage == null || RestartBtnText == null)
        {
            Debug.LogWarning("Запони diePanelImage & RestartBtnText");
            return;
        }

        var color = diePanelImage.color;
        color.a += Time.deltaTime;
        diePanelImage.color = color;
        if (color.a > 2)
        {
            color = RestartBtnText.color;
            color.a += Time.deltaTime;
            RestartBtnText.color = color;
        }
    }

    public bool isElusive()
    {
        return _playerState == PlayerState.Win || _playerState == PlayerState.UnderTheGround;
    }

    public void DecreaseSpeed()
    {
        curDecreasedSpeedTime = maxDecreasedSpeedTime;
        curDecreasedSpeed = maxDecreasedSpeed;
        isSpeedDecreased = true;
    }

    private void SpeedUpdate()
    {
        if (isSpeedDecreased)
        {
            curDecreasedSpeedTime -= Time.deltaTime;
            if (curDecreasedSpeedTime <= 0)
            {
                isSpeedDecreased = false;
                curDecreasedSpeed = 0;
            }
        }
    }
}