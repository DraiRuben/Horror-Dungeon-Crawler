using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory_Menu : MonoBehaviour
{
    public GameObject victory_Menu;

    private AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Start()
    {
        audioManager.PlayMusic(audioManager.victory_Menu);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        victory_Menu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
