using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Character
    {
        public int SettlerId { get; set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public string Location { get; private set; }
        public SettlementData Settlement { get; private set; }
        public List<LocalResItem> Bag { get; set; }
        public int PosX { get; set; }
        public int PosZ { get; set; }

        public Character()
        { }

        public Character
        (
            int _settlerId,
            string _name,
            int _hp,
            string _location,
            SettlementData _settle
        )
        {
            SettlerId = _settlerId;
            Name = _name;
            HP = _hp;
            Location = _location;
            Settlement = _settle;
            PosX = 0;
            PosZ = 0;
        }

    }
}
