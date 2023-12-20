using UnityEngine;
using UnityEngine.SceneManagement;

public class Defeat_Menu : MonoBehaviour
{
    public GameObject defeat_Menu;

    private AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Start()
    {
        audioManager.PlayMusic(audioManager.Defeat_Menu);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        defeat_Menu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
