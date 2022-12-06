using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameData;
using Models;

public class CharacterBag : MonoBehaviour
{
    [SerializeField] private int ItemsMaxCount = 5;
    public int SettlerId;
    Character character;

    public void Init(int _settlerId, bool _isNew)
    {
        SettlerId = _settlerId;
        character = WorldData.Characters[_settlerId];

        if(_isNew)
            character.Bag = new List<LocalResItem>();
    }

    public bool AddNewItem(LocalResItem item)
    {
        bool res = true;
        bool allowAdd = ItemsMaxCount > character.Bag.Count;

        LocalResItem existsItem = character.Bag.Where<LocalResItem>(
            i =>
                i.Name == item.Name &&
                i.AvailableCount + item.AvailableCount < i.InventoryStackSize
            )
            .ToList()
            .OrderBy(i => i.AvailableCount)
            .FirstOrDefault();

        if (existsItem != null)
        {
            int ind = character.Bag.IndexOf(existsItem);
            character.Bag[ind].AvailableCount += item.AvailableCount;
        }
        else if (allowAdd)
        {
            if (allowAdd)
            {
                character.Bag.Add
                    (
                        new LocalResItem()
                        {
                            InventoryStackSize = item.InventoryStackSize,
                            AvailableCount = item.AvailableCount,
                            Name = item.Name
                        }
                    );
            }
        }
        else
        { 
            res = false;
        }

        return res;
    }
}
