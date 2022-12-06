using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using Models;

namespace GameData
{
    public static class WorldData
    {
        public static byte[] WorldMapTexture { get; set; }

        public static List<LocationData> Locations { get; set; }
        public static List<SettlementData> Settlements { get; set; }
        public static List<Character> Characters { get; set; }

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
        }
    }
}
