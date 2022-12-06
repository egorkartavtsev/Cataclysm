using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public interface IEventMessagesContainer
{
    public void ShowDamageAnimation(int damage, Transform target);
    public void RemoveDmgLabel(GameObject label);
}
