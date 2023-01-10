using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Abstractions;
using Models;
using GameData;
using Assets.Scripts.Engine.Models.Character;

public class PlayerControl : MonoBehaviour
{
    public GameObject PlayerObj;
    public float speed = 10;
    Player player;
    public LocationController locationController;
    Construction currentConstruction;
    ActionManager manager;

    private void Start()
    {
        //locationController = GameObject.Find("LocationController").gameObject.GetComponent<LocationController>();
        player = PlayerObj.GetComponent<Player>();
        manager = PlayerObj.GetComponent<ActionManager>();
    }

    // Start is called before the first frame update
    private void Update()
    {
        switch (locationController.gameMode) {
            case GameMode.DefaultView:
                DefaultControl();
                break;
            case GameMode.InMenu:
                MenuKBControl();
                break;
            case GameMode.BuildingView:
                BuildingViewKBControl();
                break;
        }
    }

    #region DefaultControls
    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Casts the ray and get the first game object hit 
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitted = hit.transform.gameObject;
                clickPos = hitted.transform.position;
                if (player.InActionArea(clickPos))
                {
                    IInteractive interactiveObj = hitted.GetComponent<IInteractive>();
                    if (interactiveObj != null)
                    {
                        manager.NextAction(interactiveObj.Action);
                        manager.StartAction();
                    }
                }
                //Instantiate(clickMarker, hit.point, Quaternion.identity); //places clickMarker at hit.point. This isn't needed, just there for visualisation.
            }
        }
    }

    void MovingControl()
    {
        int checkMove = 0;
        Vector3 offset = Vector3.zero;
        Vector3 curPos = player.GetPosition();
        CharacterMoveDirection direct = new CharacterMoveDirection();

        float curSpeed = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            offset += (curPos.z < 149) ? Vector3.forward : Vector3.zero;
            checkMove++;
            direct.Vertical += 1f;

        }
        if (Input.GetKey(KeyCode.A))
        {
            offset += (curPos.x > 1) ? Vector3.left : Vector3.zero;
            checkMove++;
            direct.Horizontal -= 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            offset += (curPos.z > 1) ? Vector3.back : Vector3.zero;
            checkMove++;
            direct.Vertical -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            offset += (curPos.x < 149) ? Vector3.right : Vector3.zero;
            checkMove++;
            direct.Horizontal += 1f;
        }

        offset = ValidateOffset(offset);
        if (checkMove > 0)
        {
            direct.Speed = 1f;
            player.DoMove(offset * curSpeed, direct);
        }
        else
        { 
            direct.Speed = 0f;
            direct.Horizontal = manager.animator.GetFloat("Horizontal");
            player.DoMove(Vector3.zero, direct);
        }
    }

    void DefaultControl()
    {
        MovingControl();
        MouseControl();
    }
    #endregion

    void MenuKBControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
              LocationEventManager.CloseMenu();
    }
    void BuildingViewKBControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LocationEventManager.ChangeGameMode(GameMode.DefaultView);

        if (currentConstruction != null)
        {
            #region Moving
            Vector3 offset = Vector3.zero;            
            if (Input.GetKeyUp(KeyCode.W))
                offset += Vector3.forward;
            if (Input.GetKeyUp(KeyCode.A))
                offset += Vector3.left;
            if (Input.GetKeyUp(KeyCode.S))
                offset += Vector3.back;
            if (Input.GetKeyUp(KeyCode.D))
                offset += Vector3.right;

            if(offset != Vector3.zero)
                currentConstruction.MoveTo(offset);
            #endregion

            if (Input.GetKeyUp(KeyCode.E))
                currentConstruction.Install();
        }
    }

    public void SetCurrentConstruction(Construction con = null)
    {
        currentConstruction = con;
    }

    Vector3 ValidateOffset(Vector3 offset)
    {
        if (offset.x != 0 && offset.z != 0)
        {
            return offset * Mathf.Sqrt(2) / 2;
        }
        return offset;
    }
}
