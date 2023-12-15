using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadavres : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    private void OnDestroy()
    {
        ParticleSystem particleInstance = Instantiate(particle, transform.position, transform.rotation);
        Destroy(particleInstance, 5.0f);
    }
}
