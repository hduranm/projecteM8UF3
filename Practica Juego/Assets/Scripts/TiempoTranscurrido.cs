using UnityEngine;
using UnityEngine.UI; 
using TMPro;          

public class TiempoTranscurrido : MonoBehaviour
{
    private float tiempoTranscurrido;
    public Text textoTiempo;
    public TextMeshProUGUI textoTiempoTMP; 
    Transform jugador;
    
    void Start()
    {
        jugador = FindFirstObjectByType<MoverPersonaje>().transform;
        tiempoTranscurrido = 0f;
    }

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        string minutos = Mathf.Floor(tiempoTranscurrido / 60).ToString("00");
        string segundos = (tiempoTranscurrido % 60).ToString("00");

        if (textoTiempoTMP != null && jugador.GetComponent<MoverPersonaje>().enabled)
        {
            textoTiempoTMP.text = minutos + ":" + segundos;
        }
    }
}
