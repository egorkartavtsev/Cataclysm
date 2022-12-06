using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class BonfireBtn : MonoBehaviour
{
    public GameObject BonfirePrefab;
    public Sprite BonfireSprite;
    public LocationController LocationController;

    private void Start()
    {
        ToggleEnabled();
        LocationEventManager.BonfireSetUp += ToggleEnabled;
        LocationEventManager.BonfireDestroy += ToggleEnabled;
    }

    private void OnDisable()
    {
        LocationEventManager.BonfireSetUp -= ToggleEnabled;
        LocationEventManager.BonfireDestroy -= ToggleEnabled;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void ToggleEnabled()
    {
        //gameObject.GetComponent<Button>().enabled = !LocationController.CurrentLocation.HasBonfire;
        gameObject.SetActive(!LocationController.CurrentLocation.HasBonfire);
    }

    public void SetUpBonfire()
    {
        LocationController.SetUpCampfire(BonfirePrefab, BonfireSprite);
        LocationController.ToggleBonfire(true);
    }
    public void DestroyBonfire()
    {
        LocationController.ToggleBonfire(false);
    }

}
