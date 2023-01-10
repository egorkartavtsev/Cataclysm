using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models;
using GameData;
using Unity.VisualScripting;


public class LocationController : MonoBehaviour
{
    [Header("General")]
    public LocationData CurrentLocation;
    public GameObject BuildingContainer;
    public Player player;
    public TotalBuildingList totalBuildingList;

    [Header("GameState")]
    public bool InMenu;
    public GameMode gameMode;
    public SceneType sceneType;

    [Header("Cameras")]
    public GameObject BuildView;
    public GameObject DefaultView;


    void Start()
    {
        switch (sceneType)
        {
            case SceneType.TestPlayer:
                CurrentLocation = new LocationData();
                break;
            default:
                CurrentLocation = WorldData.Locations.Find(l => l.Current);
                break;
        }
        LocationEventManager.GameModeChanged += ToggleGameMode;
    }

    #region BonfireControl
    public void ToggleBonfire(bool state)
    { 
        CurrentLocation.HasBonfire = state;
        if(state)
            LocationEventManager.MakePlayerBonfire();
        else
            LocationEventManager.DestroyPlayerBonfire();
    }

    public void SetUpCampfire(GameObject prefab, Sprite sprite)
    {
        Vector3 pos = player.GetCurrentTile();

        GameObject bonfire = prefab;
        bonfire.name = "Campfire";
        bonfire.GetComponent<BuildingScript>().SO = totalBuildingList.BuildList.Find(b => b.Name == "Campfire");
        GameObject.Instantiate(bonfire, new Vector3(pos.x, 0.5f, pos.z), Quaternion.identity, BuildingContainer.transform);

        Tile t = WorldData.Locations
            .Find(l => l.Current)
            .Tiles
            .Find(t => t.LocalX == pos.x && t.LocalZ == pos.z);

        t.Contains = new LocationObject()
        {
            Name = "Campfire",
            Health = 100,
            ObjectType = LocationObjectType.Building,
            MainObjectTile = true
        };

        List<Tile> tiles = new List<Tile>();
        tiles.Add(t);
        LocationEventManager.PlaceConstruction(tiles);
    }
    #endregion

    public void ToggleGameMode(GameMode mode)
    {
        gameMode = mode;
        if (mode != GameMode.InMenu)
        { 
            BuildView.SetActive(mode == GameMode.BuildingView);
            DefaultView.SetActive(mode == GameMode.DefaultView);

            BuildView.transform.SetPositionAndRotation(DefaultView.transform.position, DefaultView.transform.rotation);
        }
    }
}
