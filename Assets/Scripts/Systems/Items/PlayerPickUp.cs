using UnityEngine;


public class PlayerPickUp : MonoBehaviour
{
    public GameObject character;

    private void OnMouseDown()
    {
        Destroy(gameObject);
        character.gameObject.SetActive(true);
    }
}
