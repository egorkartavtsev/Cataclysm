using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameData;
using Models;
using Helpers;

public class FlexibleLayoutGroup : MonoBehaviour
{
    public GameObject ListItemPrefab;
    public SettlementData Settlement;
    public SettlerInfoContainer InfoPanel;

    private void OnEnable()
    {
        Settlement = WorldData.Settlements.Find(s => s.Home);
        Vector2 sDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
        GameObject item = null;

        for (int i = 0; i < Settlement.Settlers.Count; i++)
        {
            item = Instantiate(ListItemPrefab, gameObject.transform);
            item.name = Settlement.Settlers[i].Name;
            item.transform.Find("Text").GetComponent<Text>().text = item.name;
            item.GetComponent<SettleBtnScript>().SettlerId = Settlement.Settlers[i].SettlerId;
            item.GetComponent<SettleBtnScript>().InfoPanel = InfoPanel;
            sDelta.y += gameObject.GetComponent<GridLayoutGroup>().cellSize.y;
        }

        gameObject.GetComponent<RectTransform>().sizeDelta = sDelta;
    }

    private void OnDisable()
    {
        Vector2 sDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
        sDelta.y = 0f;
        gameObject.GetComponent<RectTransform>().sizeDelta = sDelta;

        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }


}
