using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameData;
using Models;
using Unity.VisualScripting;
using Assets.Scripts.Engine.Helpers;

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
        CheckNextLevel();

        if (!_allowed)
            SetDisabled();
    }

    private void CheckUnique()
    {
        if (_allowed && BuildingSO.Unique)
        {
            var building = WorldData.Buildings.Where(b => b.Name == BuildingSO.Name).FirstOrDefault();
            _allowed = building == null;
        }
    }

    private void CheckNeedly()
    {
        if (_allowed && BuildingSO.NeedlyBuilding != null)
        {
            var building = WorldData.Buildings.Where(b => b.Name == BuildingSO.NeedlyBuilding.Name).First();
            _allowed = building != null;
        }
    }

    private void CheckCallerLevel()
    {
        if (_allowed && BuildingSO.MinimalBuilderLevel > parent.Caller.Level)
            _allowed = false;
    }

    private void CheckNextLevel()
    {
        if(_allowed && BuildingSO.NextLevelFor!=null && WorldData.Buildings.ToList().Where(b => b.Name == BuildingSO.NextLevelFor.Name).Count() == 0)
            _allowed = false;
    }

    public void SetDisabled()
    {
        gameObject.GetComponent<Button>().interactable = false;
        var image = gameObject.GetComponent<Image>();

        var newTex = TextureFunctions.Resize(image.sprite.texture,512,512);
        TextureFunctions.AddWatermark(newTex, Preloader.LockedIcon.texture);

        var nsprite = Sprite.Create(newTex, new Rect(0f,0f,newTex.width,newTex.height), Vector2.zero);
        image.sprite = nsprite;
    }

    public void ShowDetails()
    {
        DetailsPanel.FillInfo(BuildingSO);
    }

}
