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
    internal class NextLevelConstruction : AbstractConstruction
    {

        GameObject gameObject;
        public BuildingShopItem SO;
        public GameObject _BVCamera;

        public NextLevelConstruction(GameObject _gameObject, BuildingShopItem so, GameObject BVCamera)
        {
            gameObject = _gameObject;
            SO = so;
            _BVCamera = BVCamera;
        }

        public override Vector3 GetPos()
        {
            var lastBuild = WorldData.Buildings.Where(b => b.Name == SO.NextLevelFor.Name).First();
            var pos = new Vector3(lastBuild.MainTile.LocalX, 0, lastBuild.MainTile.LocalZ);
            _BVCamera.transform.position = new Vector3(pos.x, 7f, pos.z - 6);
            return pos;
        }

        public override bool Install()
        {
            WorldData.RemoveBuilding(SO.NextLevelFor.Name);
            
            var container = GameObject.Find("BuildingContainer").GetComponent<BuildingContainerScr>();
            container.HideOldBuild(SO.NextLevelFor);

            return true;
        }

        public override void MoveTo(Vector3 offset)
        {
            return;
        }
        public override bool BuildAllow(Vector3 pos)
        {
            return true;
        }

        public override void Show(BuildingShopItem building)
        {
            throw new NotImplementedException();
        }
    }
}
