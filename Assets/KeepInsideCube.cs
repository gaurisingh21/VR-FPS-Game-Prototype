using UnityEngine;

public class KeepInsideCube : MonoBehaviour
{
    public GameObject cube; // Reference to the cube GameObject
    private Vector3 cubeMin; // The minimum point of the cube's bounds
    private Vector3 cubeMax; // The maximum point of the cube's bounds

    public float moveSpeed = 10f; // The speed at which the game object moves towards the center of the cube


    void Start()
    {
        // Get the bounds of the cube GameObject
        Bounds cubeBounds = cube.GetComponent<Renderer>().bounds;
        cubeMin = cubeBounds.min;
        cubeMax = cubeBounds.max;
    }

    void Update()
    {
        // Check if the game object's position is outside the cube's boundaries
        if (transform.position.x < cubeMin.x || transform.position.x > cubeMax.x ||
            transform.position.y < cubeMin.y || transform.position.y > cubeMax.y ||
            transform.position.z < cubeMin.z || transform.position.z > cubeMax.z)
        {
            // Set the game object's position to the nearest point inside the cube
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, cubeMin.x, cubeMax.x),
                Mathf.Clamp(transform.position.y, cubeMin.y, cubeMax.y),
                Mathf.Clamp(transform.position.z, cubeMin.z, cubeMax.z)
            );

            // Move the game object towards the center of the cube
            //Vector3 cubeCenter = cube.transform.position;
            //Vector3 direction = cubeCenter - transform.position;
            //transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
