using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashParticle : MonoBehaviour
{
    public ParticleSystem p1, p2, p3, p4, p5;
    private ParticleSystem[] particles;
    private bool canPlayParticle = true; // Timer flag

    private void Start()
    {
        particles = new ParticleSystem[] { p1, p2, p3, p4, p5 };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GrabbableOBJ" && canPlayParticle)
        {
            PlayParticle();
        }
    }

    public void PlayParticle()
    {
        if (particles.Length > 0)
        {
            // Choose a random particle system and play it
            int randomIndex = Random.Range(0, particles.Length);
            ParticleSystem chosenParticle = particles[randomIndex];
            chosenParticle.Play();
            StartCoroutine(ParticleCooldown()); // Start the cooldown timer
        }
        else
        {
            Debug.LogWarning("No particles available to play.");
        }
    }

    private IEnumerator ParticleCooldown()
    {
        canPlayParticle = false; // Disable further particle plays
        yield return new WaitForSeconds(1f); // Wait for 1 second
        canPlayParticle = true; // Re-enable particle plays
    }
}
