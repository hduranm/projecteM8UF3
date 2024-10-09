using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    private EnemigoScriptableObject _DataEnemigo;
    public EnemigoScriptableObject dataEnemigo  { set => _DataEnemigo = value; }
    Transform _Jugador;
    public Transform Jugador { set => _Jugador = value; }
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        //_Jugador = FindFirstObjectByType<MoverPersonaje>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(_Jugador == null)
            return;

        if (_Jugador.GetComponent<MoverPersonaje>().enabled)
        {
            transform.position = Vector2.MoveTowards(transform.position, _Jugador.position, _DataEnemigo.Velocidad * Time.deltaTime);
        }

        Vector2 direccion = _Jugador.position - transform.position;

        if (direccion.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direccion.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
