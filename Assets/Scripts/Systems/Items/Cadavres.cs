using UnityEngine;
using UnityEngine.SceneManagement;

public class Cadavres : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    private bool isQuitting;
    private void OnDestroy()
    {
        if (!isQuitting)
        {
            ParticleSystem particleInstance = Instantiate(particle, transform.position, transform.rotation);
            Destroy(particleInstance.gameObject, 2.0f);
        }

    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
