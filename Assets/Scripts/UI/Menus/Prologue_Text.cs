using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue_Text : MonoBehaviour
{
    private WaitForSeconds TextDelay = new WaitForSeconds(5.0f);
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
    }
}
