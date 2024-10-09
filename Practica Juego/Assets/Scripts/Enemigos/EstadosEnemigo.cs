using System;
using UnityEngine;

public class EstadosEnemigo : MonoBehaviour, IPoolable
{
    public EnemigoScriptableObject dataEnemigo;

    float velocidadActual;
    float vidaActual;
    float dañoActual;

    private Transform estadosPersonaje;

    public event Action<GameObject> OnReturn;

    // void Awake()
    // {
    //     velocidadActual = dataEnemigo.Velocidad;
    //     vidaActual = dataEnemigo.VidaMaxima;
    //     dañoActual = dataEnemigo.Daño;
    //     estadosPersonaje = Object.FindAnyObjectByType<EstadosPersonaje>();
    // }

    public void InicializarEnemigo(EnemigoScriptableObject data, Vector2 posicion, Transform jugador){
        velocidadActual = data.Velocidad;
        vidaActual = data.VidaMaxima;
        dañoActual = data.Daño;
        estadosPersonaje = jugador;
        GetComponent<BoxCollider2D>().size = data.BoxColliderInfo;
        GetComponent<Animator>().runtimeAnimatorController = data.AnimatorController;
        GetComponent<MovimientoEnemigo>().Jugador = jugador;
        GetComponent<MovimientoEnemigo>().dataEnemigo = data;
        gameObject.SetActive(true);
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;

        if (vidaActual <= 0)
        {
            OtorgarExperiencia();
            Kill();
        }
    }

    public void Kill()
    {
        OnReturn?.Invoke(gameObject);
    }

    private void OtorgarExperiencia()
    {
        if (estadosPersonaje != null)
        {
            estadosPersonaje.GetComponent<EstadosPersonaje>().AumentarExperiencia(dataEnemigo.ExperienciaOtorgada);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.TryGetComponent<EstadosPersonaje>(out EstadosPersonaje personaje))
            personaje.RecibirDaño(dañoActual);
    }
}
