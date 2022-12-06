using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Abstractions;
using System;

public class ActionManager : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public float EndPointTrashold = 0.5f;

    public bool Moving;
    public bool ActionReady = true;
    public bool Idle;

    IAction CurrentAction;

    private void Start()
    {
        agent = null;
        //agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        //CheckMoving();

       /* if (!Moving && ActionReady)
        {
            if (CurrentAction != null)
            {
                if (!CurrentAction.IsDone)
                {
                    CurrentAction.Do();
                }
                else
                    NextAction();
            }
        }*/
    }

    public void StartAction()
    {
        float dir = CurrentAction.TargetObject.transform.position.x > transform.position.x ? 1f : -1f;
        animator.SetFloat("Horizontal", dir);
        animator.SetTrigger("Attack");
    }

    public void DoAction()
    {
        CurrentAction.Work();
    }

    public void NextAction(IAction action = null)
    {
        if (action == null)
        {
            if (gameObject.name != "Player")
            {
                //Включаем поиск цели
            }
            else
            {
                if(CurrentAction != null) CurrentAction.IsDone = false;
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
}
