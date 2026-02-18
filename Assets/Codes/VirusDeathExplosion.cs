using UnityEngine;

public class VirusDeathExplosion : MonoBehaviour
{
    [Header(header: "Cube prefab")]
    public GameObject cubePrefab;

    [Header(header: "Hit effect")]
    public int hitCubeCount = 3;
    public float hitForce = 2f;

    [Header(header: "Death effect")]
    public int deathCubeCount = 12;
    public float deathForce = 6f;

    public float explosionRadius = 1f;
    public float cubeLifeTime = 2f;

    public void Hit()
    {
        SpawnCubes(count: hitCubeCount, force: hitForce);
    }

    public void Explode()
    {
        SpawnCubes(count: deathCubeCount, force: deathForce);
    }

    void SpawnCubes(int count, float force)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos =
                transform.position + Random.insideUnitSphere * 0.2f;

            GameObject cube =
                Instantiate(original: cubePrefab, position: spawnPos, rotation: Random.rotation);

            Rigidbody rb = cube.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(
                    explosionForce: force,
                    explosionPosition: transform.position,
                    explosionRadius: explosionRadius
                );
            }

            Destroy(obj: cube, t: cubeLifeTime);
        }
    }
}
