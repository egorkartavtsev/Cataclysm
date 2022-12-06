using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosableWindow : MonoBehaviour
{
    public void ToggleActive()
    {
        bool isActive = gameObject.activeSelf;
        gameObject.SetActive(isActive ^ true);
    }
}
