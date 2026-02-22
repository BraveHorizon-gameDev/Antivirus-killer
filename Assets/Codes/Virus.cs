using UnityEngine;

public class Virus : MonoBehaviour
{
    public float health = 30f;

    [SerializeField] private GameObject healPrefab;
    [SerializeField, Range(0f, 1f)] private float healDropChance = 0.9f;

    private bool _isDead;
    VirusDeathExplosion _explosion;

    void Start()
    {
        _explosion = GetComponent<VirusDeathExplosion>();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        health -= damage;

        if (_explosion)
            _explosion.Hit();

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        if (_isDead) return;
        _isDead = true;

        if (_explosion)
            _explosion.Explode();

        if (!WaveManager.Instance.healDropedThisWave && healPrefab != null && Random.value < healDropChance)
        {
            Instantiate(healPrefab, transform.position, Quaternion.identity);
            WaveManager.Instance.healDropedThisWave = true;
        }

        WaveManager.Instance?.EnemyDied();

        Destroy(gameObject);
    }
}