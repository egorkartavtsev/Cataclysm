using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abstractions;
using UnityEngine;
using Helpers;
using Models;

namespace Actions
{
    internal class TakeLoot : IAction
    {
        public bool IsDone { get; set; }
        public GameObject TargetObject { get; set; }
        public ActionManager Interactor { get; set; }
        public bool Available { get; set; }

        public void Do()
        {
            if (Available)
            { 
                SetUnavailable();
                Interactor.StartCoroutine(EventsAnimationUtil.Wait(1f, this)); //TODO: запускаем анимацию - имитация переделать!
            }
        }

        public void SetAvailable()
        {
            Work();
            Available = true;
        }

        public void SetUnavailable()
        {
            Available = false;
        }

        public void Work()
        {
            LocalResItem item = TargetObject.GetComponent<LootObject>().InventoryItem;
            if (Interactor.gameObject.GetComponent<CharacterBag>().AddNewItem(item))
            {
                TargetObject.GetComponent<LootObject>().Remove();
            }
            IsDone = true;
        }
    }
}
