using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventHandler : MonoBehaviour
{
    public ActionManager actionManager;

    public void DoSomethingBitch()
    {
        actionManager.DoAction();
    }

    public void EndAttack()
    {
        actionManager.AllowAttack = true;
    }

    public void DoAttack()
    { 
        actionManager.DoAttack();
    }
}
