using UnityEngine;

[CreateAssetMenu(fileName = "Personaje", menuName = "ScriptableObjects/Personaje", order = 1)]
public class PersonajeScriptableObject : ScriptableObject
{
    public float VidaMaxima = 100;
    public float Velocidad = 4;
    public float Recuperacion;

     public void ResetValores()
    {
        VidaMaxima = 100;
        Velocidad = 4;
        Recuperacion = 1;
    }
}
