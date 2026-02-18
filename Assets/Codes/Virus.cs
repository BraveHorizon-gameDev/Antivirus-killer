
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float health = 30f;
    private bool isDead = false;
    VirusDeathExplosion explosion;

    void Start()
    {
        explosion = GetComponent<VirusDeathExplosion>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;

        if (explosion)
            explosion.Hit();

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (explosion)
            explosion.Explode();

        WaveManager.Instance?.EnemyDied();

        Destroy(obj: gameObject);
    }
}
