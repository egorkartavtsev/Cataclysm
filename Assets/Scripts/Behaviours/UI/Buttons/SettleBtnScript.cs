using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettleBtnScript : MonoBehaviour, IPointerClickHandler
{
    public int SettlerId;
    public SettlerInfoContainer InfoPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        InfoPanel.SettlerId = SettlerId;
        InfoPanel.ShowSettlerInfo();
    }

    
}
