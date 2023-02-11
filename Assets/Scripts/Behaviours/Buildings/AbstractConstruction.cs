using GameData;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Buildings
{
    internal abstract class AbstractConstruction
    {
        public PreloaderSO Preloader;
        public GameObject AllowGrid;
        public GameObject LockedGrid;
        public GameObject MainSprite;

        PlayerControl playerControl;
        GameObject BVCamera;

        public BuildingShopItem SO;

        public abstract Vector3 GetPos();
        public abstract void Show(BuildingShopItem building);
       
        public abstract void MoveTo(Vector3 offset);
       
        public abstract void Install();
      
        public abstract bool BuildAllow(Vector3 pos);

    }

}

