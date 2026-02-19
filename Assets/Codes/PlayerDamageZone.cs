using UnityEngine;

public class PlayerDamageZone : MonoBehaviour
{
    public float damageRadius = 1.2f;
    public float damageCooldown = 1f;
    public float damageAmount = 10f;

    private float _timer;

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0f) return;

        Collider[] hits = Physics.OverlapSphere(
            position: transform.position,
            radius: damageRadius
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag(tag: "Virus"))
            {
                GetComponent<PlayerController>()?.TakeDamage(damage: damageAmount);
                _timer = damageCooldown;
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center: transform.position, radius: damageRadius);
    }
}
