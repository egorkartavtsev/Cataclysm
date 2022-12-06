using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Preloader", menuName = "ScriptableObjects/World Data/Preloader", order = 100)]
public class PreloaderSO : ScriptableObject
{
    [Header("Backgrounds")]
    public GameObject GroundPrefab;
    public GameObject SandPrefab;
    public GameObject WaterPrefab;

    [Header("LocationSprites")]
    public Sprite LockedCell;
    public Sprite AllowCell;

    [Header("LocationObjects")]
    public GameObject ResourcePrefab;
    public GameObject LootObjectPrefab;
    public GameObject BuildingPrefab;
    public GameObject Construction;

    [Header("Labels")]
    public GameObject DamageInfoLabel;

    [Header("UI Icons")]
    public Sprite LockedIcon;

    [Header("SupportItems")]
    public GameObject LockedIndicator;
}
