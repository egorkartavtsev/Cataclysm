using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models;
using Abstractions;
using Actions;

public class LootObject : MonoBehaviour, IInteractive
{
    public IAction Action { get; set; }
    public LocalResItem InventoryItem;

    // Start is called before the first frame update
    void Start()
    {
        Action = new TakeLoot();
        Action.IsDone = false;
        Action.TargetObject = gameObject;
    }

    public void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
