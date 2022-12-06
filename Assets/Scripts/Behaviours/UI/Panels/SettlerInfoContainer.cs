using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Models;
using GameData;

public class SettlerInfoContainer : MonoBehaviour
{
    public GameObject CharacterName;
    //public GameObject CharacterIcon;
    public GameObject CharacterBagContainer;
    public WorldResourceShop WRShop;

    public int SettlerId = 0;
    public SettlementData Settlement;
    public Sprite EmptySlot;

    private void OnEnable()
    {
        SettlerId = 0;
        ShowSettlerInfo();
    }

    public void ShowSettlerInfo()
    {
        Character settler = WorldData.Characters[SettlerId];

        CharacterName.GetComponent<TMP_Text>().text = settler.Name;
        int i = 0;

        foreach (Transform BagItem in CharacterBagContainer.transform)
        {
            try
            {
                LocalResItem bagItem = settler.Bag[i];
                ResourceSO resourceSO = WRShop.GetSO(bagItem);
                BagItem.GetComponent<Image>().sprite = resourceSO.ShopItemIcon;
                BagItem.transform.Find("Count").GetComponent<TMP_Text>().text = bagItem.AvailableCount.ToString();
                BagItem.transform.Find("Name").GetComponent<TMP_Text>().text = bagItem.Name;
            }
            catch
            {
                BagItem.GetComponent<Image>().sprite = EmptySlot;
                BagItem.transform.Find("Count").GetComponent<TMP_Text>().text = "";
                BagItem.transform.Find("Name").GetComponent<TMP_Text>().text = "";
            }
            i++;
        }
    }
}
