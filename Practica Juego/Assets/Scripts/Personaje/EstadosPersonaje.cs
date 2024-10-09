using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstadosPersonaje : MonoBehaviour
{
    public PersonajeScriptableObject dataPersonaje;
    public ArmaScriptableObject dataArma;  

    // Variables estados personaje
    [SerializeField] private float VidaActual;
    public float vidaActual => VidaActual;

    [SerializeField] private float RecuperacionActual;
    public float recuperacionActual => RecuperacionActual;

    [SerializeField] private float VelocidadActual;
    public float velocidadActual => VelocidadActual;

    // Variables niveles personaje
    public int experiencia = 0;
    public int nivel = 1;
    public int limiteExperiencia = 100;
    public int aumentarLimiteExperiencia;

    // Variables inmunidad
    public float duracionInmunidad;
    float temporizadorInmunidad;
    bool esInmune;

    private Animator animator;
    private bool estaMuerto = false;
    private MoverPersonaje moverPersonaje;
    public ControladorCuchillo controladorCuchillo;
    private Rigidbody2D rb;
    private float tiempoSobrevivido;
    public MenuSeleccion menuSeleccion;

    void Awake()
    {
        dataPersonaje.ResetValores();
        dataArma.ResetValores();

        VidaActual = dataPersonaje.VidaMaxima;
        RecuperacionActual = dataPersonaje.Recuperacion;
        VelocidadActual = dataPersonaje.Velocidad;

        animator = GetComponent<Animator>();
        moverPersonaje = GetComponent<MoverPersonaje>();
        controladorCuchillo = GetComponentInChildren<ControladorCuchillo>();
        rb = GetComponent<Rigidbody2D>();

        if (menuSeleccion == null)
        {
            menuSeleccion = FindAnyObjectByType<MenuSeleccion>();
        }
    }

    void Update()
    {
        tiempoSobrevivido += Time.deltaTime;
        if (!estaMuerto)
        {
            if (temporizadorInmunidad > 0)
            {
                temporizadorInmunidad -= Time.deltaTime;
            }
            else if (esInmune)
            {
                esInmune = false;
            }
            VelocidadActual = dataPersonaje.Velocidad;
            RecuperarVida();
        }
    }

    public void AumentarExperiencia(int cantidad)
    {
        if (!estaMuerto)
        {
            experiencia += cantidad;
            ComprobarSubidaNivel();
        }
    }

    void ComprobarSubidaNivel()
    {
        if (experiencia >= limiteExperiencia)
        {
            nivel++;
            experiencia -= limiteExperiencia;
            limiteExperiencia += aumentarLimiteExperiencia;

            FindAnyObjectByType<MenuSeleccion>().MostrarMenu(); 
        }
    }

    public void RecibirDaño(float daño)
    {
        if (!esInmune && !estaMuerto)
        {
            VidaActual -= daño;

            temporizadorInmunidad = duracionInmunidad;
            esInmune = true;

            if (VidaActual <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        estaMuerto = true;
        Debug.Log("Personaje ha muerto");

        if (moverPersonaje != null)
        {
            moverPersonaje.enabled = false;
            controladorCuchillo.enabled = false;
        }

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        animator.SetTrigger("Morir");

        PlayerPrefs.SetFloat("TiempoSobrevivido", tiempoSobrevivido);
        StartCoroutine(CargarEscenaGameOver());
    }

    void RecuperarVida()
    {
        if (VidaActual < dataPersonaje.VidaMaxima && !estaMuerto)
        {
            VidaActual += RecuperacionActual * Time.deltaTime;

            if (VidaActual > dataPersonaje.VidaMaxima)
            {
                VidaActual = dataPersonaje.VidaMaxima;
            }
        }
    }

    private IEnumerator CargarEscenaGameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
}
