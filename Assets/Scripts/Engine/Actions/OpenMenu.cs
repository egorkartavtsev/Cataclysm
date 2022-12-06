using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace Actions
{
    public class OpenMenu : IAction
    {
        public BuildingShopItem SO;

        public bool IsDone { get; set; }
        public GameObject TargetObject { get; set; }
        public ActionManager Interactor { get; set; }
        public bool Available { get; set; }

        public OpenMenu(BuildingShopItem _so)
        { 
            SO = _so;
        }

        public void Do()
        {
            if (Available)
            {
                SetUnavailable();
                Work();
            }
        }

        public void SetAvailable()
        {
            Available = true;
            IsDone = true;
        }

        public void SetUnavailable()
        {
            
        }

        public void Work()
        {
            GameObject panel = SO.OpenedMenu;
            Transform container = GameObject.Find("MainUI").gameObject.transform;
            GameObject newpanel = GameObject.Instantiate(panel, container.position, container.rotation, container);
            newpanel.GetComponent<IOpenedMenu>().Caller = SO;
            //Vector3 pos = newpanel.transform.position;
            //newpanel.transform.position = new Vector3(pos.x, pos.y + 15f, pos.z);
            SetAvailable();
        }
    }
}
