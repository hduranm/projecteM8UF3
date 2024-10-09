using UnityEngine;
using UnityEngine.InputSystem;

public class MoverPersonaje : MonoBehaviour
{
    [SerializeField] private InputActionAsset _ActionAsset;
    private InputActionAsset _Input;

    private InputAction _Move;
    public InputAction Move => _Move;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float _Speed;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 _direccionMovimiento;
    public Vector2 direccionMovimiento => _direccionMovimiento;

    private Vector2 _ultimaDireccionMovimiento;
    public Vector2 ultimaDireccionMovimiento => _ultimaDireccionMovimiento;

    public PersonajeScriptableObject dataPersonaje;

    void Start()
    {
        _Input = Instantiate(_ActionAsset);
        _Move = _Input.FindActionMap("Player").FindAction("Move");
        _Input.FindActionMap("Player").Enable();

        _ultimaDireccionMovimiento = new Vector2(1, 0f);

        spriteRenderer = GetComponent<SpriteRenderer>();

        _Speed = dataPersonaje.Velocidad;
    }


    void Update()
    {
        Vector2 movement = _Move.ReadValue<Vector2>();

        if (movement.magnitude > 0)
        {
            rb.velocity = movement * _Speed;
            _direccionMovimiento = movement.normalized;
            _ultimaDireccionMovimiento = _direccionMovimiento;
        }
        else
        {
            rb.velocity = Vector2.zero;
            _direccionMovimiento = Vector2.zero;
        }

        float velocity = movement.magnitude;
        animator.SetFloat("Velocity", velocity);

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

}
