using System.Collections;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public CameraShake cameraShake;
    public LineRenderer shotLine;
    public float lineDuration = 0.05f;
    private float _rayDistance = 167f;

    public LayerMask virusLayer;
    public float damage = 10f;
    
    public bool canShoot = true;
    
    // playerCamera
    public Camera playerCamera;

    [Header(header: "Audio")]
    public AudioClip shootAudio;
    public float soundStart = 1f;
    public float soundEnd = 2.5f;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = playerCamera.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!canShoot)
            return; 
        
        if (Input.GetMouseButtonDown(button: 0))
        {
            ShootRay();
        }
    }

    void ShootRay()
    {
        PlayShootSound();

        Ray ray = new Ray(
            origin: playerCamera.transform.position,
            direction: playerCamera.transform.forward
        );

        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit, maxDistance: _rayDistance, layerMask: virusLayer))
        {
            Virus virus = hit.collider.GetComponent<Virus>();
            if (virus != null)
            {
                virus.TakeDamage(damage: damage);
            }
        }

        Vector3 start = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;
        Vector3 end = start + direction * _rayDistance;

        if (Physics.Raycast(origin: start, direction: direction, hitInfo: out RaycastHit hitSecond, maxDistance: _rayDistance, layerMask: virusLayer))
        {
            end = hitSecond.point;

            Virus virus = hitSecond.collider.GetComponent<Virus>();
            if (virus) virus.TakeDamage(damage: damage);
        }

        ShowShotLine(start: start, end: end);

        if (cameraShake)
            cameraShake.Shake();
    }

    void PlayShootSound()
    {
        if (!shootAudio || !_audioSource) return;

        _audioSource.Stop();
        _audioSource.clip = shootAudio;
        _audioSource.time = soundStart;
        _audioSource.pitch = Random.Range(minInclusive: 0.95f, maxInclusive: 1.05f);
        _audioSource.Play();

        StartCoroutine(routine: StopSoundAfter(duration: soundEnd - soundStart));
    }

    void ShowShotLine(Vector3 start, Vector3 end)
    {
        if (!shotLine) return;

        shotLine.SetPosition(index: 0, position: start);
        shotLine.SetPosition(index: 1, position: end);
        shotLine.enabled = true;

        Invoke(methodName: nameof(HideShotLine), time: lineDuration);
    }

    void HideShotLine()
    {
        if (shotLine)
            shotLine.enabled = false;
    }


    IEnumerator StopSoundAfter(float duration)
    {
        yield return new WaitForSeconds(seconds: duration);
        _audioSource.Stop();
    }
}
