using UnityEngine;

public class Target : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;
    private AudioSource _audioSource;
    private ParticleSystem _particleSystem;

    private Vector3 _randomRotation;
    private bool _isDisabled;

    public ScoreManager scoreManager;
    public GameSessionManager gameSessionManager;

    //private int targetId;

    public int targetNumber;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
        _particleSystem = GetComponentInChildren<ParticleSystem>(); 

        _randomRotation = new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
    }

    private void Update() => Rotate();

    private void Rotate() => transform.Rotate(_randomRotation);

    private void OnCollisionEnter(Collision other)
    {
        if (!_isDisabled && other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            scoreManager.UpdateScore(10);
            ToggleTarget();
            TargetDestroyEffect();
            Invoke("ToggleTarget", 0f);

            // Get the target hit data from the GameSessionManager
            TargetHitData hitData = gameSessionManager.gameData.targetHits.Find(data => data.targetId == targetNumber);

            // If target hit data does not exist, create a new one and add it to the list
            if (hitData == null)
            {
                hitData = new TargetHitData();
                hitData.targetId = targetNumber;
                gameSessionManager.gameData.targetHits.Add(hitData);
            }

            // Update the hit time of the target hit data
            hitData.hitTime = Time.time - gameSessionManager.startTime;

            // Print the target hit data to the console
            Debug.Log("Target ID: " + hitData.targetId + ", Hit Time: " + hitData.hitTime);
        }
    }

    private void ToggleTarget()
    {
        _meshRenderer.enabled = !_isDisabled;
        _boxCollider.enabled = !_isDisabled;

        _isDisabled = !_isDisabled;
    }

    private void TargetDestroyEffect()
    {
        var random = Random.Range(0.8f, 1.2f);
        _audioSource.pitch = random;

        _audioSource.Play();
        _particleSystem.Play();
    }
}
