using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class BtnBuild : MonoBehaviour
{
    public BuildingShopItem SO;
    public PreloaderSO Preloader;

    public void GrabConstruction()
    {
        GameObject newConstr = GameObject.Instantiate(Preloader.Construction);
        newConstr.GetComponent<Construction>().Show(SO);

        LocationEventManager.ChangeGameMode(GameMode.BuildingView);
        LocationEventManager.CloseMenu();
    }
}
