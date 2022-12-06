using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class SaveWorldData
    {
        public byte[] WorldMapTexture { get; set; }
        public List<LocationData> Locations { get; set; }
        public List<Character> Characters { get; set; }
        public List<SettlementData> Settlements { get; set; }


        public SaveWorldData() { }
        public SaveWorldData(byte[] texture, List<LocationData> locations, List<Character> characters, List<SettlementData> settlements)
        {
            WorldMapTexture = texture;
            Locations = locations;
            Characters = characters;
            Settlements = settlements;
        }
    }
}
