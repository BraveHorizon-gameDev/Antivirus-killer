using UnityEngine;

[RequireComponent(requiredComponent: typeof(CharacterController))]
public class VirusAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gravity = -25f;
    public float stopDistance = 1f;

    private Transform _player;
    private CharacterController _controller;
    private Vector3 _velocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag(tag: "Player")?.transform;
        _velocity = Vector3.zero;
    }

    void Update()
    {
        if (!_player) return;

        if (_controller.isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;

        _velocity.y += gravity * Time.deltaTime;

        Vector3 toPlayer = _player.position - transform.position;
        toPlayer.y = 0f;

        Vector3 move = Vector3.zero;

        if (toPlayer.magnitude > stopDistance)
        {
            Vector3 dir = toPlayer.normalized;
            transform.rotation = Quaternion.LookRotation(forward: dir);
            move = dir * moveSpeed;
        }

        Vector3 finalMove = (move + Vector3.up * _velocity.y) * Time.deltaTime;
        _controller.Move(motion: finalMove);
    }
}
