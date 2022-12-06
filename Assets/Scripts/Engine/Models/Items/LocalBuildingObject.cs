using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class LocalBuildingObject
    {
        public string Name { get; set; }
        public int PosX { get; set; }
        public int PosZ { get; set; }
        public int HP { get; set; }
        public SettlementData SettlementOwner { get; set; }
    }
}
