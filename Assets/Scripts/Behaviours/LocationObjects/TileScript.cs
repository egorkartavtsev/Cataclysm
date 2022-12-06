using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GameData;

public class TileScript : MonoBehaviour
{
    public GameObject LockedCell;
    public Tile tile;
    bool Locked;

    private void Start()
    {
        LocationEventManager.ConstructionPlaced += CheckStatusAfterBuild;
        bool locked = tile.Contains != null;
        Locked = locked;

        if(locked)
            SetLockedStatus(locked);
    }

    public void CheckStatusAfterBuild(List<Tile> tochedTiles)
    {
        if (!Locked)
        { 
            bool status = (tochedTiles.IndexOf(tile) >= 0);
            SetLockedStatus(status);
        }
    }

    public void SetLockedStatus(bool status)
    { 
        LockedCell.SetActive(status);
    }

    private void OnDestroy()
    {
        LocationEventManager.ConstructionPlaced -= CheckStatusAfterBuild;
    }
}
