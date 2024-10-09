using UnityEngine;
using TMPro;  

public class TiempoSobrevivido : MonoBehaviour
{
   public TextMeshProUGUI textoTiempo;

    void Start()
    {
        float tiempoSobrevivido = PlayerPrefs.GetFloat("TiempoSobrevivido", 0);

        string minutos = Mathf.Floor(tiempoSobrevivido / 60).ToString("00");
        string segundos = (tiempoSobrevivido % 60).ToString("00");

        if (textoTiempo != null)
        {
            textoTiempo.text = "Tiempo sobrevivido: " + minutos + ":" + segundos;
        }
    }
}
