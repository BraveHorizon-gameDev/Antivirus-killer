
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float health = 30f;
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

        WaveManager.Instance?.EnemyDied();

        Destroy(obj: gameObject);
    }
}
