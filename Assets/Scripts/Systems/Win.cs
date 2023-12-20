using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    private void OnTriggerStay(Collider other)
    {
        winScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
