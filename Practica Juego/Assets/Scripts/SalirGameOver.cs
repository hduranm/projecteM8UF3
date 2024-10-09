using UnityEngine;
using UnityEngine.SceneManagement;

public class SalirGameOver : MonoBehaviour
{
    public void Salir()
    {
        SceneManager.LoadScene("Start");
    }
}
