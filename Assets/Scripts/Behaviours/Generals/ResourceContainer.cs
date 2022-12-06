using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameData;
using Models;
using Abstractions;
using Helpers;

public class ResourceContainer : MonoBehaviour, IEventMessagesContainer
{
    public WorldResourceShop worldResourceShop;
	public PreloaderSO Preloader;
	
	LocationData locationData;

	// Start is called before the first frame update
	void Start()
    {
        locationData = WorldData.Locations.Find(l => l.Current);
		if (!locationData.Visited)
		{ 
			AllocateResources();
            locationData.SetVisited();
            WorldData.SaveWorldData();
        }

		foreach(Tile tile in locationData.Tiles.Where<Tile>(t => t.Contains != null && t.Contains.Health > 0 && t.Contains.ObjectType == LocationObjectType.Resource))
		{
			WRSItem wRSItem = worldResourceShop.Shop.Find(res => res.SO.Name == tile.Contains.Name);
			GameObject resObj = GameObject
									.Instantiate(Preloader.ResourcePrefab, 
												new Vector3(tile.LocalX, 0.55f, tile.LocalZ), 
												Quaternion.identity, gameObject.transform);

			resObj.GetComponent<ResourceObject>().LocatedIn = tile;

			resObj.name = $"{tile.Contains.Name} {tile.LocalX}-{tile.LocalZ}";
			resObj
				.GetComponent<ILootProducer>()
				.ProducedLoot = wRSItem.SO;
			resObj
				.transform
				.Find("MainSprite")
				.GetComponent<SpriteRenderer>()
				.sprite = wRSItem.SO.MainSprite;
			resObj
				.transform
				.Find("MainSprite")
				.GetComponent<SpriteRenderer>()
				.transform
				.position += wRSItem.SO.MainSpriteOffset;
		}
	}

    void AllocateResources()
    {
		System.Random rand = new System.Random();
		int objCount = 0;
		int countPerObj = 0;

		foreach (LocalResItem lri in locationData.LocalResShop)
		{
			WRSItem wRSItem = worldResourceShop.Shop.Find(res => res.SO.Name == lri.Name);
            try
            {
                objCount = Mathf.CeilToInt(lri.AvailableCount / wRSItem.SO.CountPerUnit);
				countPerObj = wRSItem.SO.CountPerUnit;
			}
            catch
            {
                objCount = 1;
				countPerObj = lri.AvailableCount;
            }

			int tmpCnt = locationData
				.Tiles
				.Where<Tile>(t => t.Contains == null && t.Background == BackgroundType.Ground)
				.ToList().Count;

            for (int i = 0; i < objCount; i++)
            {
				locationData
					.Tiles
                    .Where<Tile>(t => t.Contains == null && t.Background == BackgroundType.Ground)
                    .ToList()[rand.Next(0, tmpCnt - 1 - i)]
                    .Contains = new LocationObject()
						{
							Name = wRSItem.SO.Name,
							Health = countPerObj,
							ObjectType = LocationObjectType.Resource
						};
            }
        }
	}

	public void ShowDamageAnimation(int damage, Transform target)
	{
		GameObject dmgLabel = GameObject.Instantiate(Preloader.DamageInfoLabel, gameObject.transform);
		dmgLabel.transform.position = target.position;
		dmgLabel.GetComponent<TextMesh>().text = $"-{damage}";

		StartCoroutine(EventsAnimationUtil.AnimateDamage(this, dmgLabel));
	}
	public void RemoveDmgLabel(GameObject label)
	{
		Destroy(label);
	}
}
