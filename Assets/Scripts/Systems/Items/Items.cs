using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public AudioClip UseSFX;


        [field: SerializeField]
        public bool IsStackable { get; set; }

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public int Quantity { get; set; } = 1;

        public int index;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        public virtual bool Use()
        {
            AudioManager.Instance.PlaySFX(UseSFX);
            return true;
        }
    }
}