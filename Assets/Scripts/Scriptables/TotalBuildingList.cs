using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new BuildingList", menuName = "ScriptableObjects/World Data/BuildingList", order = 51)]
public class TotalBuildingList : ScriptableObject
{
    public List<BuildingShopItem> BuildList;
}