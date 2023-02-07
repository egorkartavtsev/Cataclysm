using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class WeaponItem
    {
        public ResourceSO WeaponSO;
        public WeaponType weaponType;
        public Sprite ShellSprite;
        public int BaseDmg;
        public List<AttackType> AttackTypes;
    }

    [Serializable]
    public class AttackType
    {
        public float Angle;
        public float Distance;
        public float DmgMult;
    }
}
