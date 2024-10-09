using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public ArmaScriptableObject datosArma;
    float cooldownActual;

    [SerializeField] protected MoverPersonaje moverPersonaje;


    protected virtual void Start()
    {
        Assert.IsNotNull(moverPersonaje);
        cooldownActual = datosArma.duracionCooldown;
    }

    protected virtual void Update()
    {
        cooldownActual -= Time.deltaTime;
        if (cooldownActual <= 0f)
        {
            Atacar();
        }
    }

    protected virtual void Atacar()
    {
        cooldownActual = datosArma.duracionCooldown;
    }

    public void ReducirCooldown(float reduccion)
    {
        datosArma.duracionCooldown *= reduccion;
    }
}
