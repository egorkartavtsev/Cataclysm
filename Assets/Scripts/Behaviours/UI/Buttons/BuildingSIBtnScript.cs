using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameData;
using Models;


public class BuildingSIBtnScript : MonoBehaviour
{
    public PreloaderSO Preloader;
    public BuildingShopItem BuildingSO;
    public BuildingDetails DetailsPanel;

    public BuildingShopPanel parent;

    private bool _allowed;

    private void Start()
    {
        parent = gameObject
            .transform
            .parent
            .parent
            .parent
            .parent
            .parent
            .parent
            .gameObject
            .GetComponent<BuildingShopPanel>();
        //parent = GameObject.Find("BuildingShop").gameObject.GetComponent<BuildingShopPanel>();

        _allowed = true;
        CheckCallerLevel();
        CheckUnique();
        CheckNeedly();

        if (!_allowed)
            SetDisabled();
    }

    private void CheckUnique()
    {
        if (_allowed && BuildingSO.Unique)
        {
            List<Tile> blds = WorldData
                .Locations
                .Find(l => l.Current)
                .Tiles
                .Where(t => t.Contains != null && t.Contains.ObjectType == LocationObjectType.Building)
                .ToList();

            Tile lbo = blds.Where(b => b.Contains.Name == BuildingSO.Name).FirstOrDefault();

            _allowed = lbo == null;
        }
    }

    private void CheckNeedly()
    {
        if (_allowed && BuildingSO.NeedlyBuilding != null)
        {
            List<Tile> blds = WorldData
                .Locations
                .Find(l => l.Current)
                .Tiles
                .Where(t => t.Contains != null && t.Contains.ObjectType == LocationObjectType.Building)
                .ToList();

            Tile lbo = blds.Where(b => b.Contains.Name == BuildingSO.NeedlyBuilding.Name).FirstOrDefault();

            _allowed = lbo != null;
        }
    }

    public void CheckCallerLevel()
    {
        if (_allowed && BuildingSO.MinimalBuilderLevel > parent.Caller.Level)
            _allowed = false;
    }

    public void SetDisabled()
    {
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.GetComponent<Image>().sprite = Preloader.LockedIcon;
    }

    public void ShowDetails()
    {
        DetailsPanel.FillInfo(BuildingSO);
    }
}
