using Inventory.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Camera cam; 
    public UIInventoryItem item;

    public void Awake()
    {
        cam = Camera.main;
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