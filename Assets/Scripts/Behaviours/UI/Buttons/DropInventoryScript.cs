using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInventoryScript : MonoBehaviour
{
    public void Drop()
    {
        gameObject.transform.parent.GetComponent<BuildingShopPanel>().DropInventory();
    }
}
