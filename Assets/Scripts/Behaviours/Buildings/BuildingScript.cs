using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abstractions;
using Actions;

public class BuildingScript : MonoBehaviour, IInteractive
{
    public BuildingShopItem SO;
    public IAction Action { get; set; }

    private void Start()
    {
        Action = new OpenMenu(SO);
        Action.TargetObject = gameObject;
    }
}
