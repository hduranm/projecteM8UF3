using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemigo")]
public class EnemigoScriptableObject : ScriptableObject
{
    [SerializeField] private float velocidad;
    public float Velocidad => velocidad;
    [SerializeField] private float vidaMaxima;
    public float VidaMaxima => vidaMaxima;
    [SerializeField] private float daño;
    public float Daño => daño;
    [SerializeField] private int experienciaOtorgada;
    public int ExperienciaOtorgada => experienciaOtorgada;
    [SerializeField] private Vector2 boxColliderInfo;
    public Vector2 BoxColliderInfo => boxColliderInfo;
    [SerializeField] private AnimatorController animator;
    public AnimatorController AnimatorController => animator;
}
