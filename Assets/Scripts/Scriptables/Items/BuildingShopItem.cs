using System;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

using Models;

[Serializable]
[CreateAssetMenu(fileName = "new Building", menuName = "ScriptableObjects/Interactables/Building")]
public class BuildingShopItem : ScriptableObject
{
    [Header("Main Data")]
    public string Name;
    public int StartHP;
    public int Level;
    public Sprite Icon;
    public string Description;
    public bool Unique;
    public bool IsStock;
    public List<LocalResItem> Production;

    [Header("Sizes")]
    public int SizeXZ;
    public int Height;
    public float YOffset;
    public float ZOffset;

    [Header("Links")]
    public GameObject OpenedMenu;
    public BuildingShopItem NeedlyBuilding;
    public BuildingShopItem NextLevelFor;
    public int MinimalBuilderLevel;

    [Header("Building Materials")]
    public List<NeedlyBuildMaterials> BuildMaterials;
}

[Serializable]
public struct NeedlyBuildMaterials
{
    public string Name;
    public int NeedlyCount;
    public int CurrentCount;
}
