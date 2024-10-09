using UnityEngine;
using UnityEngine.UI;

public class MenuSeleccion : MonoBehaviour
{
    public GameObject panelMenu;
    public Button MasDaño;
    public Button MenosCooldown;
    public Button MasVelocidad;
    public Button MasVida;
    private EstadosPersonaje estadosPersonaje;

    void Start()
    {
        panelMenu.SetActive(false);
        estadosPersonaje = FindAnyObjectByType<EstadosPersonaje>();

        MasVida.onClick.AddListener(AumentarVida);
        MasVelocidad.onClick.AddListener(AumentarVelocidad);
        MasDaño.onClick.AddListener(AumentarDaño);
        MenosCooldown.onClick.AddListener(ReducirCooldown);
    }

    public void MostrarMenu()
    {
        Time.timeScale = 0;
        panelMenu.SetActive(true);
    }

    public void OcultarMenu()
    {
        Time.timeScale = 1;
        panelMenu.SetActive(false);
    }

    void AumentarVida()
    {
        estadosPersonaje.dataPersonaje.VidaMaxima += 20;
        OcultarMenu();
    }

    void AumentarVelocidad()
    {
        estadosPersonaje.dataPersonaje.Velocidad += 1;
        OcultarMenu();
    }

    void AumentarDaño()
    {
        float nuevoDaño = estadosPersonaje.controladorCuchillo.datosArma.daño + 3;
        estadosPersonaje.controladorCuchillo.datosArma.daño = nuevoDaño;
        OcultarMenu();
    }

    void ReducirCooldown()
    {
        estadosPersonaje.GetComponentInChildren<ControladorCuchillo>().ReducirCooldown(0.9f);
        OcultarMenu();
    }

}
