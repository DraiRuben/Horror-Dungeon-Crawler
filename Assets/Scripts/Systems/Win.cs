using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    private void OnTriggerEnter(Collider other)
    {
        winScreen.gameObject.SetActive(true);
    }
}
