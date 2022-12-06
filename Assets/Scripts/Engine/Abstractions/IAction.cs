using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions
{
    public interface IAction
    {
        public bool IsDone { get; set; }
        public GameObject TargetObject { get; set; }
        public ActionManager Interactor { get; set; }
        public bool Available { get; set; }

        public void Do();

        public void Work();
        public void SetAvailable();
        public void SetUnavailable();
    }
}
