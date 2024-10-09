using UnityEngine;

public class Proyectiles : MonoBehaviour
{
    public ArmaScriptableObject dataArma;
    public PersonajeScriptableObject dataPersonaje;

    protected Vector3 direccion;
    public float destruirConElTiempo;

    protected float dañoActual;
    protected float velocidadActual;
    protected float duracionCooldownActual;
    protected int penetracionActual;

    void Awake()
    {
        dañoActual = dataArma.daño;
        velocidadActual = dataArma.velocidad;
        duracionCooldownActual = dataArma.duracionCooldown;
        penetracionActual = dataArma.penetracion;
    }


    protected virtual void Start()
    {
        Destroy(gameObject, destruirConElTiempo);
    }

    public void DireccionChecker(Vector3 dir)
    {
        direccion = dir;

        float direccionX = direccion.x;
        float direccionY = direccion.y;
        Vector3 escala = transform.localScale;
        Vector3 rotacion = transform.rotation.eulerAngles;

        if (direccionX < 0 && direccionY == 0) //Izquierda
        {
            escala.x = escala.x * -1;
            escala.y = escala.y * -1;
        }
        else if (direccionX == 0 && direccionY < 0) //Abajo
        {
            escala.y = escala.y * -1;
        }
        else if (direccionX == 0 && direccionY > 0) //Arriba
        {
            escala.x = escala.x * -1;
        }
        else if (direccionX > 0 && direccionY > 0) //Arriba derecha
        {
            rotacion.z = 0f;
        }
        else if (direccionX > 0 && direccionY < 0) //Abajo derecha
        {
            rotacion.z = -90f;
        }
        else if (direccionX < 0 && direccionY > 0) //Arriba izquierda
        {
            escala.x = escala.x * -1;
            escala.y = escala.y * -1;
            rotacion.z = -90f;
        }
        else if (direccionX < 0 && direccionY < 0) //Abajo izquierda
        {
            escala.x = escala.x * -1;
            escala.y = escala.y * -1;
            rotacion.z = 0f;
        }
        transform.localScale = escala;
        transform.rotation = Quaternion.Euler(rotacion);

    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EstadosEnemigo enemigo = col.GetComponent<EstadosEnemigo>();
            enemigo.RecibirDaño(dañoActual);
            ReducirPenetracion();
        }
    }

    void ReducirPenetracion()
    {
        penetracionActual--;
        if (penetracionActual <= 0)
        {
            Destroy(gameObject);
        }
    }

}
