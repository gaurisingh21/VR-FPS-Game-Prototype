using UnityEngine;

public class CylinderEnemy : MonoBehaviour
{
    public float speed = 5f;
    public float maxDistanceFromOrigin = 10f;
    public float forceFactor = 100f;

    private Vector3 targetPosition;
    private Rigidbody rb;

    void Start()
    {
        Vector3 randomPosition = Random.insideUnitSphere * maxDistanceFromOrigin;
        randomPosition.y = transform.position.y;
        targetPosition = randomPosition;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Vector3 randomPosition = Random.insideUnitSphere * maxDistanceFromOrigin;
            randomPosition.y = transform.position.y;
            targetPosition = randomPosition;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CylinderTarget"))
        {
            Vector3 awayFromOther = transform.position - collision.transform.position;
            awayFromOther.Normalize();
            rb.AddForce(awayFromOther * forceFactor, ForceMode.Impulse);
        }
    }
}
