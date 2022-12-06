using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Models;

[CreateAssetMenu(fileName = "new WRS", menuName = "ScriptableObjects/World Data/Resource Shop", order = 50)]
public class WorldResourceShop : ScriptableObject
{
    public List<WRSItem> Shop;

    public ResourceSO GetSO(LocalResItem item)
    {
        return Shop.Find(si => si.SO.Name == item.Name).SO;
    }
}
