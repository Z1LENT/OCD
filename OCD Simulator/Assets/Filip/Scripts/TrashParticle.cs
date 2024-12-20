using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashParticle : MonoBehaviour
{
    public ParticleSystem p1, p2, p3, p4, p5;
    private ParticleSystem[] particles;

    private void Start()
    {
        particles = new ParticleSystem[] { p1, p2, p3, p4, p5 };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GrabbableOBJ")
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
        }
        else
        {
            Debug.LogWarning("No particles available to play.");
        }
    }
}
