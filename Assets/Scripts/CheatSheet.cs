using UnityEngine;
using UnityEngine.UI;

public class CheatSheet : MonoBehaviour
{

    [SerializeField] private GameObject _player;

    [SerializeField] private Slider _jumpMultSlider;
    [SerializeField] private Slider _speedMultSlider;
    [SerializeField] private Slider _scaleSlider;
    [SerializeField] private Slider _gravitySlider; // 0 / 10
    [SerializeField] private Slider _mass;

    [SerializeField] private Slider platformScale;

    [SerializeField] private Slider _speedBar;
    [SerializeField] private Slider _jumpBar;
    Movement movement;
    Rigidbody2D rb;
    Vector3 originalScale;

    void Start()
    {
        movement = _player.GetComponent<Movement>();
        rb = _player.GetComponent<Rigidbody2D>();
        originalScale = _player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeJumpBySlider();
        ChangeMovementSpeedBySlider();
        ChangeScaleSlider();
        // ChangeGravityBySlider();
        ChangeMassBySlider();
    }


    public void ChangeJumpSlider()
    {
        _jumpMultSlider.value += _player.GetComponent<ChangeValuesByMouse>().jumpValue;
        _jumpBar.value += _player.GetComponent<ChangeValuesByMouse>().jumpValue;
    }

    public void ChangeSpeedSlider()
    {
        _speedMultSlider.value += _player.GetComponent<ChangeValuesByMouse>().speedValue;
        _speedBar.value += _player.GetComponent<ChangeValuesByMouse>().speedValue;
    }


    public void ChangeJumpBySlider()
    {
        movement.JumpForce = _jumpMultSlider.value;
    }

    public void ChangeMovementSpeedBySlider()
    {
        movement.MovementSpeed = _speedMultSlider.value;  
    }

    public void ChangeScaleSlider()
    {
        // Needs adjustment for colliders

        Vector3 defaultScale = originalScale;
        _scaleSlider.value += _player.GetComponent<Player>().scrollValue;
        _player.transform.localScale = new Vector3(_scaleSlider.value * defaultScale.x, _scaleSlider.value * defaultScale.y);
        _player.transform.localScale.Set(_scaleSlider.value * defaultScale.x,
            _scaleSlider.value * defaultScale.y,
            _scaleSlider.value * defaultScale.z);
    }

    public void ChangeGravityBySlider()
    {
        rb.gravityScale = _gravitySlider.value;
    }

    public void ChangeMassBySlider()
    {
        rb.mass = _mass.value;
    }




}
