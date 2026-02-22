using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header(header: "Spawn")]
    public GameObject virusPrefab;
    public Transform[] spawnPoints;
    public float spawnRadius = 2f;
    public float spawnDelay = 0.3f;
    public LayerMask virusLayer;

    [Header(header: "Waves")]
    public int currentWave = 1;
    public int baseEnemies = 3;
    public bool healDropedThisWave;

    private int aliveEnemies;
    private bool spawning = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(obj: gameObject);
    }

    void Start()
    {
        UIManager.Instance.UpdateWave(wave: currentWave);
        StartCoroutine(routine: StartWave());
    }

    IEnumerator StartWave()
    {
        healDropedThisWave = false;
        
        spawning = true;

        int count = baseEnemies + currentWave;
        aliveEnemies = count;

        for (int i = 0; i < count; i++)
        {
            SpawnVirusSafe();
            yield return new WaitForSeconds(seconds: spawnDelay);
        }

        spawning = false;
    }

    void SpawnVirusSafe()
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            Transform sp = spawnPoints[Random.Range(minInclusive: 0, maxExclusive: spawnPoints.Length)];

            Vector3 randomOffset = new Vector3(
                x: Random.Range(minInclusive: -spawnRadius, maxInclusive: spawnRadius),
                y: 0f,
                z: Random.Range(minInclusive: -spawnRadius, maxInclusive: spawnRadius)
            );

            Vector3 spawnPos = sp.position + randomOffset;

            if (!Physics.CheckSphere(position: spawnPos, radius: 0.6f, layerMask: virusLayer))
            {
                Instantiate(original: virusPrefab, position: spawnPos, rotation: Quaternion.identity);
                return;
            }
        }
        
        Transform fallback = spawnPoints[Random.Range(minInclusive: 0, maxExclusive: spawnPoints.Length)];
        Instantiate(original: virusPrefab, position: fallback.position, rotation: Quaternion.identity);
    }

    public void EnemyDied()
    {
        aliveEnemies--;

        if (aliveEnemies <= 0 && !spawning)
        {
            currentWave++;
            UIManager.Instance.UpdateWave(wave: currentWave);
            StartCoroutine(routine: StartWave());
        }
    }
}
