using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private GameObject m_slashPrefab;
    [SerializeField] private GameObject m_bulletPrefab;

    private void PlayFX(FXType type)
    {
        switch (type)
        {
            case FXType.Slash:

                break;
        }
    }

    public enum FXType
    {
        Slash,
        Bullet
    }
}
