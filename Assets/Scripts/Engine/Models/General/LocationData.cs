using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using GameData;

namespace Models
{
    [Serializable]
    public class LocationData
    {
        public string Name { get; private set; }
        public List<Tile> Tiles { get; private set; }
        public List<LocalResItem> LocalResShop { get; private set; }
        public SettlementData Settlement 
        { 
            get 
            { 
                return WorldData.Settlements.Find(s => s.Location == this); 
            } 
        }

        public bool Visited { get; private set; }
        public bool Current { get; private set; }
        public bool Explored { get; private set; }        
        public bool HasBonfire { get; set; }

        #region Generate
        public void SetName(string name)
        {
            Name = name;
        }

        public void AddTiles(List<Tile> tiles)
        {
            Tiles = new List<Tile>(tiles);

        }

        public void AddResItem(string name, int count)
        {
            if (LocalResShop == null)
                LocalResShop = new List<LocalResItem>();

            LocalResShop.Add(
                new LocalResItem()
                {
                    Name = name,
                    AvailableCount = count
                }
            );
        }

        public void SetSettlement(bool home)
        {
            WorldData.Settlements.Add(new SettlementData(this, home));
            Visited = false;
            Explored = Current = home;
        }
        #endregion

        public void SetVisited()
        {
            Visited = true;
        }

        public void Leave()
        {
            Current = false;
        }

        public void Enter()
        {
            WorldData
                .Locations
                .ForEach(l => l.Leave());

            Current = true;
        }
    }
}
