using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using Models;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Assets.Scripts.Behaviours.Buildings;

public class Construction : MonoBehaviour
{
    public PreloaderSO Preloader;
    public GameObject AllowGrid;
    public GameObject LockedGrid;
    public GameObject MainSprite;

    PlayerControl playerControl;
    GameObject BVCamera;

    public BuildingShopItem SO;

    private AbstractConstruction abstractConstruction;

    private void Start()
    {
        BVCamera = GameObject.Find("BuildingView");
        playerControl = GameObject.Find("PlayerController").GetComponent<PlayerControl>();
        playerControl.SetCurrentConstruction(this);

        LocationEventManager.GameModeChanged += Cancel;

        this.abstractConstruction = GetAbstractConstruction();

        Vector3 pos = abstractConstruction.GetPos();
        gameObject.transform.position = new Vector3(pos.x, 0f, pos.z);
    }

    public void Show(BuildingShopItem building)
    {
        SO = building;

        SpriteRenderer s = MainSprite.GetComponent<SpriteRenderer>();
        s.sprite = SO.Icon;

        float width = SO.SizeXZ * 2f + 1f;
        s.size = new Vector2(width, SO.Height);
        MainSprite.transform.position = new Vector3(0f, SO.YOffset, SO.ZOffset);

        DrawGrid();
    }

    public void MoveTo(Vector3 offset)
    {
        abstractConstruction.MoveTo(offset);
    }

    public void Install()
    {
        abstractConstruction.Install();
        Cancel(GameMode.DefaultView);
    }

    private AbstractConstruction GetAbstractConstruction()
    {
        if(SO.NextLevelFor == null)
        {
            return new DefaultConstruction(SO,Preloader, AllowGrid, LockedGrid, MainSprite, playerControl, BVCamera, this.gameObject);
        } 
        else
        {
            return new NextLeveConstruction();
        }
    }

    void DrawGrid()
    {
        for (int z = 0 - SO.SizeXZ; z <= SO.SizeXZ; z++)
        {
            for (int x = 0 - SO.SizeXZ; x <= SO.SizeXZ; x++)
            {
                GameObject allowCell = GameObject.Instantiate(Preloader.LockedIndicator, AllowGrid.transform);
                GameObject lockedCell = GameObject.Instantiate(Preloader.LockedIndicator, LockedGrid.transform);

                Vector3 pos = new Vector3(x, 0.1f, z);
                allowCell.transform.position = lockedCell.transform.position = pos;

                allowCell.GetComponent<SpriteRenderer>().sprite = Preloader.AllowCell;
                lockedCell.GetComponent<SpriteRenderer>().sprite = Preloader.LockedCell;
            }
        }
        AllowGrid.SetActive(false);
        LockedGrid.SetActive(true);
    }

    bool BuildAllow(Vector3 pos)
    {
        return abstractConstruction.BuildAllow(pos);
    }

    private void Cancel(GameMode mode)
    {
        Destroy(gameObject);
        playerControl.SetCurrentConstruction();
    }

    private void OnDestroy()
    {
        LocationEventManager.GameModeChanged -= Cancel;
    } 
}
