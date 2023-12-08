using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Info")]
    public Sprite ItemIcon;
    public string ItemName;
    public GameObject prefab;
    public ParticleSystem particle;
}
