using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider sliderVida;
    public EstadosPersonaje personaje;

    void Start()
    {
        if (sliderVida != null && personaje != null)
        {
            sliderVida.maxValue = personaje.dataPersonaje.VidaMaxima;
            sliderVida.value = personaje.vidaActual; 
        }
    }

    void Update()
    {
        if (sliderVida != null && personaje != null)
        {
            sliderVida.value = personaje.vidaActual;
        }
    }
}
