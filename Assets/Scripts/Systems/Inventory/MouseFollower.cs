using Inventory.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    public UIInventoryItem item;
    public static MouseFollower Instance;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}