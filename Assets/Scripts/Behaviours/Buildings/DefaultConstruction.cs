using GameData;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Buildings
{
    internal class DefaultConstruction: AbstractConstruction
    {
        public BuildingShopItem SO;

        public PreloaderSO _Preloader;
        public GameObject _AllowGrid;
        public GameObject _LockedGrid;
        public GameObject _MainSprite;

        PlayerControl _playerControl;
        GameObject _BVCamera;

        GameObject gameObject;

        public DefaultConstruction(
            BuildingShopItem so,
            PreloaderSO Preloader,
            GameObject AllowGrid,
            GameObject LockedGrid,
            GameObject MainSprite,
            PlayerControl playerControl,
            GameObject BVCamera,
            GameObject constructionObject) 
        {
            SO = so;
            _Preloader= Preloader;
           _AllowGrid= AllowGrid;
           _LockedGrid=LockedGrid;
           _MainSprite= MainSprite;
           _playerControl= playerControl;
           _BVCamera= BVCamera;
           gameObject= constructionObject;
        }


        public override Vector3 GetPos()
        {
            GameObject player = GameObject.Find("Player");
            Vector3 pos = player.GetComponent<Player>().GetCurrentTile();
            return pos;
        }

        public override bool BuildAllow(Vector3 pos)
        {
            bool res = true;

            var tiles = WorldData
                .Locations
                .Find(l => l.Current)
                .Tiles;
            var buildTiles = tiles.Where(t =>
                    t.LocalX >= pos.x - SO.SizeXZ &&
                    t.LocalX <= pos.x + SO.SizeXZ &&
                    t.LocalZ >= pos.z - SO.SizeXZ &&
                    t.LocalZ <= pos.z + SO.SizeXZ)
                .ToList();
            buildTiles.ForEach(t =>
                {
                    if (res) res = t.Contains == null;
                });

            return res;
        }

        public override void Install()
        {
            Vector3 pos = gameObject.transform.position;

            if (!BuildAllow(pos)) return;

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

            //проверка на needly
            //if (!container.totalBuildingList.BuildList.Contains(SO.NeedlyBuilding)) return;
            container.ShowNewBuild(mainTile, SO);


            WorldData.Settlements.Find(s => s.Home).WriteOffFromStock(SO.BuildMaterials);

            LocationEventManager.PlaceConstruction(tilesGrid);
            LocationEventManager.ChangeGameMode(GameMode.DefaultView);

            var buildingData = BuildingData.Create(SO.Name, SO.StartHP, mainTile, WorldData.Settlements.Find(s => s.Home));
            WorldData.AddBuilding(buildingData);

        }

        public override void MoveTo(Vector3 offset)
        {
            gameObject.transform.position += offset;
            Vector3 pos = gameObject.transform.position;

            _BVCamera.transform.position = new Vector3(pos.x, 7f, pos.z - 6);

            bool state = BuildAllow(pos);

            _AllowGrid.SetActive(state);
            _LockedGrid.SetActive(!state);
        }

        public override void Show(BuildingShopItem building)
        {
            throw new NotImplementedException();
        }


    }
}
