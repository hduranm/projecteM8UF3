using UnityEngine;

public class ActivarChunk : MonoBehaviour
{
    MapaInfinito mapainfinito;
    [SerializeField] private GameObject mapaActual;

    void Start()
    {
        mapainfinito = FindFirstObjectByType<MapaInfinito>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mapainfinito.chunkActual = mapaActual;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (mapainfinito.chunkActual == mapaActual)
            {
                mapainfinito.chunkActual = null;
            }
        }
    }
}
