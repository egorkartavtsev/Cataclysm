using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameData;
using Models;
using Abstractions;
using UnityEngine.EventSystems;
using Assets.Scripts.Engine.Models.Character;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public readonly float actionRadius = 1.5f;
    public Animator animator;
    public LocationController locationController;

    ActionManager manager;
    public Character player;
    Rigidbody rb;
    CharacterBag bag;

    // Start is called before the first frame update
    void Start()
    {
        bool isNew = true;
        switch (locationController.sceneType)
        {
            case SceneType.TestPlayer:
                player = new Character();
                gameObject.GetComponent<CharacterBag>().Init(this, true);
                break;
            default:
                isNew = InitPlayer();
                gameObject.GetComponent<CharacterBag>().Init(0, isNew);
                break;
        }
        

        gameObject.transform.position = new Vector3(player.PosX, 0.5f, player.PosZ);
        Camera.main.transform.position = new Vector3(player.PosX, 7f, player.PosZ - 7f);
        manager = gameObject.GetComponent<ActionManager>();

        rb = gameObject.GetComponent<Rigidbody>();
        int i = 0;
    }

    private void OnDestroy()
    {
        if (locationController.sceneType != SceneType.TestPlayer)
        { 
            Vector3 pos = GetCurrentTile();
            WorldData.Characters[0].PosX = Mathf.RoundToInt(pos.x);
            WorldData.Characters[0].PosZ = Mathf.RoundToInt(pos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private bool InitPlayer()
    {
        bool isNew = false;
        player = WorldData.Characters[0];
        if (player.PosX == 0)
        {
            LocationData ld = WorldData.Locations.Find(l => l.Current);
            Tile startTile = ld
                .Tiles
                .Where<Tile>
                (
                    t =>
                        t.LocalX > 20
                        && t.LocalX < 130
                        && t.LocalZ > 20
                        && t.LocalX < 130
                        && t.Background != BackgroundType.Water
                        && t.Contains == null
                ).ToList()
                .First();
            player.PosX = startTile.LocalX;
            player.PosZ = startTile.LocalZ;
            isNew = true;
            WorldData.SaveWorldData();
        }
        return isNew;
    }

    public bool InActionArea(Vector3 point)
    {
        Vector3 center = transform.position;
        return Mathf.Pow(point.x - center.x, 2) + Mathf.Pow(point.z - center.z, 2) <= Mathf.Pow(actionRadius, 2);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void DoMove(Vector3 offset, CharacterMoveDirection direct)
    {
        //manager.NextAction();
        rb.MovePosition(gameObject.transform.position +  offset);
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, Camera.main.transform.position.y, gameObject.transform.position.z - 7);

        animator.SetFloat("Horizontal", direct.Horizontal);
        animator.SetFloat("Vertical", direct.Vertical);
        animator.SetFloat("Speed", direct.Speed);
    }

    public Vector3 GetCurrentTile()
    {
        Vector3 res = new Vector3();

        Vector3 pos = gameObject.transform.position;
        res.x = Mathf.RoundToInt(pos.x);
        res.z = Mathf.RoundToInt(pos.z);

        return res;
    }

    public void SetSO()
    {
        int z = 0 - Mathf.RoundToInt(gameObject.transform.position.z);
        gameObject.transform.Find("MainSprite").gameObject.GetComponent<SpriteRenderer>().sortingOrder = z;
    }
}
