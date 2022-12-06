using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Tile
    {
        public int GlobalId;
        public int X;
        public int Z;
        public int LocalX;
        public int LocalZ;
        public BackgroundType Background;
        public bool Location;
        public LocationObject Contains;
    }
}
