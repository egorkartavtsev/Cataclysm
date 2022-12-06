using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedlyResourcePanel : MonoBehaviour
{
    public Image Icon;
    public Text Counts;

    public void ShowRes(Sprite icon, int needly, int current, bool allowed)
    {
        Icon.sprite = icon;
        Counts.text = $"{needly}/{current}";
    }
}
