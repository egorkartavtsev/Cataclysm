using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameData;
using Models;

public class LocationBuilder : MonoBehaviour
{
    LocationData CurrentLocation;
    public PreloaderSO Preloader;
    //[Range(0, 16)]
    //public int Index; //DEBUG! ÓÁÐÀÒÜ!!!

    private void Start()
    {
        System.Random rand = new System.Random();
        //TODO: DEBUG
        WorldData.LoadWorldData();
        CurrentLocation = WorldData.Locations.Find(l => l.Current);
        GameObject tile = Preloader.GroundPrefab;
        if (!CurrentLocation.Visited)
        {
            SetLocalCoordinates();
        }
        string tileName = string.Empty;
        foreach (Tile t in CurrentLocation.Tiles)
        {
            switch (t.Background)
            {
                case BackgroundType.Water:
                    tile = Preloader.WaterPrefab;
                    tileName = $"Water{t.LocalX}-{t.LocalZ}";
                    break;
                case BackgroundType.Sand:
                    tile = Preloader.SandPrefab;
                    tileName = $"Sand{t.LocalX}-{t.LocalZ}";
                    break;
                case BackgroundType.Ground:
                    tile = Preloader.GroundPrefab;
                    tileName = $"Ground{t.LocalX}-{t.LocalZ}";
                    break;
            }            
            GameObject newTile = GameObject.Instantiate(tile, new Vector3(t.LocalX, 0, t.LocalZ), Quaternion.identity, gameObject.transform);
            newTile.GetComponent<TileScript>().tile = t;
            newTile.name = tileName;
        }
    }

    private void OnDestroy()
    {
        WorldData.SaveWorldData();
    }

    void SetLocalCoordinates()
    {
        for (int z = 0; z < 151; z++)
        {
            for (int x = 0; x < 151; x++)
            {
                int index = z * 151 + x;
                CurrentLocation.Tiles[index].LocalX = x;
                CurrentLocation.Tiles[index].LocalZ = z;
            }
        }
    }
}
