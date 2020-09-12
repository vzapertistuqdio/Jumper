using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundFire : MonoBehaviour
{
    [SerializeField] private ParticleSystem roundParticle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            roundParticle.Play();
        }
    }
}
