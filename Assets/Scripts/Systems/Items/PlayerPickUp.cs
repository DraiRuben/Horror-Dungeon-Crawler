using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using JetBrains.Annotations;

public class PlayerPickUp : MonoBehaviour
{
    public GameObject character;

    private void OnMouseDown()
    {
        Destroy(gameObject);
        character.gameObject.SetActive(true);
    }
}
