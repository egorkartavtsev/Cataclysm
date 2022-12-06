using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GameData;

public class BuildingDetails : MonoBehaviour
{
    public WorldResourceShop ResShop;
    public Image image;
    public Text bName;
    public Text description;
    public GridLayoutGroup resources;
    public GameObject ResSlotPreset;
    public Button btnBuild;

    bool _allowBuild;

    public void FillInfo(BuildingShopItem so)
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);

        GameObject resList = resources.gameObject;

        for (int i = 0; i < resList.transform.childCount; i++)
            Destroy(resList.transform.GetChild(i).gameObject);

        image.sprite = so.Icon;
        bName.text = so.Name;
        description.text = so.Description;
        _allowBuild = true;

        so.BuildMaterials.ForEach(bm =>
            {
                LocalResItem stockRes = WorldData.Settlements.Find(s => s.Home).Stock.Find(sr => sr.Name == bm.Name);
                int existsCount = (stockRes != null) ? stockRes.AvailableCount : 0;
                bool exists = bm.NeedlyCount <= existsCount;
                ResourceSO wrsItem = ResShop.Shop.Find(r => r.SO.Name == bm.Name).SO;

                GameObject slot = GameObject.Instantiate(ResSlotPreset, resList.transform.position, resList.transform.rotation, resList.transform);
                slot.name = bm.Name;
                slot.GetComponent<NeedlyResourcePanel>().ShowRes(wrsItem.ShopItemIcon, bm.NeedlyCount, existsCount, exists);
                
                if(_allowBuild) _allowBuild = exists;
            }
        );

        btnBuild.interactable = _allowBuild;
        btnBuild.gameObject.GetComponent<BtnBuild>().SO = so;
    }
}
