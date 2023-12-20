using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue_Text : MonoBehaviour
{
    private WaitForSeconds TextDelay = new WaitForSeconds(4.0f);
    [SerializeField] List<Animator> m_sentences = new();

    void Start()
    {
         StartCoroutine(TextApparition());
    }

    private IEnumerator TextApparition()
    {
        foreach (var m in m_sentences)
        {
            m.SetTrigger("Appear");
            yield return TextDelay;
        }
        Play();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
