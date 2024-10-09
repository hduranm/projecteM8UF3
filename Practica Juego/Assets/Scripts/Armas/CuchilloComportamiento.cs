using UnityEngine;

public class CuchilloComportamiento : Proyectiles
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.position += direccion * dataArma.velocidad * Time.deltaTime;
    }

}
