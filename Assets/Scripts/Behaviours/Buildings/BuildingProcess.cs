using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Models;
using Abstractions;
using Helpers;

public class BuildingProcess : MonoBehaviour //, IPointerClickHandler, IInteractive
{
    /*[Header("Links")]
    public TotalBuildingList BuildingList;
    public CurLocationData LocationData;

    [Header("Inners")]
    public int BuildingShopId = -1;
    public bool BuildInProcess = true;
    public List<NeedlyBuildMaterials> BuildMaterials = new List<NeedlyBuildMaterials>();

    private void Update()
    {
        if (BuildingShopId >= 0 && BuildInProcess)
            ShowBuildMatPanel();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"Click {gameObject.name}");

            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.TargetObject = gameObject;

            Vector3 targetPos = new Vector3
                (
                    gameObject.transform.position.x - 0.7f,
                    0,
                    gameObject.transform.position.z - 1f
                );

            player.StartMove(targetPos, SettlerCond.ReadyToWork);
        }
    }

    public void ShowBuildMatPanel()
    {
        if (BuildMaterials.Count == 0)
        {
            BuildMaterials = new List<NeedlyBuildMaterials>();
            foreach (NeedlyBuildMaterials bm in BuildingList.BuildList[BuildingShopId].BuildMaterials)
            {
                BuildMaterials.Add(new NeedlyBuildMaterials()
                {
                    Material = bm.Material,
                    CurrentCount = 0,
                    NeedlyCount = bm.NeedlyCount
                });
            }
        }

        GameObject panel = gameObject.transform.Find("NeedlyMaterialsContainer").gameObject;

        try
        {
            panel.transform.Find("Slot1").gameObject.GetComponent<SpriteRenderer>().sprite = BuildMaterials[0].Material.Icon;
            panel.transform.Find("Slot1").Find("counts").gameObject.GetComponent<TextMesh>().text = $"{BuildMaterials[0].CurrentCount}/{BuildMaterials[0].NeedlyCount}";
        }
        catch
        {
            panel.transform.Find("Slot1").gameObject.GetComponent<SpriteRenderer>().sprite = null;
            panel.transform.Find("Slot1").Find("counts").gameObject.GetComponent<TextMesh>().text = "";
        }

        try
        {
            panel.transform.Find("Slot2").gameObject.GetComponent<SpriteRenderer>().sprite = BuildMaterials[1].Material.Icon;
            panel.transform.Find("Slot2").Find("counts").gameObject.GetComponent<TextMesh>().text = $"{BuildMaterials[1].CurrentCount}/{BuildMaterials[1].NeedlyCount}";
        }
        catch
        {
            panel.transform.Find("Slot2").gameObject.GetComponent<SpriteRenderer>().sprite = null;
            panel.transform.Find("Slot2").Find("counts").gameObject.GetComponent<TextMesh>().text = "";
        }

        panel.SetActive(true);
    }

    public bool Interact(GameObject interactor, int influenceValue = 0)
    {
        int complBM = 0;
        List<NeedlyBuildMaterials> supBM = new List<NeedlyBuildMaterials>(BuildMaterials);

        foreach (NeedlyBuildMaterials bm in supBM)
        {
            int getCount = interactor.GetComponent<CharacterBag>().TryGetMaterial(bm.Material.Name, bm.NeedlyCount - bm.CurrentCount);
            NeedlyBuildMaterials nbm = bm;
            nbm.CurrentCount += getCount;
            BuildMaterials[BuildMaterials.IndexOf(bm)] = nbm;

            if (nbm.CurrentCount == nbm.NeedlyCount)
                complBM++;
        }

        if (complBM == BuildMaterials.Count)
        {
            BuildingComplete();
        }


        return false;
    }

    public void BuildingComplete()
    {
        BuildInProcess = false;
        gameObject.transform.Find("NeedlyMaterialsContainer").gameObject.SetActive(false);
        
        gameObject.GetComponent<BuildingScript>().BuildingShopId = BuildingShopId;
        gameObject.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        BuildingList.BuildList[BuildingShopId].ShowChilds = true;
        UpdateLocationData();

        gameObject.GetComponent<BuildingScript>().enabled = true;
        gameObject.GetComponent<BuildingProcess>().enabled = false;
    }

    private void UpdateLocationData()
    {
        List<LocationCell> curentCells = LocationData.Cells.Where(
                c => c.X >= Mathf.FloorToInt(gameObject.transform.position.x) &&
                     c.X <= Mathf.FloorToInt(gameObject.transform.position.x) + BuildingList.BuildList[BuildingShopId].Width - 1 &&
                     c.Z >= Mathf.FloorToInt(gameObject.transform.position.z) &&
                     c.Z <= Mathf.FloorToInt(gameObject.transform.position.z) + BuildingList.BuildList[BuildingShopId].Height - 1
            ).ToList();

        foreach (LocationCell cell in curentCells)
        {
            int cellInd = LocationData.Cells.IndexOf(cell);

            LocationData.Cells[cellInd].Contains = new LocationObject(100, gameObject.name, 6);
        }
    }*/
}
