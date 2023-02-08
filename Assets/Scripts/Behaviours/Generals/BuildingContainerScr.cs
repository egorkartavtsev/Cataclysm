using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
public class BuildingContainerScr : MonoBehaviour
{
    public LocationController locationController;
    public TotalBuildingList totalBuildingList;
    public PreloaderSO Preloader;

    // Start is called before the first frame update
    void Start()
    {
        locationController
            .CurrentLocation
            .Tiles
            .Where(t => t.Contains != null && t.Contains.ObjectType == LocationObjectType.Building && t.Contains.MainObjectTile)
            .ToList()
            .ForEach(t =>
                {
                    BuildingShopItem bsi = totalBuildingList.BuildList.Find(i => i.Name == t.Contains.Name);
                    ShowNewBuild(t, bsi);
                }
        );
    }

    public void ShowNewBuild(Tile t, BuildingShopItem bsi)
    {
        GameObject newBld = GameObject.Instantiate(Preloader.BuildingPrefab, new Vector3(t.LocalX, 0.5f, t.LocalZ), Quaternion.identity, gameObject.transform);
        newBld.name = t.Contains.Name;
        newBld.GetComponent<BuildingScript>().SO = bsi;
        //newBld.GetComponent<BuildingScript>().id = [GUID из WorldData];

        GameObject sprite = newBld.transform.Find("Sprite").gameObject;
        SpriteRenderer s = sprite.gameObject.GetComponent<SpriteRenderer>();
        s.sprite = bsi.Icon;

        float width = bsi.SizeXZ * 2f + 1f;
        s.size = new Vector2(width, bsi.Height);
        sprite.transform.localPosition = new Vector3(0f, bsi.YOffset, bsi.ZOffset);
    }
}
