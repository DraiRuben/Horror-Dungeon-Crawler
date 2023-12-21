using UnityEngine;
using UnityEngine.SceneManagement;

public class Defeat_Menu : MonoBehaviour
{

    public void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.Defeat_Menu);
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
