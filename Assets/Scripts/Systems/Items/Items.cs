using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public AudioManager m_audioManager;
        public AudioClip UseSFX;

        private void Start()
        {
            m_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        [field: SerializeField]
        public bool IsStackable { get; set; }

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public int Quantity { get; set; } = 1; 

        [SerializeField]
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
            m_audioManager.PlaySFX(UseSFX);
            return true;
        }
    }
}