using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellsContainer : MonoBehaviour
{
    public GameObject ShellPrefab;

    public void CreateShell(Sprite shellSprite, Vector3 startpoint, Vector3 endpoint)
    {
        GameObject shell = GameObject.Instantiate(ShellPrefab, startpoint, Quaternion.identity, transform);
        StartCoroutine(EventsAnimationUtil.ShellFly(this, shell, endpoint));
    }
}
