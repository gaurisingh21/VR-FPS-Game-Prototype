using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class DummyTarget : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public Transform rotationPoint; // The point around which the cube should rotate

    void Update()
    {
        // Rotate the cube around its local Y-axis
        transform.RotateAround(rotationPoint.position, transform.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnCollissionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

}

