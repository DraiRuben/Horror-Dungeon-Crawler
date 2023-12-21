using UnityEngine;

public class Menu_Music : MonoBehaviour
{

    public void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.Main_Menu);
    }
}
