using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;


        [SerializeField]
        public int index;

        [field: SerializeField]
        public string Name { get; set; }


        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }


        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        public virtual bool Use() { return true; }
    }
}