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
            Vector3 pos = gameObject.transform.position;

            WorldData.RemoveBuilding(SO.NextLevelFor.Name);

            Tile mainTile = new Tile();

            List<Tile> tilesGrid = WorldData
                .Locations.Find(l => l.Current).Tiles.Where<Tile>(t =>
                    t.LocalX >= pos.x - SO.SizeXZ &&
                    t.LocalX <= pos.x + SO.SizeXZ &&
                    t.LocalZ >= pos.z - SO.SizeXZ &&
                    t.LocalZ <= pos.z + SO.SizeXZ)
                .ToList();

            tilesGrid.ForEach(t =>
            {
                bool main = t.LocalX == pos.x && t.LocalZ == pos.z;

                t.Contains = new LocationObject()
                {
                    Name = SO.Name,
                    Health = SO.StartHP,
                    ObjectType = LocationObjectType.Building,
                    MainObjectTile = main
                };

                if (main) mainTile = t;
            }
            );

            var container = GameObject.Find("BuildingContainer").GetComponent<BuildingContainerScr>();

            container.HideOldBuild(SO.NextLevelFor);
            container.ShowNewBuild(mainTile, SO);


            WorldData.Settlements.Find(s => s.Home).WriteOffFromStock(SO.BuildMaterials);

            LocationEventManager.PlaceConstruction(tilesGrid);
            LocationEventManager.ChangeGameMode(GameMode.DefaultView);

            var buildingData = BuildingData.Create(SO.Name, SO.StartHP, mainTile, WorldData.Settlements.Find(s => s.Home));
            WorldData.AddBuilding(buildingData);

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
