using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData;

namespace Models
{
    [Serializable]
    public class SettlementData
    {
        public LocationData Location { get; set; }
        public bool Home { get; set; }
        public List<LocalResItem> Stock { get; set; }
        public List<Character> Settlers 
        { 
            get 
            { 
                return WorldData.Characters.Where<Character>(c => c.Settlement == this).ToList(); 
            } 
        }

        public SettlementData()
        { }

        public SettlementData(LocationData location, bool home)
        {
            Location = location;
            Home = home;
            Stock = new List<LocalResItem>();
        }

        public void AddToStock(LocalResItem res)
        {
            LocalResItem stockRes = Stock.Find(r => r.Name == res.Name);
            if (stockRes == null)
                Stock.Add(res);
            else
                Stock.Find(r => r.Name == res.Name).AvailableCount += res.AvailableCount;
        }

        public void WriteOffFromStock(List<NeedlyBuildMaterials> reses)
        {
            reses.ForEach(r => Stock.Find(sr => sr.Name == r.Name).AvailableCount -= r.NeedlyCount);
        }
    }
}
