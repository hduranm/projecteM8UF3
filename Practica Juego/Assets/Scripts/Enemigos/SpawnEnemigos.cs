using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    [System.Serializable]
    public class Oleada
    {
        public string nombreOleada;
        public List<GrupoEnemigos> gruposEnemigos;
        public float intervaloSpawn;
        public float tiempoEntreOleadas;
    }

    [System.Serializable]
    public class GrupoEnemigos
    {
        public string nombreEnemigo;
        public int totalEnemigos;
        public int totalSpawn;
        public EnemigoScriptableObject dataEnemigo;
    }

    public Oleada oleada;
    [SerializeField] private int oleadaActual = 1;
    private Transform jugador;

    private Pool pool;

    void Awake()
    {
        jugador = FindAnyObjectByType<EstadosPersonaje>().transform;
        pool = GetComponent<Pool>();
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            int capacidadOleadaActual = 0;
            int enemigosSpawneadosOleadaActual = 0;

            foreach (var grupoEnemigo in oleada.gruposEnemigos)
            {
                if (grupoEnemigo.totalEnemigos + oleadaActual >= 0)
                {
                    grupoEnemigo.totalSpawn = 0;
                    grupoEnemigo.totalEnemigos += oleadaActual;
                    capacidadOleadaActual += grupoEnemigo.totalEnemigos;
                }
            }

            while (enemigosSpawneadosOleadaActual < capacidadOleadaActual)
            {
                foreach (var grupoEnemigo in oleada.gruposEnemigos)
                {
                    if (grupoEnemigo.totalSpawn < grupoEnemigo.totalEnemigos)
                    {
                        Vector2 posicionSpawn;
                        float distanciaMinima = 10f;
                        float distanciaMaxima = 15f;

                        Vector2 direccionAleatoria = Random.insideUnitCircle.normalized;

                        float distanciaSpawn = Random.Range(distanciaMinima, distanciaMaxima);

                        posicionSpawn = new Vector2(jugador.position.x, jugador.position.y) + direccionAleatoria * distanciaSpawn;


                        GameObject enemigo = pool.GetElement();
                        if (enemigo != null)
                        {
                            enemigo.GetComponent<EstadosEnemigo>().InicializarEnemigo(grupoEnemigo.dataEnemigo, posicionSpawn, jugador);
                            enemigo.GetComponent<IPoolable>().OnReturn += DevolverEnemigo;
                            grupoEnemigo.totalSpawn++;
                            enemigosSpawneadosOleadaActual++;
                        }
                        // Instantiate(grupoEnemigo.prefabEnemigos, posicionSpawn, Quaternion.identity);

                    }
                }
                yield return new WaitForSeconds(oleada.intervaloSpawn);
            }

            yield return new WaitForSeconds(oleada.tiempoEntreOleadas);

            oleadaActual++;
        }
    }

    private void DevolverEnemigo(GameObject enemigo){
        enemigo.GetComponent<IPoolable>().OnReturn -= DevolverEnemigo;
        pool.ReturnElement(enemigo);

    }

}
