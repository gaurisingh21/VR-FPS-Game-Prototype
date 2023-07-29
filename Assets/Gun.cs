using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public float damage = 1000f;
    public float impactForce = 1000f;
    public float range = 400f;
    public float firingrate = 150f;

    public Camera FPScam;
    private float nextimetoFire = 0f;

    public ScoreManager scoreManager;

    [SerializeField] private AudioSource hitSound;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // Get the XRGrabInteractable component attached to the gun object
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        // Subscribe to the activated event of the XRGrabInteractable component
        grabInteractable.activated.AddListener(Shoot);
    }

    private void OnDisable()
    {
        // Unsubscribe from the activated event of the XRGrabInteractable component
        grabInteractable.activated.RemoveListener(Shoot);
    }

    void Shoot(ActivateEventArgs args)
    {
        Debug.Log("Shoot function called.");
        //int layerMask = 1 << LayerMask.NameToLayer("Default");

        // Check if enough time has passed since the last shot
        if (Time.time < nextimetoFire)
            return;

        nextimetoFire = Time.time + 1f / firingrate;

        // Get the position and forward direction of the right controller
        Vector3 controllerPos = grabInteractable.attachTransform.position;
        Vector3 controllerForward = grabInteractable.attachTransform.forward;

        // Create a ray from the controller position in the forward direction
        Ray ray = new Ray(controllerPos, controllerForward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // Check if the hit object has the "Target" tag
            if (hit.collider.CompareTag("PillarTarget"))
            {
                // Add code here to play a sound effect or particle effect for hitting the target
                hitSound.Play();
                Destroy(hit.collider.gameObject, 0f);
                scoreManager.UpdateScore(10);
            }

            if (hit.collider.CompareTag("CubeTarget"))
            {
                // Add code here to play a sound effect or particle effect for hitting the target
                hitSound.Play();
                Destroy(hit.collider.gameObject, 0f);
                scoreManager.UpdateScore(15);
            }

            if (hit.collider.CompareTag("SphereTarget"))
            {
                // Add code here to play a sound effect or particle effect for hitting the target
                hitSound.Play();
                Destroy(hit.collider.gameObject, 0f);
                scoreManager.UpdateScore(20);
            }

            if (hit.collider.CompareTag("CylinderTarget"))
            {
                // Add code here to play a sound effect or particle effect for hitting the target
                hitSound.Play();
                Destroy(hit.collider.gameObject, 0f);
                scoreManager.UpdateScore(15);
            }

            // Apply an impact force to the hit object
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}

