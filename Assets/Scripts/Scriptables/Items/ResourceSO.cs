using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "new Resource", menuName = "ScriptableObjects/Interactables/Resource", order = 50)]
public class ResourceSO : ScriptableObject
{
    public string Name;
    public int CountPerUnit;

    [Header("MainSprite")]
    public Sprite MainSprite;
    public Vector3 MainSpriteOffset;
    public Sprite MenuSprite;

    [Header("LootSprites")]
    public Sprite LootSprite; 
    public Sprite ShopItemIcon;
    public Sprite ShopItemIconMini;

    [Header("ResItem Setting")]
    public ResItemType ItemType;
    public ResRarityType RarityType;

    [Header("LootSetting")]
    public int InventoryStackSize;
}
