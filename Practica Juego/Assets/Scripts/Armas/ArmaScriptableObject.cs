using UnityEngine;

[CreateAssetMenu(fileName = "ArmaScriptableObject", menuName = "ScriptableObjects/Arma")]
public class ArmaScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public float daño;
    public float velocidad;
    public float duracionCooldown;
    public int penetracion;

    public void ResetValores()
    {
        daño = 5;
        velocidad = 10;
        duracionCooldown = 1;
        penetracion = 1;
    }

}
