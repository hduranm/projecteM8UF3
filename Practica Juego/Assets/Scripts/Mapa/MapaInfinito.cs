using System.Collections.Generic;
using UnityEngine;

public class MapaInfinito : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunksTerreno;
    [SerializeField] private GameObject player;
    [SerializeField] private float checkerRadius;
    [SerializeField] private Vector3 posicionSinTerreno;
    [SerializeField] private LayerMask terrainMask;

    [SerializeField] private MoverPersonaje moverPersonaje;

    public GameObject chunkActual;
    public List<GameObject> chunksSpawneados;
    public GameObject ultimoChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDuration;


    void Start()
    {
        moverPersonaje = FindFirstObjectByType<MoverPersonaje>();

        if (moverPersonaje == null)
        {
            Debug.LogError("No está MoverJugador");
        }
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {

        Vector2 movement = moverPersonaje.Move.ReadValue<Vector2>();

        if (!chunkActual)
        {
            return;
        }

        if (movement.x > 0 && movement.y == 0) // Derecha
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Derecha").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Derecha").position;
                SpawnChunk();
            }
        }
        else if (movement.x < 0 && movement.y == 0) // Izquierda
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Izquierda").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Izquierda").position;
                SpawnChunk();
            }
        }
        else if (movement.x == 0 && movement.y > 0) // Arriba
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Arriba").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Arriba").position;
                SpawnChunk();
            }
        }
        else if (movement.x == 0 && movement.y < 0) // Abajo
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Abajo").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Abajo").position;
                SpawnChunk();
            }
        }
        else if (movement.x > 0 && movement.y > 0) // Arriba derecha
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Arriba derecha").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Arriba derecha").position;
                SpawnChunk();
            }
        }
        else if (movement.x > 0 && movement.y < 0) // Abajo derecha
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Abajo derecha").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Abajo derecha").position;
                SpawnChunk();
            }
        }
        else if (movement.x < 0 && movement.y > 0) // Arriba izquierda
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Arriba izquierda").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Arriba izquierda").position;
                SpawnChunk();
            }
        }
        else if (movement.x < 0 && movement.y < 0) // Abajo izquierda
        {
            if (!Physics2D.OverlapCircle(chunkActual.transform.Find("Abajo izquierda").position, checkerRadius, terrainMask))
            {
                posicionSinTerreno = chunkActual.transform.Find("Abajo izquierda").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int random = Random.Range(0, chunksTerreno.Count);
        ultimoChunk = Instantiate(chunksTerreno[random], posicionSinTerreno, Quaternion.identity);
        chunksSpawneados.Add(ultimoChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in chunksSpawneados)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }

}
