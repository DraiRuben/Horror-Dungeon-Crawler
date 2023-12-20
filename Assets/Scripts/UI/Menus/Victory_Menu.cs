using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory_Menu : MonoBehaviour
{
    public GameObject victory_Menu;

    public void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.Victory_Menu);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        victory_Menu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
