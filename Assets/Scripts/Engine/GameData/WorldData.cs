using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Models;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;

namespace GameData
{
    public static class WorldData
    {
        public static byte[] WorldMapTexture { get; set; }

        public static List<LocationData> Locations { get; set; }
        public static List<SettlementData> Settlements { get; set; }
        public static List<Character> Characters { get; set; }


        private static IList<BuildingData> _Buildings = new List<BuildingData>();
        public static IReadOnlyList<BuildingData> Buildings { get
            {
                return (IReadOnlyList<BuildingData>)_Buildings.AsReadOnlyList();
            }}

        public static void AddCampfire(BuildingData buildingData)
        {
            buildingData.Tiles = new List<Tile>();
            buildingData.Tiles.Add(buildingData.MainTile);
            _Buildings.Add(buildingData);
        }
        public static void AddBuilding(BuildingData buildingData)
        {
            if (buildingData == null) return;
            _Buildings.Add(buildingData);
        }

        private static IList<Tile> GetBuildingTiles(BuildingData building)
        {
            if(building.Tiles!=null) return building.Tiles;
            building.Tiles = Locations.Find(l => l.Current).Tiles
                .Where(t =>
                t.Contains?.Name == building.Name
                ).ToList();
            return building.Tiles;
            /*return Locations.Find(l => l.Current).Tiles
                .Where<Tile>(t =>
                t.Contains.Name==building.Name
                ).ToList();*/
        }
        public static void RemoveBuilding(string buildingName)
        {
            if (buildingName.Equals(string.Empty)) return;
            var building = _Buildings.Where(b=>b.Name==buildingName).FirstOrDefault();
            GetBuildingTiles(building).ToList().ForEach(tile =>
            {
                tile.Contains = null;
            });
            _Buildings.Remove(building);
        }


        public static void AddLocation(LocationData locationData)
        {
            if (Locations == null)
                Locations = new List<LocationData>();

            Locations.Add(locationData);
        }

        public static void SaveWorldData()
        {
            SaveWorldData data = new SaveWorldData(WorldMapTexture, Locations, Characters, Settlements);
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(Application.persistentDataPath + "/MySaveData.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, data);
            }
        }

        public static void LoadWorldData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            SaveWorldData data = null;
            using (FileStream fs = new FileStream(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open, FileAccess.Read))
            {
                data = (SaveWorldData)bf.Deserialize(fs);
            }
            WorldMapTexture = data.WorldMapTexture;
            Locations = data.Locations;
            Characters = data.Characters;
            Settlements = data.Settlements;

            var tiles = WorldData.Locations.Find(l => l.Current).Tiles
                .Where(t => t.Contains != null && t.Contains.ObjectType == LocationObjectType.Building)
                .ToList();
            _Buildings = tiles
                .Where((tile)=>tile.Contains.MainObjectTile)
                .Select((tile) => 
                    BuildingData.Create(tile.Contains.Name, tile.Contains.Health, tile, Settlements.Where(s=>s.Home).First() )
                ).ToList();
        }
    }
}
