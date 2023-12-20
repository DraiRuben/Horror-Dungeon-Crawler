using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{

    public void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.Main_Menu);
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
