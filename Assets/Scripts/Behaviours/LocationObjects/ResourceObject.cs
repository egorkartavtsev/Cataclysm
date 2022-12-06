using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models;
using Actions;
using Abstractions;

public class ResourceObject : MonoBehaviour, IInteractive, ILootProducer
{
    public IAction Action { get; set; }
    public Tile LocatedIn { get; set; }
    public ResourceSO ProducedLoot { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Action = new GatherResource();
        Action.IsDone = false;
        Action.TargetObject = gameObject;
    }

    

    public void Remove()
    {
        GameObject.Destroy(gameObject);
    }

    public void ProduceLoot(int count)
    {
        //TODO: разбить лут на куски по InventoryStackSize!!!!!!!
        Transform lootContainer = GameObject.Find("LootContainer").transform;
        GameObject prefab = gameObject.transform.parent.gameObject.GetComponent<ResourceContainer>().Preloader.LootObjectPrefab;
        Vector3 lootPos = new Vector3(
                transform.position.x + Random.Range(-1.5f, 1.5f),
                transform.position.y,
                transform.position.z + Random.Range(-1.5f, 1.5f)
            );

        GameObject lootGO = GameObject.Instantiate(prefab, lootPos, Quaternion.identity, lootContainer);
        lootGO.name = $"{ProducedLoot.Name} {transform.position.x}-{transform.position.z}";
        lootGO.transform.Find("MainSprite").GetComponent<SpriteRenderer>().sprite = ProducedLoot.LootSprite;

        LocalResItem iItem = new LocalResItem()
        {
            Name = ProducedLoot.Name,
            InventoryStackSize = ProducedLoot.InventoryStackSize,
            AvailableCount = count
        };

        lootGO.GetComponent<LootObject>().InventoryItem = iItem;
    }
}
