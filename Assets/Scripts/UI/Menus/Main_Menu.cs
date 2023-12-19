using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{
    private AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Start()
    {
        audioManager.PlayMusic(audioManager.main_Menu);
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
