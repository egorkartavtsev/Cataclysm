using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class LocationObject
    {
        public string Name;
        public int Health;
        public LocationObjectType ObjectType;
        public bool MainObjectTile;
    }
}
