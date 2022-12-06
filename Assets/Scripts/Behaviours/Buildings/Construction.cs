using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using Models;
using System.Security.Cryptography;

public class Construction : MonoBehaviour
{
    public PreloaderSO Preloader;
    public GameObject AllowGrid;
    public GameObject LockedGrid;
    public GameObject MainSprite;

    PlayerControl playerControl;
    GameObject BVCamera;

    public BuildingShopItem SO;

    private void Start()
    {
        BVCamera = GameObject.Find("BuildingView");
        playerControl = GameObject.Find("PlayerController").GetComponent<PlayerControl>();
        playerControl.SetCurrentConstruction(this);

        LocationEventManager.GameModeChanged += Cancel;
        GameObject player = GameObject.Find("Player");
        Vector3 pos = player.GetComponent<Player>().GetCurrentTile();
        gameObject.transform.position = new Vector3(pos.x, 0f, pos.z);
    }

    public void Show(BuildingShopItem building)
    {
        SO = building;

        SpriteRenderer s = MainSprite.GetComponent<SpriteRenderer>();
        s.sprite = SO.Icon;

        float width = SO.SizeXZ * 2f + 1f;
        s.size = new Vector2(width, SO.Height);
        MainSprite.transform.position = new Vector3(0f, SO.YOffset, SO.ZOffset);

        DrawGrid();
    }

    public void MoveTo(Vector3 offset)
    {
        gameObject.transform.position += offset;
        Vector3 pos = gameObject.transform.position;

        BVCamera.transform.position = new Vector3(pos.x, 7f, pos.z - 6);

        bool state = BuildAllow(pos);

        AllowGrid.SetActive(state);
        LockedGrid.SetActive(!state);
    }

    public void Install()
    {
        Vector3 pos = gameObject.transform.position;
        Tile mainTile = new Tile();

        List<Tile> tilesGrid = WorldData
            .Locations.Find(l => l.Current).Tiles.Where<Tile>(t =>
                t.LocalX >= pos.x - SO.SizeXZ &&
                t.LocalX <= pos.x + SO.SizeXZ &&
                t.LocalZ >= pos.z - SO.SizeXZ &&
                t.LocalZ <= pos.z + SO.SizeXZ)
            .ToList();

        tilesGrid.ForEach(t =>
            {
                bool main = t.LocalX == pos.x && t.LocalZ == pos.z;

                t.Contains = new LocationObject()
                {
                    Name = SO.Name,
                    Health = SO.StartHP,
                    ObjectType = LocationObjectType.Building,
                    MainObjectTile = main
                };

                if (main) mainTile = t;
            }
        );

        GameObject container = GameObject.Find("BuildingContainer");
        container.GetComponent<BuildingContainerScr>().ShowNewBuild(mainTile, SO);

        WorldData.Settlements.Find(s => s.Home).WriteOffFromStock(SO.BuildMaterials);

        LocationEventManager.PlaceConstruction(tilesGrid);
        LocationEventManager.ChangeGameMode(GameMode.DefaultView);
        Cancel(GameMode.DefaultView);
    }

    void DrawGrid()
    {
        for (int z = 0 - SO.SizeXZ; z <= SO.SizeXZ; z++)
        {
            for (int x = 0 - SO.SizeXZ; x <= SO.SizeXZ; x++)
            {
                GameObject allowCell = GameObject.Instantiate(Preloader.LockedIndicator, AllowGrid.transform);
                GameObject lockedCell = GameObject.Instantiate(Preloader.LockedIndicator, LockedGrid.transform);

                Vector3 pos = new Vector3(x, 0.1f, z);
                allowCell.transform.position = lockedCell.transform.position = pos;

                allowCell.GetComponent<SpriteRenderer>().sprite = Preloader.AllowCell;
                lockedCell.GetComponent<SpriteRenderer>().sprite = Preloader.LockedCell;
            }
        }
        AllowGrid.SetActive(false);
        LockedGrid.SetActive(true);
    }

    bool BuildAllow(Vector3 pos)
    {
        bool res = true;

        WorldData
            .Locations
            .Find(l => l.Current)
            .Tiles.Where<Tile>(t =>
                t.LocalX >= pos.x - SO.SizeXZ &&
                t.LocalX <= pos.x + SO.SizeXZ &&
                t.LocalZ >= pos.z - SO.SizeXZ &&
                t.LocalZ <= pos.z + SO.SizeXZ)
            .ToList()
            .ForEach(t =>
            {
                if (res) res = t.Contains == null;
            });

        return res;
    }

    private void Cancel(GameMode mode)
    {
        Destroy(gameObject);
        playerControl.SetCurrentConstruction();
    }

    private void OnDestroy()
    {
        LocationEventManager.GameModeChanged -= Cancel;
    }
}
