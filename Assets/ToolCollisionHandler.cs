using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCollisionHandler : MonoBehaviour
{
    // public ParticleSystem particles;
    public ParticleSystem sparks;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        ContactPoint contactPoint = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
        Vector3 position = contactPoint.point;
        // Debug.Log("The tool hit the weapon at " + position);
        sparks.transform.position = position;
        sparks.transform.rotation = rotation;

        sparks.Stop();
        sparks.Play();

        //+ TODO communicate to system that a hit has been made
    }
}
