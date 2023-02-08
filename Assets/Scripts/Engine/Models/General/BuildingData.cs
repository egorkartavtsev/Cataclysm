using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class BuildingData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public Tile MainTile { get; set; }
        public SettlementData Settlement { get; set; }
    }
}
