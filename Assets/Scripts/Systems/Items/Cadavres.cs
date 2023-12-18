using UnityEngine;

public class Cadavres : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    private void OnDestroy()
    {
        ParticleSystem particleInstance = Instantiate(particle, transform.position, transform.rotation);
        Destroy(particleInstance.gameObject, 2.0f);
    }
}
