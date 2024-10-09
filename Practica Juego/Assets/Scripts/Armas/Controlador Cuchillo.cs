using UnityEngine;

public class ControladorCuchillo : Controlador
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Atacar()
    {
        base.Atacar();

        GameObject cuchilloNuevo = Instantiate(datosArma.prefab);

        cuchilloNuevo.transform.position = transform.position;
        Vector2 ultimaDireccionMovimiento = GetComponentInParent<MoverPersonaje>().ultimaDireccionMovimiento;

        cuchilloNuevo.GetComponent<CuchilloComportamiento>().DireccionChecker(ultimaDireccionMovimiento);
    }



}
