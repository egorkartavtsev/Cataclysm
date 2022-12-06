using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using Abstractions;


public class BuildingShopPanel : MonoBehaviour, IOpenedMenu
{
    public GameObject BtnDropInv;
    public Text CallerName;
    public BuildingShopItem Caller { get; set; }
    LocationController _lc;


    private void Start()
    {
        LocationEventManager.CloseButtonPress += CloseWindow;
        _lc = GameObject.Find("LocationController").GetComponent<LocationController>();
        _lc.ToggleGameMode(GameMode.InMenu);
        BtnDropInv.SetActive(Caller.IsStock);
        CallerName.text = Caller.Name;
    }

    private void CloseWindow()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LocationEventManager.CloseButtonPress -= CloseWindow;
        if(_lc.gameMode != GameMode.BuildingView)
            _lc.ToggleGameMode(GameMode.DefaultView);
    }

    public void DropInventory()
    {
        WorldData.Characters[0].Bag.ForEach(ii =>
            {
                WorldData.Settlements.Find(s => s.Home).AddToStock(ii);
            }
        );

        WorldData.Characters[0].Bag.Clear();
    }
}
