using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Abstractions;
using System;
using CharacterOptions;
using Helpers;
using System.Linq;

public class ActionManager : MonoBehaviour
{
    public Animator animator;
    public AttackManager attackManager;


    public bool Moving;
    public bool AllowAttack = true;
    public bool ActionReady = true;
    public bool Idle;

    public float attackDistance = 2f;
    public float attackAngle = 40f;

    public Vector3 AttackHitPoint;



    Rigidbody rb;
    IAction CurrentAction;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        AttackHitPoint = new Vector3();
        //agent = null;
        //agent = GetComponent<NavMeshAgent>();
    }

    public void StartAction()
    {
        float dir = CurrentAction.TargetObject.transform.position.x > transform.position.x ? 1f : -1f;
        animator.SetFloat("Horizontal", dir);
        animator.SetTrigger("Bite");
    }

    public void DoAction()
    {
        CurrentAction.Work();
    }

    public void StartAttack(Vector3 pos)
    {
        AllowAttack = false;
        float dirX = pos.x > transform.position.x ? 1f : -1f;
        float dirZ = pos.z > transform.position.z ? 1f : -1f;
        AttackHitPoint = pos;

        animator.SetFloat("Horizontal", dirX);
        animator.SetFloat("Vertical", dirZ);
        string attackType = attackManager.CurrentWeaponType();
        Debug.Log(attackType);
        animator.SetTrigger($"{attackType}Attack");
    }

    public void ToggleWeapon()
    {
        attackManager.ToggleCurrentWeapon();
    }

    public void DoAttack()
    {
        attackManager.SetDamage(AttackHitPoint);
        AttackHitPoint = new Vector3();
    }

    public void DoMove(Vector3 offset, CharacterMoveDirection direct)
    {
        //manager.NextAction();
        rb.MovePosition(gameObject.transform.position + offset);
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, Camera.main.transform.position.y, gameObject.transform.position.z - 7);

        animator.SetFloat("Horizontal", direct.Horizontal);
        animator.SetFloat("Vertical", direct.Vertical);
        animator.SetFloat("Speed", direct.Speed);
    }

    public void NextAction(IAction action = null)
    {
        if (action == null)
        {
            if (gameObject.name != "Player")
            {
                //�������� ����� ����
            }
            else
            {
                if (CurrentAction != null) CurrentAction.IsDone = false;
                CurrentAction = null;
                Idle = true;
            }
        }
        else
        {
            CurrentAction = action;
            CurrentAction.Interactor = this;
            CurrentAction.Available = true;
            Idle = false;
        }
    }

    public void TakeDamage()
    {
        animator.SetTrigger("TakeDamage");
    }

}
