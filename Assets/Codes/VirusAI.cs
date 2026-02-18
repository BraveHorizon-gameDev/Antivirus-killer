using UnityEngine;

[RequireComponent(requiredComponent: typeof(CharacterController))]
public class VirusAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gravity = -25f;
    public float stopDistance = 1f;

    private Transform player;
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag(tag: "Player")?.transform;
        velocity = Vector3.zero;
    }

    void Update()
    {
        if (!player) return;

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;

        Vector3 move = Vector3.zero;

        if (toPlayer.magnitude > stopDistance)
        {
            Vector3 dir = toPlayer.normalized;
            transform.rotation = Quaternion.LookRotation(forward: dir);
            move = dir * moveSpeed;
        }

        Vector3 finalMove = (move + Vector3.up * velocity.y) * Time.deltaTime;
        controller.Move(motion: finalMove);
    }
}
