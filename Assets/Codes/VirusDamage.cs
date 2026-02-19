using UnityEngine;

public class VirusDamage : MonoBehaviour
{
    public float damage = 10f;
    public float damageCooldown = 1f;

    private float _timer;
    
    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(tag: "Player")) return;
        
        _timer -= Time.deltaTime;
        if (_timer > 0f) return;
        
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        
        player.TakeDamage(damage: damage);
        _timer = damageCooldown;
        
        Debug.Log(message: "Trigger with " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tag: "Player"))
            _timer = 0f;
    }
}
