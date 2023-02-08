using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "new Weapon Shop", menuName = "ScriptableObjects/World Data/Weapon Shop", order = 50)]
public class WeaponShop : ScriptableObject
{
    public List<WeaponItem> Shop;
}
