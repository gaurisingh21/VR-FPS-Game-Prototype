using UnityEngine;
using UnityEngine.UI;

public class GunAim : MonoBehaviour
{
    [SerializeField] private Image reticleImage;

    void Update()
    {
        // Get the position and rotation of the gun
        var gunPosition = transform.position;
        var gunRotation = transform.rotation;

        // Calculate the direction the gun is aiming
        var aimDirection = gunRotation * Vector3.forward;

        // Cast a ray from the gun's position in the direction it's aiming
        if (Physics.Raycast(gunPosition, aimDirection, out var hit))
        {
            // If the ray hits something, set the reticle position to the point of the hit
            reticleImage.transform.position = hit.point;
        }
        else
        {
            // If the ray doesn't hit anything, set the reticle position to the maximum range of the gun
            reticleImage.transform.position = gunPosition + aimDirection * 100f;
        }
    }
}
